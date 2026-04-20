using DG.Tweening;
using TMPro;
using UnityEngine;

/// <summary>
/// ダメージテキストの実際のアニメーションはここで管理する
/// </summary>
public class DamageAndHealTextAnimation : MonoBehaviour
{
    [Header("上昇量")]
    [SerializeField] private float _upAmount;

    [Header("演出時間")]
    [SerializeField] private float _duration;

    [SerializeField] private TextMeshProUGUI damageText;


    /// <summary>
    /// 実際のアニメーション
    /// Setupメソッドの最後に呼び出される
    /// </summary>
    /// <param name="startPosition"></param>
    /// <param name="parent"></param>
    private void DamageTextAnimation(Vector3 startPosition, Transform parent)
    {
        // 親を設定
        transform.SetParent(parent, false);

        // 開始位置を設定
        transform.position = startPosition;

        // 上昇アニメーション
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOMoveY(startPosition.y + _upAmount, _duration).SetEase(Ease.OutQuad));

        // アニメーション完了時に削除
        seq.OnComplete(() => Destroy(gameObject));
    }


    /// <summary>
    /// TextSpawnで呼び出される
    /// ダメージと、テキストの出現場所(ダメージを受けたキャラ座標)と、親を引数とする
    /// ダメージ(int)をテキスト型に変換してアニメーション再生する
    /// </summary>
    /// <param name="atk"></param>
    /// <param name="startPosition"></param>
    /// <param name="parent"></param>
    public void Setup(int damage, Vector3 startPosition, Transform parent)
    {
        if (damageText != null)
        {
            damageText.text = damage.ToString();
        }

        DamageTextAnimation(startPosition, parent);
    }
}
