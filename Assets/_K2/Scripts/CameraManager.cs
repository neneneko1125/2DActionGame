using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [SerializeField] private Transform _target;

    [SerializeField] private float _smoothTime = 0.2f;
    private Vector3 _velocity = Vector3.zero;

    [SerializeField] private float _minX, _maxX;
    [SerializeField] private float _minY, _maxY;

    void LateUpdate()
    {
        FollowTarget();
    }

    void FollowTarget()
    {
        if (_target == null) return;

        // ñ⁄ïWà íu
        Vector3 targetPos = new Vector3(
            Mathf.Clamp(_target.position.x, _minX, _maxX),
            Mathf.Clamp(_target.position.y, _minY, _maxY),
            transform.position.z
        );

        // ääÇÁÇ©Ç…í«è]
        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPos,
            ref _velocity,
            _smoothTime
        );
    }
}
