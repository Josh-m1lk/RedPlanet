using Unity.VisualScripting;
using UnityEngine;

public class EnemyEars : MonoBehaviour
{
    public bool heardSomething = false;
    public Vector3 lastHeardSound;
    public SoundType lastHeardType;
    public float hearingRadius = 15f;

    void OnEnable() => SoundManager.OnSoundEmitted += HandleSound;
    void OnDisable() => SoundManager.OnSoundEmitted -= HandleSound;

    public void HandleSound(Vector3 soundPos, float soundRadius, SoundType type)
    { 
        float distanceToSound = Vector3.Distance(transform.position, soundPos);
        if (distanceToSound <= soundRadius && distanceToSound <= hearingRadius)
        {
            Debug.Log("I heard something");
            heardSomething = true;
            lastHeardSound = soundPos;
            lastHeardType = type;
        }
    }
}
