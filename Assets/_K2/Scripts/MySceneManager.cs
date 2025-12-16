using UnityEngine;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{
    public void OnClickTitle()
    {
        SceneManager.LoadScene("TitleScene");
    }

    public void OnClickMain()
    {
        OrganizationManager.Instance.ApplyOrganization();
        SceneManager.LoadScene("MainScene");
    }
}
