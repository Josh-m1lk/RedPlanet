using Unity.Cinemachine;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Map : MonoBehaviour
{
    [SerializeField] private CinemachineFollow cameraFollow;
    [SerializeField, Min(1f)] private float mapZoomMultiplier = 2f;
    [SerializeField, Min(0f)] private float transitionSpeed = 5f;

    [SerializeField] PlayerController playerController;

    private Vector3 normalFollowOffset;
    public bool isMapView;

    public void OnMap(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isMapView = !isMapView;
        }
    }
    
    private void Awake()
    {
        if (cameraFollow == null)
        {
            cameraFollow = GetComponent<CinemachineFollow>();
        }

        if (cameraFollow == null)
        {
            Debug.LogError("Map requires a CinemachinFollow camer reference.",  this);
            enabled = false;
            return;
        }

        normalFollowOffset = cameraFollow.FollowOffset;
    }

    private void Update()
    {
        Vector3 targetOffset;

        if (isMapView)
        {
            targetOffset = normalFollowOffset * mapZoomMultiplier;
            //playerController.DisableInput();
        }
        else
        {
            targetOffset = normalFollowOffset;
            //playerController.EnableInput();
        }

        cameraFollow.FollowOffset = Vector3.Lerp(cameraFollow.FollowOffset, targetOffset, 1f - Mathf.Exp(-transitionSpeed * Time.deltaTime));
    }
}
