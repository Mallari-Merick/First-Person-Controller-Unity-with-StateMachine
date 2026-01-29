using UnityEngine;

public class Player : MonoBehaviour
{
    StateMachine stateMachine;
    Rigidbody rb;
    StateChecks stateChecks;
    public PlayerInputMap inputActions {get; private set;}
    [Header("Camera Variables")]
    [SerializeField] Transform Transform_cameraTransform;
    public Transform cameraTransform => Transform_cameraTransform;
    [SerializeField] float mouseSensitivity;
    public Vector2 mouseVector {get; private set;}
    float xRotation;

    [Header("Player States")]
    public Player_DashState dashState {get; private set;}
    // Player Ground States
    public Player_IdleState idleState {get; private set;}
    public Player_MoveState moveState {get; private set;}
    // Player Air States
    public Player_JumpState jumpState {get; private set;}
    public Player_FallState fallState {get; private set;}
    public Player_SlamState slamState {get; private set;}
    [Header ("Gravity")]
    public float gravityMultiplier = 2f;
    [Header("Movement Settings")]
    [Range(5, 100)]
    public float movementSpeed;
    [Range(0,1)]
    public float airStrafeMultiplier = 0.7f;
    public float groundAcceleration = 5f;
    public float airAcceleration =2f;
    public float jumpHeight = 12f;
    public float slamSpeed = 40f;
    [Header("Dash Settings")]
    public float dashSpeed = 40f;
    public float dashDuration = 0.2f;
    public float dashLength = 5f;
    public Vector2 moveVector {get; private set;}
    void Awake()
    {
        stateChecks = GetComponentInChildren<StateChecks>();
        rb = GetComponent<Rigidbody>();

        stateMachine = new StateMachine();
        inputActions = new PlayerInputMap();

        dashState = new Player_DashState(this, stateMachine, "dash", rb, stateChecks);

        // Ground State
        idleState = new Player_IdleState(this, stateMachine, "idle", rb, stateChecks);
        moveState = new Player_MoveState(this, stateMachine, "move", rb, stateChecks);

        // Air State
        jumpState = new Player_JumpState(this, stateMachine, "jumpFall", rb, stateChecks); //They have the same State name because of the animation is supposed to go on a blend tree
        fallState = new Player_FallState(this, stateMachine, "jumpFall", rb, stateChecks);
        slamState = new Player_SlamState(this, stateMachine, "slam", rb, stateChecks);
    }

    void Start()
    {
        stateMachine.Initialize(idleState);
        rb.useGravity = false; // We'll use our own Gravity
        Cursor.lockState = CursorLockMode.Locked;
    }
    void Update()
    {
        stateMachine.UpdateCurrentState();
    }
    void FixedUpdate()
    {
        HandleCameraMovement();
        rb.AddForce(Physics.gravity * gravityMultiplier, ForceMode.Acceleration); // Custom Gravity setting just for player
        stateMachine?.FixedUpdateCurrentState();
    }

    void HandleCameraMovement()
    {
        mouseVector = inputActions.Player.Look.ReadValue<Vector2>();
        float mouseX = mouseVector.x * mouseSensitivity * Time.deltaTime;
        float mouseY = mouseVector.y * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0, 0);
    }

    void OnEnable() 
    {
        inputActions.Enable();
        inputActions.Player.Movement.performed += context =>moveVector = context.ReadValue<Vector2>();
        inputActions.Player.Movement.canceled += context => moveVector = Vector2.zero;
    }
    void OnDisable()
    {
        inputActions.Disable();
    }

    public void SetVelocityXZ(Vector3 movement, float speed, float acceleration)
    {
        Vector3 currentHorizontal = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z);
        Vector3 targetVelocity = movement * speed;

        Vector3 newHorizontal = Vector3.MoveTowards(
            currentHorizontal,
            targetVelocity,
            acceleration
        );

        rb.linearVelocity = new Vector3(newHorizontal.x, rb.linearVelocity.y, newHorizontal.z);
    }

    public Vector3 GetCameraRelativeMovement(Vector2 input)
    {
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        Vector3 movement = (forward*input.y) + (right * input.x);
        return movement;
    }

}
