using UnityEngine;
using System.Collections;

public class PlayerATK : MonoBehaviour
{
    [SerializeField] private float _dashSpeed = 5.0f;
    [SerializeField] private float _dashMinSpeed = 1.0f;
    public bool IsDashing = false;

    [SerializeField] private float _downSpeed = 5.0f;
    public bool IsDowning = false;

    [SerializeField] private float _animTime = 0.5f;
    [SerializeField] private float _animTime_Down = 1.0f;
    [SerializeField] private Collider2D _atkCollider;
    [SerializeField] private Collider2D _atkCollider_Dash;
    [SerializeField] private Collider2D _atkCollider_Down;

    private Animator _anim;
    private Rigidbody2D _rb;

    public bool IsGuard = false;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
       
        _atkCollider.enabled = false;
        _atkCollider_Dash.enabled = false;
        _atkCollider_Down.enabled = false;
    }


    void Update()
    {
        ATK();
    }

    private void ATK()
    {
        if (!_atkCollider.enabled && !_atkCollider_Dash.enabled && !_atkCollider_Down.enabled && !IsGuard)
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S) && transform.localScale.x > 0)
                {
                    StartCoroutine(DashATK(Vector2.right));
                }
                else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S) && transform.localScale.x < 0)
                {
                    StartCoroutine(DashATK(Vector2.left));
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    StartCoroutine(DownATK());
                }
                else
                {
                    StartCoroutine(NomalATK());
                }
            }

            if (Input.GetMouseButton(1))
            {
                IsGuard = true;
                _anim.SetBool("Guard", true);
            }
        }

        if (Input.GetMouseButtonUp(1))
        {
            IsGuard = false;
            _anim.SetBool("Guard", false);
        }

    }

    private IEnumerator NomalATK()
    {
        SEManager.Instance.SEATK();
        _anim.SetBool("ATK", true);
        _atkCollider.enabled = true;
        yield return new WaitForSeconds(_animTime);
        _atkCollider.enabled = false;
        _anim.SetBool("ATK", false);
    }

    private IEnumerator DashATK(Vector2 dir)
    {
        SEManager.Instance.SEDashATK();

        IsDashing = true;
        _anim.SetBool("DashATK", true);
        _atkCollider_Dash.enabled = true;

        float currentSpeed = _dashSpeed;
        float duration = _animTime;
        float elapsed = 0f;

        //タイマーで減速
        while (elapsed < duration)
        {
            _rb.linearVelocity = dir * currentSpeed;
            currentSpeed = Mathf.Lerp(_dashSpeed, _dashMinSpeed, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }


        yield return new WaitForSeconds(_animTime);
        _atkCollider_Dash.enabled = false;
        _anim.SetBool("DashATK", false);
        IsDashing = false;
    }

    private IEnumerator DownATK()
    {
        SEManager.Instance.SEDownATK();

        IsDowning = true;
        _anim.SetBool("DownATK", true);
        _atkCollider_Down.enabled = true;
        _rb.AddForce(Vector2.down * _downSpeed, ForceMode2D.Impulse);
        yield return new WaitForSeconds(_animTime_Down);
        _atkCollider_Down.enabled = false;
        _anim.SetBool("DownATK", false);
        IsDowning = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && IsDowning)
        {
            _atkCollider_Down.enabled = false;
            _anim.SetBool("DownATK", false);
        }
    }
}
