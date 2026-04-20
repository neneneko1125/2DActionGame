using UnityEngine;

//PlayerAttackでも使うからクラス外でpublicにしておく
public enum AttackType { None, Normal, Dash, Down, Up};

/// <summary>
/// 移動、攻撃、ガードの入力処理
/// </summary>
public class PlayerInput : MonoBehaviour
{
    public float MoveDirection {  get; private set; }   //移動方向
    public bool IsJumpButtonPressed { get; private set; }     //ジャンプ入力
    public bool IsGuarding { get; private set; }    //ガード中trueになる
    public AttackType CurrentAttackType { get; private set; }    //攻撃の種類
   

    void Update()
    {
        //ガード中は移動ができない
        if (!IsGuarding)
        {
            //移動の入力処理
            InputMovement();
        }

        // 攻撃タイプ (PCとスマホ共通)
        CurrentAttackType = GetAttackType();

        //ガード入力処理
        if (!InputChangeButton.IsPressedSystem)
        {
            // PC操作の場合
            IsGuarding = Input.GetMouseButton(1);
        }
        else
        {
            // スマホ操作の場合
            IsGuarding = BoolButtonClick.IsGuard;
        }
    }
    /// <summary>
    /// 左右の移動とジャンプの入力
    /// </summary>
    private void InputMovement()
    {
        //PCで遊ぶ場合
        if (!InputChangeButton.IsPressedSystem)
        {
            MoveDirection = Input.GetAxisRaw("Horizontal");
            IsJumpButtonPressed = Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space);
        }
    }
    /// <summary>
    /// 攻撃の入力
    /// </summary>
    /// <returns></returns>
    private AttackType GetAttackType()
    {
        //PCで遊ぶ場合
        if (!InputChangeButton.IsPressedSystem)
        {
            //左クリックを押したとき
            if (Input.GetMouseButtonDown(0))
            {
                //Sを押している状態のとき
                if (Input.GetKey(KeyCode.S))
                {
                    //一緒にAやDを押していればダッシュ攻撃 どっち方向にダッシュするかはPlayerAttack側で決定する
                    if ((Input.GetKey(KeyCode.D) && transform.localScale.x > 0) || (Input.GetKey(KeyCode.A) && transform.localScale.x < 0))
                    {
                        return AttackType.Dash;
                    }
                    //Sだけなら下攻撃
                    else
                    {
                        return AttackType.Down;
                    }
                }

                //左クリックだけなら通常攻撃
                return AttackType.Normal;
            }
            //左クリックしたままジャンプすれば上攻撃
            else if((Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.Space)) && Input.GetMouseButton(0))
            {
                return AttackType.Up;
            }
            else
            {
                return AttackType.None;
            }
           
        }
        //スマホで遊ぶ場合
        else
        {
            if (BoolButtonClick.IsDash) return AttackType.Dash;
            if (BoolButtonClick.IsDown) return AttackType.Down;
            if (BoolButtonClick.IsUp) return AttackType.Up;
            if (BoolButtonClick.IsAttack) return AttackType.Normal;
            return AttackType.None;
        }
    }

    /// <summary>
    /// 攻撃が受理されたらリセットするためのメソッド
    /// </summary>
    public void ClearAttackType()
    {
        CurrentAttackType = AttackType.None;
    }
}
