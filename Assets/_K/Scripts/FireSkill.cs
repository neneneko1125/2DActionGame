using System.Collections;
using UnityEngine;

public class FireSkill : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private float _fireSpeed = 5.0f;
    [SerializeField] private float _adjustShootPos_x = 0.5f;

    private Animator _anim;
    private bool _isInterval = false;
    [SerializeField] private float _animInterval = 0.5f;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }


    void Update()
    {
        if(_isInterval == false) StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
            _anim.SetBool("ATK", true);
            _isInterval = true;
            yield return new WaitForSeconds(_animInterval);
            _isInterval = false;
            _anim.SetBool("ATK", false);
        }
    }

    private void Fire()
    {
        Vector2 shootPos;
        Vector2 direction;

        if(transform.localScale.x > 0)
        {
            shootPos = new Vector2(transform.position.x + _adjustShootPos_x, transform.position.y);
            direction = Vector2.right;
        }
        else
        {
            shootPos = new Vector2(transform.position.x - _adjustShootPos_x, transform.position.y);
            direction = Vector2.left;
        }
        

        if (_fire != null)
        {
            GameObject fireObj = Instantiate(_fire, shootPos, Quaternion.identity);

            
            if (direction == Vector2.left)
            {
                //ñÇñ@Ç‡ç∂âEîΩì]
                fireObj.transform.localScale = new Vector3(
                    -fireObj.transform.localScale.x,
                    fireObj.transform.localScale.y,
                    fireObj.transform.localScale.z
                );
            }

                Rigidbody2D fireRb = fireObj.GetComponent<Rigidbody2D>();

            if (fireRb != null)
            {
                fireRb.AddForce(direction * _fireSpeed, ForceMode2D.Impulse);
            }
        }

    }


}
