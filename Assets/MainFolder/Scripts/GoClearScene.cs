using UnityEngine;
using UnityEngine.SceneManagement;

public class GoClearScene : MonoBehaviour
{
    [SerializeField] private GameObject _boss;
    [SerializeField] private string _clearScene;

    [SerializeField] private GameObject _arrow;
    [SerializeField] private GameObject _bgm;
    [SerializeField] private GameObject _bgm2;

    private void Start()
    {
        if (_arrow != null) _arrow.SetActive(false);
        if (_bgm != null) _bgm.SetActive(true);
        if (_bgm2 != null) _bgm2.SetActive(false);
    }

    private void Update()
    {
        if(_boss == null)
        {
            if (_arrow != null) _arrow.SetActive(true);
            if (_bgm != null) _bgm.SetActive(false);
            if (_bgm2 != null) _bgm2.SetActive(true);
        }
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(_boss == null && collision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene(_clearScene);
        }
    }
}
