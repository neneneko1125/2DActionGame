using TMPro;
using UnityEngine;

public class DropItemManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _dropCoinText;
    public int dropCoinCount = 0;

    public static DropItemManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void UpdateDropCoinText()
    {
        if (_dropCoinText != null)
            _dropCoinText.text = dropCoinCount.ToString();
    }
}
