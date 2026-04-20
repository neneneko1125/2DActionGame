using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class MySceneManager : MonoBehaviour
{

    private void Update()
    {
        //デバッグ用
        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("1-2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("1-3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && Input.GetKey(KeyCode.M))
        {
            SceneManager.LoadScene("Boss1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.N))
        {
            SceneManager.LoadScene("2-1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.N))
        {
            SceneManager.LoadScene("2-2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.N))
        {
            SceneManager.LoadScene("2-3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && Input.GetKey(KeyCode.N))
        {
            SceneManager.LoadScene("Boss2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha1) && Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene("3-1");
        }
        if (Input.GetKeyDown(KeyCode.Alpha2) && Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene("3-2");
        }
        if (Input.GetKeyDown(KeyCode.Alpha3) && Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene("3-3");
        }
        if (Input.GetKeyDown(KeyCode.Alpha0) && Input.GetKey(KeyCode.L))
        {
            SceneManager.LoadScene("Boss3");
        }

    }

    public void OnClickTitle()
    {
        PlayerPrefs.DeleteKey("PLAYER_SAVE");
        FriendSaveManager.DeleteAllFriendData(OrganizationManager.Instance.AllFriends);
       // OrganizationManager.Instance.SelectedFriends.Clear();   //ここで編成データも削除
        SceneManager.LoadScene("TitleScene");
    }

    public void OnClickStage1()
    {
        PlayerPrefs.DeleteKey("PLAYER_SAVE");
        FriendSaveManager.DeleteAllFriendData(OrganizationManager.Instance.AllFriends);
        //これから出撃するのでClearはだめ

        OrganizationManager.Instance.MakeAndSendInstanceData();
        SceneManager.LoadScene("1-1");
    }

    public void OnClickStage2()
    {
        PlayerPrefs.DeleteKey("PLAYER_SAVE");
        FriendSaveManager.DeleteAllFriendData(OrganizationManager.Instance.AllFriends);
        //これから出撃するのでClearはだめ

        OrganizationManager.Instance.MakeAndSendInstanceData();
        SceneManager.LoadScene("2-1");
    }

    public void OnClickStage3()
    {
        PlayerPrefs.DeleteKey("PLAYER_SAVE");
        FriendSaveManager.DeleteAllFriendData(OrganizationManager.Instance.AllFriends);
        //これから出撃するのでClearはだめ

        OrganizationManager.Instance.MakeAndSendInstanceData();
        SceneManager.LoadScene("3-1");
    }
}
