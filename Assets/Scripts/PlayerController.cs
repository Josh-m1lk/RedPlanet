//using System.Numerics;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    public float speed = 0f;
    private Vector2 move, mouseLook;
    private Vector3 rotationTarget;

    private Rigidbody rb;
    public bool isPc = true;

    public void OnMove(InputAction.CallbackContext context)
    {
        move = context.ReadValue<Vector2>();
    }

    public void OnMouseLook(InputAction.CallbackContext context)
    {
        mouseLook = context.ReadValue<Vector2>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();

        if (isPc)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(mouseLook);

            if (Physics.Raycast(ray, out hit))
            {
                rotationTarget = hit.point;
            }
            playerLook();
        }
    }

    public void MovePlayer()
    {
        Vector3 movement = new Vector3(move.x, 0f, move.y);

        transform.Translate(movement * speed * Time.deltaTime, Space.World);
    }

    public void playerLook()
    {
        var lookPos = rotationTarget - transform.position;
        lookPos.y = 0;
        var rotation = Quaternion.LookRotation(lookPos);

        Vector3 aimDirection = new Vector3(rotationTarget.x, 0f, rotationTarget.y);

        if (aimDirection != Vector3.zero)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.15f);
        }
    }
}
