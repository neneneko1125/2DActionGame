using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.OnScreen;

public class Stick : MonoBehaviour, IDragHandler, IEndDragHandler
{
    [SerializeField] private RectTransform _stickTransform; // 動く赤い丸のTransform
    [SerializeField] private float _range = 50f; // 動く範囲

    public void OnDrag(PointerEventData eventData)
    {
        // マウス/指の位置を取得
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            transform as RectTransform, eventData.position, eventData.pressEventCamera, out pos);

        // X軸だけ制限をかけて代入、Y軸は0固定
        float x = Mathf.Clamp(pos.x, -_range, _range);
        _stickTransform.anchoredPosition = new Vector2(x, 0);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // 指を離したら真ん中に戻る
        _stickTransform.anchoredPosition = Vector2.zero;
    }
}