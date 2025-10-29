using UnityEngine;

public class PlayerStage : MonoBehaviour
{
    [field: SerializeField] public PlayerMovementStage CurrentPlayerMovementState { get; private set; } = PlayerMovementStage.Idling;
    public enum PlayerMovementStage
    {
        Idling = 0,
        Walking = 1,
        Running = 2,
        Sprinting = 3,
        Jumping = 4,
        Falling = 5,
        Strafing = 6,
    }
}
