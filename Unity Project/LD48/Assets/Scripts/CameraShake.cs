using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    // Transform of the GameObject you want to shake
    private Transform trans;

    // Desired duration of the shake effect
    private float shakeDuration = 0f;

    // A measure of magnitude for the shake. Tweak based on your preference
    public float shakeMagnitude = 0.7f;

    // A measure of how quickly the shake effect should evaporate
    public float dampingSpeed = 1.0f;

    // The initial position of the GameObject
    Vector3 initialPosition;

    public static bool CameraShaking = false;

    void Awake()
    {
        if (transform == null)
        {
            trans = GetComponent<Transform>();
        }
    }

    //void OnEnable()
    //{
    //    initialPosition = transform.localPosition;
    //}

    void Update()
    {
        if (shakeDuration > 0)
        {
            transform.localPosition = new Vector2(CameraManager.cameraRef.transform.position.x, CameraManager.cameraRef.transform.position.y) + Random.insideUnitCircle * shakeMagnitude;
            transform.localPosition = new Vector3(transform.localPosition.x, transform.localPosition.y, -10);
            shakeDuration -= Time.deltaTime * dampingSpeed;

            CameraShaking = true;
        }
        else if (CameraShaking)
        {
            CameraShaking = false;
            shakeDuration = 0f;
            Camera.main.GetComponent<CameraManager>().FixCam(1);
            transform.localPosition = CameraManager.cameraRef.transform.position;
        }
    }

    public void TriggerShake()
    {
        initialPosition = transform.localPosition;
        shakeDuration = 2.0f;
    }
}
