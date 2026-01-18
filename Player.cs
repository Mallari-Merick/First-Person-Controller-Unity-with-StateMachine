using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("References / Player Information")]
    [SerializeField] Transform playerMeshTransform;
    CapsuleCollider capsuleCollider;
    float originalHeight;
    float crouchHeight;
    StateChecks stateChecks;
    StateMachine stateMachine;
    public PlayerInputMap inputActions {get; private set;}
    private Rigidbody rb;
    [Header("Player States")]
    public Player_CrouchState crouchState {get; private set;}
    public Player_IdleState idleState {get; private set;}
    public Player_JumpState jumpState {get; private set;}
    public Player_MoveState moveState {get; private set;}
    [Header("Crouch Settings")]
    public float crouchHeightMultiplier = 0.5f;
    [Header("Movement Settings")]
    public float inAirSpeedMultiplier;
    public float crouchSpeedMultiplier = 0.5f;
    public Vector2 moveVector {get; private set;}
    public float jumpHeight = 12f;
    public float moveSpeed = 12f;
    public float sprintSpeed = 16f;
    [Header("Camera Settings")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity;
    public Vector2 mouseVector {get; private set;}
    float xRotation;
    

    void Awake()
    {
        stateMachine = new StateMachine();
        inputActions = new PlayerInputMap();
        rb = GetComponent<Rigidbody>();
        stateChecks = GetComponentInChildren<StateChecks>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        originalHeight = capsuleCollider.height;
        crouchHeight = originalHeight * crouchHeightMultiplier;

        crouchState = new Player_CrouchState(this, rb, stateMachine, "crouch", stateChecks);
        idleState = new Player_IdleState(this, rb, stateMachine, "idle", stateChecks);
        jumpState = new Player_JumpState(this, rb, stateMachine, "jump", stateChecks);
        moveState = new Player_MoveState(this, rb, stateMachine, "move", stateChecks);
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.Initialize(idleState);
    }
    void Update()
    {
        stateMachine.UpdateCurrentState();
        HandleJumping();
    }
    void FixedUpdate()
    {
        HandleCameraMovement();
        stateMachine?.FixedUpdateCurrentState();
    }
    void OnEnable()
    {
        inputActions.Enable();
        inputActions.Player.Movement.performed += context => moveVector = context.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += context => moveVector = Vector2.zero;
    }
    void OnDisable()
    {
        inputActions.Disable();
    }

    public void SetVelocity(Vector3 movement, float speed)
    {
        rb.linearVelocity = new Vector3(
            movement.x * speed,
            rb.linearVelocity.y,
            movement.z * speed
        );
    }
    public void SetCrouchHeight(bool crouch)
    {
        if (crouch)
        {
            // Scale collider
            capsuleCollider.height = crouchHeight;
            capsuleCollider.center = new Vector3(0, crouchHeight/2f, 0);

            // Scale visual mesh
            if(playerMeshTransform != null)
                playerMeshTransform.localScale = new Vector3(1f, crouchHeightMultiplier, 1f);
        }
        else
        {
            // Scale Collider
            capsuleCollider.height = originalHeight;
            capsuleCollider.center = new Vector3(0, originalHeight/2f, 0);
            
            // Scale visual mesh
            if(playerMeshTransform != null)
                playerMeshTransform.localScale = Vector3.one;
        }
    }

    void HandleCameraMovement()
    {
        mouseVector = inputActions.Player.Look.ReadValue<Vector2>();
        float mouseX = mouseVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseVector.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0,0);
    }
    // State Checkers
    public bool HandleJumping()
    {
        return inputActions.Player.Jump.IsPressed();
    }

    // Helpers
    public Vector3 GetCameraRelativeMovement(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();
        Vector3 movement = (forward * input.y) + (right * input.x);
        return movement;
    }
}