
/// <summary>
/// スマホでボタン操作をするときはこれを使う
/// </summary>
public static class BoolButtonClick
{
    // A, D, S, Attack などの状態をここに集約
    //移動はスティックでやるからDとAはボツ
    //public static bool IsD;
    //public static bool IsA;
    public static bool IsS;
    public static bool IsW;
    public static bool IsAttack;
    public static bool IsDash;
    public static bool IsDown;
    public static bool IsUp;
    public static bool IsGuard;
}