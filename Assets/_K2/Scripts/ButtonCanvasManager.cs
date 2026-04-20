using UnityEngine;

/// <summary>
/// スマホで遊ぶチェックのONOFF状態によって自動で表示非表示を切り替え
/// </summary>
public class ButtonCanvasManager : MonoBehaviour
{
    private void Start()
    {
        ChangeActive();
    }

    void ChangeActive()
    {
        if (InputChangeButton.IsPressedSystem)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }
}
