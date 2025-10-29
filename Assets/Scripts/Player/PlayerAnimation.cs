using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private Animator animator;

    private PlayerLocalInput _playerLocalInput;


    private static readonly int inputXHash = Animator.StringToHash("InputX");
    private static readonly int inputYHash = Animator.StringToHash("InputY");

    [Header("Animation Smoothing")]
    [Tooltip("Thời gian để các tham số input animation chuyển đổi mượt mà.")]
    [SerializeField]
    private float animationSmoothTime = 0.15f; 


    private float currentInputX;
    private float currentInputY;


    private float inputXVelocity;
    private float inputYVelocity;

    private void Awake()
    {
        _playerLocalInput = GetComponent<PlayerLocalInput>();
        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }
        
        currentInputX = _playerLocalInput.MovementInput.x;
        currentInputY = _playerLocalInput.MovementInput.y;
    }

    private void Update()
    {
        UpdateAnimationStage();
    }

    private void UpdateAnimationStage()
    {
        Vector2 inputTarget = _playerLocalInput.MovementInput;

        currentInputX = Mathf.SmoothDamp(currentInputX, inputTarget.x, ref inputXVelocity, animationSmoothTime);
        currentInputY = Mathf.SmoothDamp(currentInputY, inputTarget.y, ref inputYVelocity, animationSmoothTime);

        animator.SetFloat(inputXHash, currentInputX);
        animator.SetFloat(inputYHash, currentInputY);
    }
}