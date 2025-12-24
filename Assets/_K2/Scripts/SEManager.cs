using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource _source_damage;  //ダメージSEのAudioSource
    [SerializeField] private AudioClip _clip_damage; //ダメージSEのAudioClip

    [SerializeField] private AudioSource _source_ATK;
    [SerializeField] private AudioClip _clip_ATK;

    [SerializeField] private AudioSource _source_DashATK;
    [SerializeField] private AudioClip _clip_DashATK;

    [SerializeField] private AudioSource _source_DownATK;
    [SerializeField] private AudioClip _clip_DownATK;

    [SerializeField] private AudioSource _source_LvUp;
    [SerializeField] private AudioClip _clip_LvUp;

    [SerializeField] private AudioSource _source_coin;
    [SerializeField] private AudioClip _clip_coin;
    /*
    [SerializeField] private AudioSource _source_; 
    [SerializeField] private AudioClip _clip_;
    */

    public static SEManager Instance { get; private set; }   //シングルトンパターン　

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject); 
        }
    }

    //これを呼び出せばSEを鳴らすことができる
    public void SEDamage()
    {
        _source_damage.Play();
    }
    public void SEATK()
    {
        _source_ATK.Play();
    }

    public void SEDashATK()
    {
        _source_DashATK.Play();
    }

    public void SEDownATK()
    {
        _source_DownATK.Play();
    }

    public void SELvUp()
    {
        _source_LvUp.Play();
    }

    public void SECoin()
    {
        _source_coin.Play();
    }
}