using UnityEngine;

public class StateChecks : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] float groundRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    bool isGrounded;
    bool isCrouching;
    void OnDrawGizmosSelected()
    {
        if(groundCheck == null) return;

        Gizmos.color = Color.green;
        // GroundCheck
        Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
    public bool IsGrounded()
    {
        return isGrounded = Physics.CheckSphere(
            groundCheck.position,
            groundRadius,
            groundLayer
        );
    }
    public bool IsCrouching(PlayerInputMap inputActions)
    {
        isCrouching = inputActions.Player.Crouch.IsPressed();
        return isCrouching;
    }
}

