using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLocalInput : MonoBehaviour, PlayerControls.IPlayerLocalMotionMapActions
{
    public PlayerControls PlayerControls {  get; private set; }
    public Vector2 MovementInput {  get; private set; }
    public Vector2 LookInput { get; private set; }

    

    private void OnEnable()
    {
        PlayerControls =  new PlayerControls();
        PlayerControls.Enable();

        PlayerControls.PlayerLocalMotionMap.Enable();
        PlayerControls.PlayerLocalMotionMap.SetCallbacks(this);
    }

    private void OnDisable()
    {
        PlayerControls.PlayerLocalMotionMap.Disable();
        PlayerControls.PlayerLocalMotionMap.RemoveCallbacks(this);
    }

    public void OnMovement(InputAction.CallbackContext context)
    {
        MovementInput = context.ReadValue<Vector2>();

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        LookInput = context.ReadValue<Vector2>();
    }
}
