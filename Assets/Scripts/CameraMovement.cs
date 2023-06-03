using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de la cámara
    public float slowDownPoint = 50f;  // Punto donde la cámara disminuirá la velocidad
    public float slowDownTime = 5f;  // Tiempo en que la velocidad disminuirá

    private float currentSpeed;  // Velocidad actual de la cámara
    private bool slowingDown = false;  // Indicador de si la cámara está disminuyendo la velocidad

    void Start()
    {
        currentSpeed = speed;  // Inicializar la velocidad actual de la cámara
    }

    void Update()
    {
        // Mueve la cámara hacia arriba con su velocidad actual
        transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);

        // Si la cámara llega al punto de ralentización, disminuye su velocidad
        if (transform.position.y >= slowDownPoint && !slowingDown)
        {
            slowingDown = true;
            currentSpeed /= 2f;  // Reducir la velocidad actual a la mitad

            // Establecer un temporizador para restaurar la velocidad original después de un tiempo
            StartCoroutine(RestoreSpeedAfterDelay());
        }
    }

    IEnumerator RestoreSpeedAfterDelay()
    {
        yield return new WaitForSeconds(slowDownTime);
        currentSpeed *= 2f;  // Restaurar la velocidad original
        slowingDown = false;
    }
}
