using UnityEngine;

public class CharacterBufferObject : MonoBehaviour
{
    [SerializeField] private BuffData _buff;

    //自分自身の保持者をキャッシュ
    private ICharacterInstanceHolder _ownerHolder;

    private void Awake()
    {
        //親オブジェクト(バッファー本人)からコンポーネントを探して保持
        _ownerHolder = GetComponentInParent<ICharacterInstanceHolder>();
    }

    private void OnEnable()
    {
        if (_ownerHolder != null)
        {
            Debug.Log("発動者本人にバフを適用");
            _ownerHolder.InstanceData.AddBuff(_buff);
        }
    }

    private void OnDisable()
    {
        if (_ownerHolder != null)
        {
            Debug.Log("発動者本人のバフを解除");
            _ownerHolder.InstanceData.RemoveBuff(_buff);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICharacterInstanceHolder>(out var holder))
        {
            // 自分自身以外なら
            if (holder != _ownerHolder)
            {
                holder.InstanceData.AddBuff(_buff); //バフを付与
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent<ICharacterInstanceHolder>(out var holder))
        {
            if (holder != _ownerHolder)
            {
                holder.InstanceData.RemoveBuff(_buff);      //バフ解除
            }
        }
    }
}