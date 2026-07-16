using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float speed = 0f;

    [Header("Input")]
    private Vector2 moveInput, mouseLook;

    [Header("Rotation")]
    private Vector3 rotationTarget;

    [Header("References")]
    private Rigidbody rb;
    private Camera cam;

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
        cam = GetComponent<Camera>();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    // Update is called once per frame
    void Update()
    {
        MouseLook();
    }

    private void MovePlayer()
    {
        //Get player input and move based on input, speed, and direction.
        Vector3 movement = new Vector3(moveInput.x, 0f, moveInput.y);
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

    private void PlayerLook()
    {
        //Grabbing the rotation of player to target
        Vector3 lookDirection = rotationTarget - transform.position;
        lookDirection.y = 0;
        var rotation = Quaternion.LookRotation(lookDirection);

        //Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.y);//creating new vector3 directions 

        if (lookDirection != Vector3.zero)
        {
            //if aim direction exists face that way and smoothly turn player
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f * Time.deltaTime);
        }
    }

    private void MouseLook()
    {
        //Make ground plane and create a ray towards the mouse pointer
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = cam.ScreenPointToRay(mouseLook);

        //Player turn towards wherever ground plane is
        if (groundPlane.Raycast(ray, out float enter))
        {
            Vector3 hitPoint = ray.GetPoint(enter);
            rotationTarget = hitPoint;
        }
        PlayerLook();
    }
}
