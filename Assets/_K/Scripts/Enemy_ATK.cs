using UnityEngine;

public class Enemy_ATK : MonoBehaviour
{
    private EnemyFireSkill _enemyFireSkill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _enemyFireSkill = GetComponent<EnemyFireSkill>();
    }

    // Update is called once per frame
    void Update()
    {
        ATK();
    }

    private void ATK()
    {
        if (_enemyFireSkill != null)
        {
            StartCoroutine(_enemyFireSkill.Shoot());
        }
        else
        {
            Debug.Log("EnemyFireSkill‚ªŒ©‚Â‚©‚è‚Ü‚¹‚ñ");
        }

    }
}
