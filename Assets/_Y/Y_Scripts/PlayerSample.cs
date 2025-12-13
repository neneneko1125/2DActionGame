using UnityEngine;

public class PlayerSample : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8.0f;
    private float _direction = 0f;

    [SerializeField] private float _jumpForce = 15.0f;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField, Range(0, 1)] private float _checkRadius = 0.1f;
    private bool _isGrounded;

    private Rigidbody2D _rb;
    private Animator _anim;


    private Vector3 _defaultScale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _defaultScale = transform.localScale;
    }

    void Start()
    {

    }

    void Update()
    {
        Walk();
        Jump();
    }


    private void Walk()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        _rb.linearVelocityX = _direction * _moveSpeed;



        if (_direction > 0)
        {
            transform.localScale = _defaultScale;
            _anim.SetBool("Walk", true);
        }
        else if (_direction < 0)
        {
            transform.localScale = new Vector3(-_defaultScale.x, _defaultScale.y, _defaultScale.z);
            _anim.SetBool("Walk", true);
        }
        else
        {
            _anim.SetBool("Walk", false);
        }
    }

    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _groundLayer);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _isGrounded)
        {
            _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

}

