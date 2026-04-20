using UnityEngine;
using System;

public class FriendWarp : MonoBehaviour
{
    [Header("プレイヤーとこの距離以上離れたら自動ワープ")]
    [SerializeField] private float _warpRange = 10.0f;

    public event Action OnWarped;

    //プレイヤーを安全に取得するプロパティ
    private Transform PlayerTransform
    {
        get
        {
            // PlayerManager(シングルトン)がいればそこから、いなければタグで探す
            if (PlayerManager.Instance != null) return PlayerManager.Instance.transform;
            return GameObject.FindGameObjectWithTag("Player")?.transform;
        }
    }

    private void Update()
    {
        var player = PlayerTransform;
        if (player == null) return;

        float xDistance = Mathf.Abs(player.position.x - transform.position.x);

        // Qキーか、距離が離れすぎたらワープ
        //距離が離れたらワープするのはPC、スマホ共通
        if (Input.GetKeyDown(KeyCode.Q) || xDistance > _warpRange)
        {
            WarpToPlayer(player);
        }
    }

    /// <summary>
    /// イベントとして登録するためのメソッド
    /// </summary>
    private void OnWarpCommandReceived()
    {
        var player = PlayerTransform;
        if (player == null) return;

        WarpToPlayer(player);
    }

    public void WarpToPlayer(Transform player)
    {
        // プレイヤーの少し後ろに移動
        transform.position = new Vector3(player.position.x - 1, player.position.y + 1, player.position.z);

        OnWarped?.Invoke();
    }

    //ここからはスマホで遊ぶときのボタンから呼び出される
    private void OnEnable() => GameEventManager.OnWarpCommand += OnWarpCommandReceived;
    private void OnDisable() => GameEventManager.OnWarpCommand -= OnWarpCommandReceived;
}