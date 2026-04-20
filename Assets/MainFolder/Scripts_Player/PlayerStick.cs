using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerStick : MonoBehaviour
{
    [SerializeField] private InputAction _inputMover;

    private Vector2 _movementValue;
    public float nowDirection;


    private void OnEnable()
    {
        _inputMover.Enable();
    }

    private void OnDisable()
    {
        _inputMover.Disable();
    }

    private void Update()
    {
        //“ü—Í
        _movementValue = _inputMover.ReadValue<Vector2>();

        //Œ»Ý‚Ì•ûŒü‚ðPlayerMovement‚É“n‚·
        if (Mathf.Abs(_movementValue.x) > 0.1f)
        {
            nowDirection = Mathf.Sign(_movementValue.x);
        }
        else
        {
            nowDirection = 0f;
        }
    }
}
