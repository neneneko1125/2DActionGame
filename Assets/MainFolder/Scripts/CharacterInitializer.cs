using UnityEngine;
using System.Collections;
/// <summary>
/// 3
/// Instantiateする、AttackObjectやHPスクリプトにInstanceDataを渡す
/// MakeAndSendInstanceData(InstanceData作成メソッド)の作成はシーン遷移前で、
/// これはシーン遷移後の最初に呼ばれるという順番
/// ＜仕事内容＞
/// 実際にInstantiateでキャラクターたちを生成
/// HPクラスやAttackObjectクラスにInstanceDataを渡す
/// </summary>
public class CharacterInitializer : MonoBehaviour
{
    void Start()
    {
        SetFriendsData();
        SetPlayerData();
    }

    /// <summary>
    /// Playerを実際に生成し、PlayerHPとPlayerAttackObjectに
    /// InstanceDataを渡す
    /// </summary>
    private void SetPlayerData()
    {
        //CharacterInstanceManagerが持ってるPlayerのInstanceDataをもらう
        PlayerInstanceData instance = CharacterInstanceManager.Instance.PlayerInstanceData;

        //実際にPlayerを生成
        GameObject obj = Instantiate(instance.Data.Prefab);

        //橋渡しのための座標情報をinstanceDataに教えてあげる
        instance.CharacterTransform = obj.transform;

        //PlayerHPにInstanceDataを渡す
        PlayerHP hp = obj.GetComponent<PlayerHP>();
        if (hp != null) 
        {
            hp.Initialize(instance);
        }

        GetInstanceData getData = obj.GetComponent<GetInstanceData>();
        if (getData != null)
        {
            getData.Initialize(instance);
        }
        else
        {
            Debug.Log("GetInstanceDataのセットに失敗");
        }

        //子オブジェクトの攻撃用オブジェクトたちにInstanceDataを渡す
        PlayerAttackObject[] attackObj = obj.GetComponentsInChildren<PlayerAttackObject>(true);
        if (attackObj.Length == 0)
        {
            Debug.Log("PlayerAttaKObjectが1つも見つかりません");
            return;
        }
        foreach (var atkObj in attackObj)
        {
            if (atkObj != null)
            {
                atkObj.Initialize(instance);
            }
        }
    }

    /// <summary>
    /// Friendたちを実際に生成し、FriendHPと
    /// ICharacterInitializerインタフェースを持った全クラスに
    /// InstanceDataを渡す
    /// </summary>
    private void SetFriendsData()
    {
        //CharacterInstanceManagerが持ってるFriendたちのFriendInstanceDataListをもらう(リスト単位で)
        var friendsInstanceData_List = CharacterInstanceManager.Instance.FriendsInstanceDataList;

        //---------ここからはfor文でFriend一体ごとに処理していく-----------
        for (int i = 0; i < friendsInstanceData_List.Count; i++)
        {
            FriendInstanceData instance = friendsInstanceData_List[i];

            //実際にFriend1体を生成
            GameObject obj = Instantiate(instance.Data.Prefab);

            //橋渡しのための座標情報をinstanceDataに教えてあげる
            instance.CharacterTransform = obj.transform;

            //FriendHPへInstanceDataに加えてインデックスも渡す(インデックスによってどのUIに表示させるかが変わってくるから)
            FriendHP hp = obj.GetComponent<FriendHP>();
            if (hp != null)
            {
                hp.Initialize(instance, i);
            }

            GetInstanceData getData = obj.GetComponent<GetInstanceData>();
            if(getData != null)
            {
                getData.Initialize(instance);
            }
            else
            {
                Debug.Log("GetInstanceDataのセットに失敗");
            }

            //objが持ってる「ICharacterInitializerインタフェースを持った全クラス」を取得
            var components = obj.GetComponentsInChildren<ICharacterInitializer>(true);

            foreach (var c in components)
            {
                //それぞれの攻撃用オブジェクトなどのInitializeを呼び出してInstanceデータを渡す
                c.Initialize(instance);
            }
        }
    }
}
