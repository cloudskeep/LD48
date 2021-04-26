using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Transform target;
    public static GameObject cameraRef;
    public float smoothing;
    public Vector3 targetPosition;
    public Vector2 maxPosition;
    public Vector2 minPosition;
    public Vector2 area2Min;
    public Vector2 area2Max;
    public Vector2 area3Min;
    public Vector2 area3Max;

    // Start is called before the first frame update
    void Start()
    {
        cameraRef = GameObject.Find("CameraRef");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!CameraShake.CameraShaking)
        {
            targetPosition = new Vector3(target.position.x, target.position.y, transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x, maxPosition.x);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y, maxPosition.y);

            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);
        }
        else
        {
            targetPosition = new Vector3(target.position.x, target.position.y, cameraRef.transform.position.z);

            targetPosition.x = Mathf.Clamp(targetPosition.x, minPosition.x - 0.1f, maxPosition.x + 0.1f);
            targetPosition.y = Mathf.Clamp(targetPosition.y, minPosition.y - 0.1f, maxPosition.y + 0.1f);

            cameraRef.transform.position = Vector3.Lerp(cameraRef.transform.position, targetPosition, smoothing);
        }
    }

    public void FixCam(int area)
    {
        if (area == 1 || area == 0) transform.position = new Vector3(cameraRef.transform.position.x, cameraRef.transform.position.y, -10);
        if (area == 2)
        {
            minPosition = area2Min;
            maxPosition = area2Max;
        }
        else if (area == 3)
        {
            minPosition = area3Min;
            maxPosition = area3Max;
        }
    }
}
