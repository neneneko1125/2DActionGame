using UnityEngine;
using System.Collections;
public class EnemyFireSkill : MonoBehaviour
{
    [SerializeField] private GameObject _fire; 
    [SerializeField] private float _fireSpeed = 5.0f;
    [SerializeField] private float _adjustShootPos_x = 0.5f;

    [SerializeField] private GameObject _fire2;
    [SerializeField] private float _fireSpeed2 = 5.0f;
    [SerializeField] private float _adjustShootPos2_x = 0.5f;

    private Animator _anim;
    private bool _isInterval = false;
    private float _animInterval = 0.25f;
    [SerializeField] private float _animIntervalMin = 0.15f;
    [SerializeField] private float _animIntervalMax = 0.7f;

    private bool _isInterval2 = false;
    private float _animInterval2 = 0.25f;
    [SerializeField] private float _animIntervalMin2 = 1f;
    [SerializeField] private float _animIntervalMax2 = 3f;

    private SpriteRenderer _sr;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _sr = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Shoot()
    {
        if (_isInterval == false)
        {
            _animInterval = Random.Range(_animIntervalMin, _animIntervalMax);
            Fire();
            _anim.SetBool("ATK", true);
            _isInterval = true;
            yield return new WaitForSeconds(_animInterval);
            _isInterval = false;
            _anim.SetBool("ATK", false);
        }

        if (_isInterval2 == false)
        {
            _animInterval2 = Random.Range(_animIntervalMin2, _animIntervalMax2);
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

        if (_sr.flipX == true)
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

        if (_sr.flipX == true)
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
