using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float speed = 0f;
    [SerializeField] float rotationSpeed = 0f;
    private const float maxInputMagnitude = 1f;
    public float footStepRadius = 5f;

    [Header("Input")]
    private Vector2 moveInput, mouseLook;

    [Header("References")]
    private Rigidbody rb;
    private Vector3 rotationTarget;

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();//Call to move input actions
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();//Call to mouse pointer position
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        MovePlayer();
        PlayerLook();
    }

    void Update()
    {
        MouseLook();
    }

    private void MovePlayer()
    {
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);//Get player input
        movement = Vector3.ClampMagnitude(movement, maxInputMagnitude);//Limit player movement to maximum input

        if (movement == Vector3.zero) return;//If player is not moving stop func

        Vector3 movementDirection = movement.normalized;//Figure out direction player wants to move
        float moveDistance = speed * Time.fixedDeltaTime;//Figure out how far player wants to move this frame

        if (rb.SweepTest(movementDirection, out RaycastHit hit, moveDistance))//if there is a wall in movement direction
        {
            movement = Vector3.ProjectOnPlane(movement, hit.normal);//Change movement so player slides along the wall
        }

        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);//Move the player
        FootStepSound();
    }

    private void FootStepSound()
    {
        SoundManager.EmitSound(transform.position, footStepRadius, SoundType.Footstep);
    }

    private void PlayerLook()
    {
        Vector3 lookDirection = rotationTarget - rb.position;//calculate direction from player to mouse
        lookDirection.y = 0;//ignore any Y rotation

        if (lookDirection == Vector3.zero) return;//If not look direction stop this func

        Quaternion targetRotation = Quaternion.LookRotation(lookDirection);//Calc rotation needed to face the mouse
        Quaternion newRotation = Quaternion.RotateTowards(rb.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);//Smoothily rotate to target

        rb.MoveRotation(newRotation);//Apply the new rotation to player
    }

    private void MouseLook()
    {
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);//Create invisible ground plane
        Ray ray = Camera.main.ScreenPointToRay(mouseLook);//Shoot ray from camera to mouse 

        if (groundPlane.Raycast(ray, out float enter))//Does ray hit ground plane
        {
            rotationTarget = ray.GetPoint(enter);//Store point on the ground where mouse points
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, footStepRadius);
    }
}
