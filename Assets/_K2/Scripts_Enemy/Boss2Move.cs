using System.Collections;
using UnityEngine;

public class Boss2Move : MonoBehaviour
{
    public float _moveSpeed = 5f;  //“G‚ÌˆÚ“®‘¬“x
    [SerializeField] private Transform _enemyTransform;

    private Rigidbody2D rb;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }


    void FixedUpdate()
    {
        EnemyMove();

    }

    /// <summary>
    /// “G‚Ì“®‚«‚ğŠÇ—
    /// </summary>
    void EnemyMove()
    {
        float moveX;    //ƒvƒŒƒCƒ„[‚Æ‚Í‹t•ûŒü‚ÉˆÚ“®‚·‚é‚±‚Æ‚É’ˆÓ

        //”½“]‚µ‚Ä‚È‚¢‚Æ‚«‚Í
        if (_enemyTransform.localScale.x > 0)
        {
            moveX = -_moveSpeed * Time.deltaTime;
        }
        //”½“]‚µ‚Ä‚é‚Æ‚«‚Í
        else
        {
            moveX = _moveSpeed * Time.deltaTime;
        }

        rb.linearVelocity = new Vector2(moveX * _moveSpeed, rb.linearVelocity.y);    //x•ûŒü‚É‘¬“x‚ğ‰Á‚¦‚ÄˆÚ“®‚³‚¹‚é
    }


}
