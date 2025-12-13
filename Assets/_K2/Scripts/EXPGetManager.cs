using UnityEngine;
using System.Collections.Generic;

public class EXPGetManager : MonoBehaviour
{
    public static EXPGetManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    public void AddExpToAll(int exp)
    {
        var ch = CharInstanceManager.Instance;

        if(ch.Player != null)
        {
            Debug.Log(ch.Player.baseData.PlayerName + "は" + exp + "経験値を得た");
            ch.Player.AddExp(exp);
        }
        
        
        foreach (var f in ch.Friends)
        {
            if(f != null)
            {
                Debug.Log(f.baseData.FriendName + "は" + exp + "経験値を得た");
                f.AddExp(exp);
            }
            
        }
            
    }
}
