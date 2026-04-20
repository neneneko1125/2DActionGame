using UnityEngine;

/// <summary>
/// 他のキャラクターに見つけてもらうためにこれを使う
/// Findで探してもいいが、重たくなるからシングルトンパターンを使用
/// </summary>
public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance { get; private set; }

    private void Awake()
    {
        // もしすでに誰か（古いプレイヤー）が登録されていたら、自分を最新として上書きする
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
}