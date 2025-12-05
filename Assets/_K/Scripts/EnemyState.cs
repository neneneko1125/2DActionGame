using UnityEngine;
/*
public class EnemyState_ : MonoBehaviour
{
    private enum ATKState
    {
        ATK,
        ChargeATK
    }

    public enum MoveState
    {
        ATKMove,
        EscapeMove
    }

    private ATKState _atkCurrentState = ATKState.ATK;
    public MoveState moveCurrentState = MoveState.ATKMove;


    [SerializeField] private Transform _player;
    private float _distanceToPlayer;
    [SerializeField] private float _stateChangeHP = 20f;

    
    [SerializeField] private float _distanceLimit = 5.0f; //プレイヤーに距離を詰められたとき回避するための変数

   
    


    private Rigidbody2D _rb;
    private HPManager _hpManager;
    private SpriteRenderer _sr;
    

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _hpManager = GetComponent<HPManager>();
        _sr = GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
       
    }


    private void FixedUpdate()
    {
        //プレイヤーとの距離を計算
        _distanceToPlayer = Vector2.Distance(transform.position, _player.position);

        //状態変更判定
        ChangeMoveState();
    }

    private void ChargeATK()
    {

    }


    private void ChangeMoveState()
    {
        if (moveCurrentState == MoveState.ATKMove && _distanceToPlayer < _distanceLimit)
        {
            Debug.Log("Escapeモードに切り替えた");
            moveCurrentState = MoveState.EscapeMove;
        }
        else if (moveCurrentState == MoveState.EscapeMove && _distanceToPlayer > _distanceLimit)
        {
            Debug.Log("ATKモードに切り替えた");
            moveCurrentState = MoveState.ATKMove;
        }

    }

   


}
*/
