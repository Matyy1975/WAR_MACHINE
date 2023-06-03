using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float speed = 5f;  // Velocidad de la c�mara
    public float slowDownPoint = 50f;  // Punto donde la c�mara disminuir� la velocidad
    public float slowDownTime = 5f;  // Tiempo en que la velocidad disminuir�

    private float currentSpeed;  // Velocidad actual de la c�mara
    private bool slowingDown = false;  // Indicador de si la c�mara est� disminuyendo la velocidad

    void Start()
    {
        currentSpeed = speed;  // Inicializar la velocidad actual de la c�mara
    }

    void Update()
    {
        // Mueve la c�mara hacia arriba con su velocidad actual
        transform.Translate(Vector2.up * currentSpeed * Time.deltaTime);

        // Si la c�mara llega al punto de ralentizaci�n, disminuye su velocidad
        if (transform.position.y >= slowDownPoint && !slowingDown)
        {
            slowingDown = true;
            currentSpeed /= 2f;  // Reducir la velocidad actual a la mitad

            // Establecer un temporizador para restaurar la velocidad original despu�s de un tiempo
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
