using System.Collections;
using UnityEngine;

public class FireSkill : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private float _fireSpeed = 5.0f;
    [SerializeField] private float _adjustShootPos_x = 0.5f;

    [SerializeField] private GameObject _fire2;
    [SerializeField] private float _fireSpeed2 = 5.0f;
    [SerializeField] private float _adjustShootPos2_x = 0.5f;

    private Animator _anim;
    private bool _isInterval = false;
    private bool _isInterval2 = false;
    [SerializeField] private float _animInterval = 0.5f;
    [SerializeField] private float _animInterval2 = 1.0f;

    private SpriteRenderer _sr;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }


    void Update()
    {
         StartCoroutine(Shoot());
    }

    private IEnumerator Shoot()
    {
        if (Input.GetMouseButtonDown(0) && _isInterval == false)
        {
            Fire();
            _anim.SetBool("ATK", true);
            _isInterval = true;
            yield return new WaitForSeconds(_animInterval);
            _isInterval = false;
            _anim.SetBool("ATK", false);
        }

        if (Input.GetMouseButtonDown(1) && _isInterval2 == false)
        {
            Fire2();
            _anim.SetBool("ATK", true);
            _isInterval2 = true;
            yield return new WaitForSeconds(_animInterval2);
            _isInterval2 = false;
            _anim.SetBool("ATK", false);
        }
    }

    private void Fire()
    {
        Vector2 shootPos;
        Vector2 direction;

        if(_sr.flipX == false)
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


    private void Fire2()
    {
        Vector2 shootPos;
        Vector2 direction;

        if (_sr.flipX == false)
        {
            shootPos = new Vector2(transform.position.x + _adjustShootPos2_x, transform.position.y);
            direction = Vector2.right;
        }
        else
        {
            shootPos = new Vector2(transform.position.x - _adjustShootPos2_x, transform.position.y);
            direction = Vector2.left;
        }


        if (_fire2 != null)
        {
            GameObject fireObj = Instantiate(_fire2, shootPos, Quaternion.identity);


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
                fireRb.AddForce(direction * _fireSpeed2, ForceMode2D.Impulse);
            }
        }

    }

}
