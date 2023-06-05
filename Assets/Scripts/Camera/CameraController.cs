using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 2.0f;

    void Update()
    {
        // Mueve la cámara hacia arriba a una velocidad determinada
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
