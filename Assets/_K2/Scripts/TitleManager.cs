using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _teamCanvas;

    private void Start()
    {
        _mainCanvas.SetActive(true);
        _teamCanvas.SetActive(false);
    }

    public void OnClickMainCanvas()
    {
        _mainCanvas.SetActive(true);
        _teamCanvas.SetActive(false);
    }

    public void OnClickMainTeam()
    {
        _mainCanvas.SetActive(false);
        _teamCanvas.SetActive(true);
    }
}
