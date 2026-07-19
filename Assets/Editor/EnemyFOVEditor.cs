using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EnemyFOV))]
public class EnemyFOVEditor : Editor
{
    public void OnSceneGUI()
    {
        EnemyFOV enemyFOV = (EnemyFOV)target;//grab the target

        //Draw detecion radius
        Handles.color = Color.white;
        Handles.DrawWireArc(enemyFOV.transform.position, Vector3.up, Vector3.forward, 360, enemyFOV.radius);

        //Triangle lines for both sides of triangle
        Vector3 viewAngle1 = DirectionFromAngle(enemyFOV.transform.eulerAngles.y, -enemyFOV.angle / 2);
        Vector3 viewAngle2 = DirectionFromAngle(enemyFOV.transform.eulerAngles.y, enemyFOV.angle / 2);

        //Draw the vision cone lines 
        Handles.color = Color.yellow;
        Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngle1 * enemyFOV.radius);
        Handles.DrawLine(enemyFOV.transform.position, enemyFOV.transform.position + viewAngle2 * enemyFOV.radius);

        if(enemyFOV.canSeePlayer)
        {
            //If player is seen draw a line towards it
            Handles.color = Color.red;
            Handles.DrawLine(enemyFOV.transform.position, enemyFOV.playerRef.transform.position);
        }
    }

    private Vector3 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees += eulerY;

        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}
