using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource source_damage;  //ダメージSEのAudioSource
    [SerializeField] private AudioClip clip_damage; //ダメージSEのAudioClip

    public static SEManager Instance { get; private set; }   //シングルトンパターン　

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //このインスタンスがnullならば
        if (Instance == null)
        {
            Instance = this;    //これをインスタンスとする
        }
        else
        {
            Destroy(gameObject);    //これを削除する
        }
    }

    //これを呼び出せばSEを鳴らすことができる
    public void DamageSE()
    {
        source_damage.Play();
    }

}