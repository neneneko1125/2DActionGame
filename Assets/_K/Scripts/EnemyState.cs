using UnityEngine;

public class EnemyState : MonoBehaviour
{
    private enum ATKState
    {
        ATK,
        ChargeATK
    }

    private enum MoveState
    {
        ATKMove,
        EscapeMove
    }

    private ATKState _atkState;
    private MoveState _moveState;


    [Header("プレイヤーの情報")]
    public Transform _player; // プレイヤーの位置（Inspectorで指定）

    [SerializeField] private float _moveSpeed = 12f;

    private Rigidbody2D _rb;




    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }


    private void Move()
    {
        
    }

    private void ChangeState()
    {
        if (_atkState == ATKState.ATK)
        {

        }
    }
}
