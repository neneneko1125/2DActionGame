using UnityEngine;

public class SEManager : MonoBehaviour
{
    [SerializeField] private AudioSource _sourceDamage;

    [SerializeField] private AudioSource _sourceAttack;

    [SerializeField] private AudioSource _sourceDashAttack;

    [SerializeField] private AudioSource _sourceDownAttack;

    [SerializeField] private AudioSource _sourceLvUp;

    [SerializeField] private AudioSource _sourceCoin;

    [SerializeField] private AudioSource _sourceFire;

    [SerializeField] private AudioSource _sourceIce;

    [SerializeField] private AudioSource _sourceThunder;

    [SerializeField] private AudioSource _sourceThunder2;

    [SerializeField] private AudioSource _sourceBeam;

    [SerializeField] private AudioSource _sourceBeam2;

    [SerializeField] private AudioSource _sourceTornado;

    [SerializeField] private AudioSource _sourceHeal;

    [SerializeField] private AudioSource _sourceBuff;

    [SerializeField] private AudioSource _sourceButton;

    //[SerializeField] private AudioSource _source; 
    

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
    public void SEDamage() => _sourceDamage.Play();
    
    public void SEAttack() => _sourceAttack.Play();

    public void SEDashAttack() => _sourceDashAttack.Play();

    public void SEDownAttack() => _sourceDownAttack.Play();

    public void SELvUp() => _sourceLvUp.Play();

    public void SECoin() => _sourceCoin.Play();

    public void SEFire() => _sourceFire.Play();

    public void SEIce() => _sourceIce.Play();

    public void SEThudener() => _sourceThunder.Play();

    public void SEThudener2() => _sourceThunder2.Play();

    public void SEBeam() => _sourceBeam.Play();

    public void SEBeam2() => _sourceBeam2.Play();

    public void SETornado() => _sourceTornado.Play();

    public void SEHeal() => _sourceHeal.Play();

    public void SEBuff() => _sourceBuff.Play();

    public void SEButton() => _sourceButton.Play();
}