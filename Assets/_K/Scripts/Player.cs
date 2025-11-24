using UnityEngine;

public class Player_Test : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 8.0f;
    private float _direction = 0f;

    [SerializeField] private float _jumpForce = 15.0f;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private LayerMask _groundLayer;
    [SerializeField, Range(0, 1)] private float _checkRadius = 0.1f;
    private bool _isGrounded;

    private Rigidbody2D rb;

    private Vector3 _defaultScale;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        rb.linearVelocityX = _direction * _moveSpeed;

        if(_direction > 0)
        {
            transform.localScale = _defaultScale;
        }
        else if(_direction < 0)
        {
            transform.localScale = new Vector3(-_defaultScale.x, _defaultScale.y, _defaultScale.z);
        }
    }

    private void Jump()
    {
        _isGrounded = Physics2D.OverlapCircle(_groundChecker.position, _checkRadius, _groundLayer);

        if ((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && _isGrounded)
        {
            rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
        }
    }

}
