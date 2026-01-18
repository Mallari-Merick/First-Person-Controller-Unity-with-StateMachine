using UnityEngine;

public class StateChecks : MonoBehaviour
{
    [Header("Ground Check")]
    [SerializeField] Transform groundCheck;
    [SerializeField] float groundCheckRadius = 0.2f;
    [SerializeField] LayerMask groundLayer;

    void OnDrawGizmosSelected()
    {
        if(groundCheck == null) return;

        Gizmos.color = Color.green;
        // GroundCheck
        Gizmos.DrawWireSphere(groundCheck.position, groundCheckRadius);
    }
    public bool IsCrouching(PlayerInputMap inputActions)
    {
        return inputActions.Player.Crouch.IsPressed();
    }
    public bool IsGrounded()
    {
        return Physics.CheckSphere(
            groundCheck.position, 
            groundCheckRadius, 
            groundLayer
            );
    }
    public bool IsSprinting(PlayerInputMap inputActions)
    {
        return inputActions.Player.Sprint.IsPressed();
    }
}