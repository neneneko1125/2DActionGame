using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource source_damage;  //ダメージSEのAudioSource
    [SerializeField] private AudioClip clip_damage; //ダメージSEのAudioClip

    [SerializeField] private AudioSource source_ATK;
    [SerializeField] private AudioClip clip_ATK;

    [SerializeField] private AudioSource source_DashATK;
    [SerializeField] private AudioClip clip_DashATK;

    [SerializeField] private AudioSource source_DownATK;
    [SerializeField] private AudioClip clip_DownATK;
    /*
    [SerializeField] private AudioSource source_; 
    [SerializeField] private AudioClip clip_;
    */

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
    public void SEDamage()
    {
        source_damage.Play();
    }
    public void SEATK()
    {
        source_ATK.Play();
    }

    public void SEDashATK()
    {
        source_DashATK.Play();
    }

    public void SEDownATK()
    {
        source_DownATK.Play();
    }
}