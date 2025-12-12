public class FriendInstanceData
{
    public FriendData baseData; //元のデータ
    public int currentHP;
    public int level;
    public int exp;

    /// <summary>
    /// コンストラクタ
    /// MonoBehaviourだとコンストラクタは使えない
    /// </summary>
    /// <param name="data"></param>
    public FriendInstanceData(FriendData data)
    {
        baseData = data;
        currentHP = data.MaxHP;
        level = 1;
        exp = 0;
    }
}
