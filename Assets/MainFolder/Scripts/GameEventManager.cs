using System;
/// <summary>
/// ワープとジャンプのイベントを管理
/// </summary>
public static class GameEventManager
{
    //オブジェクトが紐づいてないやつらはstaticを利用してイベントを起こす
    //(例えばPlayerInstanceData,PlayerHPにあるイベントはちゃんと両者が繋がってるからここでやる必要はない)
    public static event Action OnWarpCommand;
    public static event Action OnJumpCommand;


    //命令を発信する（PlayerButtonから呼ばれる）
    public static void RaiseWarp() => OnWarpCommand?.Invoke();
    public static void RaiseJump() => OnJumpCommand?.Invoke();
}