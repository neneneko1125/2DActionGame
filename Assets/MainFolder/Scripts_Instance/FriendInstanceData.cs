using System.Collections.Generic;

public class FriendInstanceData : BaseInstanceData
{
    public FriendData Data { get; set; }
    protected override BaseCharacterData BaseData => Data;

    public FriendInstanceData(FriendData data)
    {
        Data = data;
        currentHP = data.MaxHP;
        currentLv = 1;
        currentEXP = 0;
        criticalProbability = data.CriticalProbability;
        FriendSaveManager.Load(this);
        Save();
    }


    //親クラスで使う
    protected override void Save() => FriendSaveManager.Save(this);
}