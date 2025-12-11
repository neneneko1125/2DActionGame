public class FriendInstanceData
{
    public FriendData BaseData; // Œ³‚Ìƒf[ƒ^
    public int currentHP;
    public int currentLv;
    public int currentEXP;

    //FriendData:ScriptableObject‚Ì‚±‚Æ
    public FriendInstanceData(FriendData data)
    {
        BaseData = data;
        currentHP = data.MaxHP;
    }
}
