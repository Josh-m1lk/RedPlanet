using UnityEngine;

public class GrenadeFlashScaler : MonoBehaviour
{
    private float startScaleSetter;
    float scalerValue;
    public GameObject grenadeFlash;
    
    void Start()
    {
        grenadeFlash.transform.localScale = new Vector3(500.0f, 500.0f, 500.0f);
    }

    void Update()
    {
        
    }
}
