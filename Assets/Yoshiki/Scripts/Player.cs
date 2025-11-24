using UnityEngine;

public class Player_Test : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 8.0f;
    private float direction = 0f;
    [SerializeField] private float jumpForce = 15.0f;

    private Rigidbody2D rb;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        InputWalk();
    }

    private void FixedUpdate()
    {
        Walk(direction);
    }

    private void InputWalk()
    {
        direction = Input.GetAxis("Horizontal");
    }

    private void Walk(float direc)
    {
        
        rb.linearVelocityX = direc * moveSpeed;
    }
}
