using UnityEngine;
/*
public class Fire_ : MonoBehaviour
{
    [SerializeField] private int _atk = 5;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            HPManager hpManager = collision.gameObject.GetComponent<HPManager>();

            if (hpManager != null)
            {
                hpManager.ReduceHP(_atk);
            }
            else
            {
                Debug.Log("HPManager‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ‚Å‚µ‚½");
            }

            Destroy(gameObject);
        }
    }
}

*/
