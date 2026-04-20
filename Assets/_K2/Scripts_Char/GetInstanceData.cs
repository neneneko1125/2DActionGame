using UnityEngine;

/// <summary>
/// オブジェクトそのものにInstanceDataの情報を持たせる
/// </summary>
public class GetInstanceData : MonoBehaviour, ICharacterInstanceHolder
{
    public BaseInstanceData InstanceData { get; private set; }

    public void Initialize(BaseInstanceData data)
    {
        InstanceData = data;
    }
}
