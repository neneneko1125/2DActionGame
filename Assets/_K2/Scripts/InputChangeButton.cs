using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// PCかスマホかを切り替えるクラス
/// </summary>
public class InputChangeButton : MonoBehaviour
{
    [SerializeField] private Image _check;
    public static bool IsPressedSystem {  get; private set; }

    private void Start()
    {
        if (IsPressedSystem)
        {
            _check.enabled = true;
            IsPressedSystem = true;
        }
        else
        {
            _check.enabled = false;
            IsPressedSystem = false;
        }
        
    }

    public void OnClickInputChange()
    {
        if(_check.enabled)
        {
            _check.enabled = false;
            IsPressedSystem = false;
        }
        else
        {
            _check.enabled = true;
            IsPressedSystem = true;
        }
    }
}
