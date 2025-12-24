using UnityEngine;

public class FriendHealObject : MonoBehaviour
{
    private FriendInstanceData _instance;

    [SerializeField] private int _healDefault = 1;
    [SerializeField] private float _scalingFactor = 1.5f;
    

    /// <summary>
    /// FriendInstaceData‚ðŽæ“¾
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(FriendInstanceData data)
    {
        _instance = data;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_instance == null) 
        {
            Debug.Log("Instance‚ªnull");
            return;
        }

        int heal = CalcHeal();

        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player‚ðŒŸ’m‚µ‚½");
            var hp = collision.GetComponent<PlayerHP>();
            if (hp != null)
            {
                Debug.Log("Player‚ð‰ñ•œ");
                hp.Heal(heal);
            }
        }
        else if (collision.CompareTag("Friend"))
        {
            Debug.Log("Friend‚ðŒŸ’m‚µ‚½");
            var hp = collision.GetComponent<FriendHP>();
            if (hp != null)
            {
                Debug.Log("Friend‚ð‰ñ•œ");
                hp.Heal(heal);
            }
        }

        Destroy(gameObject);
    }

    private int CalcHeal()
    {
        float lvMul = 1 + (_instance.currentLv - 1) * _scalingFactor;
        return Mathf.RoundToInt(_healDefault * lvMul + _instance.currentLv);
    }

}
