using UnityEngine;

public class FriendInstallationAttack : CharacterInstallationAttack, ICharacterInitializer
{
    private FriendInstanceData _instanceData;

    /// <summary>
    /// FriendInstaceDataを取得
    /// </summary>
    /// <param name="data"></param>
    public void Initialize(FriendInstanceData data) => _instanceData = data;


    /// <summary>
    /// FriendはEnemyと違ってinstanceデータからレベルを取得し攻撃力に反映させる必要がある
    /// インタフェースでオブジェクト生成時に攻撃用オブジェクトたちにInstanceDataを配ってはいるが、
    /// 途中から生成される、遠距離攻撃オブジェクトには配れないからここで配る
    /// </summary>
    /// <param name="bullet"></param>
    protected override void OnBulletShot(GameObject bullet)
    {

        FriendAttackObject friendAttackObject = bullet.GetComponent<FriendAttackObject>();
        if (friendAttackObject != null)
        {
            friendAttackObject.Initialize(_instanceData);
        }
    }
}
