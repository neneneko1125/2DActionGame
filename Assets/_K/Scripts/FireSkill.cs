using UnityEngine;

public class FireSkill : MonoBehaviour
{
    [SerializeField] private GameObject _fire;
    [SerializeField] private float _fireSpeed = 5.0f;
    [SerializeField] private float _adjustShootPos_x = 0.5f;

    
    void Start()
    {
        
    }


    void Update()
    {
        Shoot();
    }

    private void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Fire();
        }
    }

    private void Fire()
    {
        Vector2 shootPos = new Vector2(transform.position.x + _adjustShootPos_x, transform.position.y);

        if (_fire != null)
        {
            GameObject fireObj = Instantiate(_fire, shootPos, Quaternion.identity);
            Rigidbody2D fireRb = fireObj.GetComponent<Rigidbody2D>();

            if (fireRb != null)
            {
                fireRb.AddForce(Vector2.right * _fireSpeed, ForceMode2D.Impulse);
            }
        }

    }


}
