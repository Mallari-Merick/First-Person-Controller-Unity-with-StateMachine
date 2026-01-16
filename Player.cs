using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Information")]
    [SerializeField] Transform playerHead;
    [SerializeField] Transform playerCapsule;
    public StateChecks stateChecks {get; private set;}
    private PlayerInputMap inputActions;
    private StateMachine stateMachine;
    private Rigidbody rb;
    [Header("Player States")]
    public Player_IdleState idleState {get; private set;}
    public Player_MoveState moveState {get; private set;}
    public Player_CrouchState crouchState {get; private set;}
    public Player_JumpState jumpState {get; private set;}
    [Header("Mouse Settings")]
    [SerializeField] Transform cameraTransform;
    [SerializeField] float mouseSensitivity = 10f;
    public float jumpHeight = 12f;
    public Vector2 mouseVector {get; private set;}
    float xRotation;

    [Header("Movement Settings")]
    public Vector2 moveVector {get; private set;}
    public float moveSpeed = 12f;
    public float crouchSpeedMultiplier = 0.5f;
    public float inAirSpeedMultiplier = 0.3f;

    void Awake()
    {
        inputActions = new PlayerInputMap();
        stateMachine = new StateMachine();
        rb = GetComponent<Rigidbody>();
        stateChecks = GetComponentInChildren<StateChecks>();

        idleState = new Player_IdleState(this, rb, stateMachine, "idle");
        moveState = new Player_MoveState(this, rb, stateMachine, "move");
        crouchState = new Player_CrouchState(this, rb, stateMachine, "crouch");
        jumpState = new Player_JumpState(this, rb, stateMachine, "jump");
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        stateMachine.Initialize(idleState);
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
    void Update()
    {
        stateMachine.UpdateCurrentState();
        HandleCameraMovement();
        HandleCrouching();
        HandleJumping();
    }
    void FixedUpdate()
    {
        stateMachine?.FixedUpdateCurrentState();
    }
    public void SetVelocityXZ(Vector3 movement, float speed)
    {
        rb.linearVelocity = new Vector3(
            movement.x * speed,
            rb.linearVelocity.y,
            movement.z * speed
        );
    }
    void HandleCameraMovement()
    {
        mouseVector = inputActions.Player.Look.ReadValue<Vector2>();
        float mouseX = mouseVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseVector.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f,90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0,0);
    }
    // ===============================
    // State checkers
    // ===============================
    public bool HandleCrouching()
    {
        return stateChecks.IsCrouching(inputActions);
    }
    public bool HandleJumping()
    {
        return inputActions.Player.Jump.IsPressed();
    }
    // ===============================
    // Helpers
    // ===============================    
    public Vector3 GetCameraRelativeMovement(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Make the y 0 to prevent from moving upward
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();
        Vector3 movement = (forward * input.y) + (right * input.x);
        return movement;
    }
}