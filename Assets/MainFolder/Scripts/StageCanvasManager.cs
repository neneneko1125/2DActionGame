using UnityEngine;

public class StageCanvasManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _tutorialCanvas;
   
    void Start()
    {
        _mainCanvas.SetActive(true);
        _tutorialCanvas.SetActive(false);
    }


    public void OnClickMain()
    {
        Time.timeScale = 1f;
        _mainCanvas.SetActive(true);
        _tutorialCanvas.SetActive(false);
    }

    public void OnClickTutorial()
    {
        _mainCanvas.SetActive(false);
        _tutorialCanvas.SetActive(true);
        Time.timeScale = 0f;
    }
}
