using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private GameObject _stageSelectCanvas;
    [SerializeField] private GameObject _teamCanvas;
    [SerializeField] private GameObject _optionCanvas;
    [SerializeField] private GameObject _tutorialCanvas;


    private void Start()
    {
        _mainCanvas.SetActive(true);
        _stageSelectCanvas.SetActive(false);
        _teamCanvas.SetActive(false);
        _optionCanvas.SetActive(false);
        _tutorialCanvas.SetActive(false);
    }

    public void OnClickMainCanvas()
    {
        _mainCanvas.SetActive(true);
        _stageSelectCanvas.SetActive(false);
        _teamCanvas.SetActive(false);
        _optionCanvas.SetActive(false);
        _tutorialCanvas.SetActive(false);
    }

    public void OnClickStageSelectCanvas()
    {
        _mainCanvas.SetActive(false);
        _stageSelectCanvas.SetActive(true);
        _teamCanvas.SetActive(false);
        _optionCanvas.SetActive(false);
        _tutorialCanvas.SetActive(false);
    }

    public void OnClickMainTeam()
    {
        _mainCanvas.SetActive(false);
        _stageSelectCanvas.SetActive(false);
        _teamCanvas.SetActive(true);
        _optionCanvas.SetActive(false);
        _tutorialCanvas.SetActive(false);
    }

    public void OnClickMainOption()
    {
        _mainCanvas.SetActive(false);
        _stageSelectCanvas.SetActive(false);
        _teamCanvas.SetActive(false);
        _optionCanvas.SetActive(true);
        _tutorialCanvas.SetActive(false);
    }

    public void OnClickTutorial()
    {
        _mainCanvas.SetActive(false);
        _stageSelectCanvas.SetActive(false);
        _teamCanvas.SetActive(false);
        _optionCanvas.SetActive(false);
        _tutorialCanvas.SetActive(true);
    }
}
