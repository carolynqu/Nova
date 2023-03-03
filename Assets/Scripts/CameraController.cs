using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
   
    public Transform target;
    public float speed;
    public float globalMaxX;
    public float globalMaxY;
    public float globalMinX;
    public float globalMinY;

    public float offsetX = 0;

    void Start()
    {

    }

    void Update()
    {

        Vector3 goal = target.position + new Vector3(0.0f, 0.0f, -10);
        float maxX = globalMaxX - Camera.main.orthographicSize * Camera.main.aspect;
        float maxY = globalMaxY - Camera.main.orthographicSize;
        float minX = globalMinX + Camera.main.orthographicSize * Camera.main.aspect;
        float minY = globalMinY + Camera.main.orthographicSize;
        goal.x = Mathf.Clamp(goal.x, minX, maxX);
        goal.y = Mathf.Clamp(goal.y, minY, maxY);
        transform.position = goal;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0.0f), new Vector3(globalMaxX, globalMinY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMaxY, 0.0f), new Vector3(globalMaxX, globalMaxY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMinX, globalMinY, 0.0f), new Vector3(globalMinX, globalMaxY, 0.0f));
        Gizmos.DrawLine(new Vector3(globalMaxX, globalMinY, 0.0f), new Vector3(globalMaxX, globalMaxY, 0.0f));
    }

}
