using UnityEngine;

public class ResourceScript : MonoBehaviour
{
    public int resourcesCollected = 0;
     AudioSource resourceHum;

    void OnCollisionEnter(Collision collision)
    {
            print("Picked up resource");
            Destroy(gameObject);
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        resourceHum.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
