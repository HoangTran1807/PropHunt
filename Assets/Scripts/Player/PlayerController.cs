using UnityEngine;


[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private CharacterController m_CharacterController;
    [SerializeField]
    private Camera m_PlayerCamera;

    [Header("Movement setting")]
    [SerializeField]
    private float runAcceleration = 50f;
    [SerializeField]
    private float runSpeed = 4f;
    private float verticalVelocity = 0f;
    [SerializeField] private float gravity = -9.81f;
    [SerializeField] private float jumpForce = 5f;

    [Header("Camera setting")]
    [SerializeField]
    private float lookSenceH = 0.1f;
    [SerializeField]
    private float lookSenceV = 0.1f;
    [SerializeField]
    private float lookLimitV = 80f;


    
    private PlayerLocalInput m_PlayerLocalInput;
    private Vector2 _cameraRotation = Vector2.zero;

    private void Awake()
    {
        m_PlayerLocalInput = GetComponent<PlayerLocalInput>();
    }

    private void Update()
    {
        Vector3 cameraForwardXZ = new Vector3(m_PlayerCamera.transform.forward.x, 0f, m_PlayerCamera.transform.forward.z).normalized;
        Vector3 cameraRightXZ = new Vector3(m_PlayerCamera.transform.right.x, 0f, m_PlayerCamera.transform.right.z).normalized;
        Vector3 movementDirection = cameraRightXZ * m_PlayerLocalInput.MovementInput.x + cameraForwardXZ * m_PlayerLocalInput.MovementInput.y;

        Vector3 horizontalVelocity = new Vector3(m_CharacterController.velocity.x, 0f, m_CharacterController.velocity.z);
        Vector3 targetVelocity = movementDirection * runSpeed;
        Vector3 smoothVelocity = Vector3.MoveTowards(horizontalVelocity, targetVelocity, runAcceleration * Time.deltaTime);


        if (m_CharacterController.isGrounded)
        {
            if (verticalVelocity < 0f)
                verticalVelocity = -2f;

        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        Vector3 finalMove = smoothVelocity;
        finalMove.y = verticalVelocity;

        m_CharacterController.Move(finalMove * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        _cameraRotation.x += lookSenceH * m_PlayerLocalInput.LookInput.x;
        _cameraRotation.y = Mathf.Clamp(_cameraRotation.y - lookSenceV * m_PlayerLocalInput.LookInput.y, -lookLimitV, lookLimitV);

        transform.rotation = Quaternion.Euler(0f, _cameraRotation.x, 0f);
        m_PlayerCamera.transform.localRotation = Quaternion.Euler(_cameraRotation.y, 0f, 0f);

    }

}
