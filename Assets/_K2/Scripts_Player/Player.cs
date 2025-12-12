using UnityEngine;

public class Player : MonoBehaviour
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
    private PlayerATK _playerATK;


    private Vector3 _defaultScale;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _playerATK = GetComponent<PlayerATK>();
        _defaultScale = transform.localScale;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if(!_playerATK.IsDashing && !_playerATK.IsDowning)
        {
            Walk();
            Jump();
        }

    }


    private void Walk()
    {
        _direction = Input.GetAxisRaw("Horizontal");

        if (_playerATK.IsGuard)
        {
            _rb.linearVelocityX = 0;
        }
        else
        {
            _rb.linearVelocityX = _direction * _moveSpeed;
        }
        

        if(_direction > 0)
        {
            transform.localScale = _defaultScale;
            _anim.SetBool("Walk", true);
        }
        else if(_direction < 0)
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
            if (_playerATK.IsGuard)
            {
                return;
            }
            else
            {
                _rb.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            }
            
        }
    }

}
