using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // El objeto que seguirá la cámara
    public float speed = 2.0f; // La velocidad a la que se moverá la cámara
    public float minY = 0.0f; // La posición Y mínima de la cámara
    public float slowDownHeight = 10.0f; // La altura a la que se disminuirá la velocidad
    public float slowDownTime = 5.0f; // El tiempo que durará la cuenta regresiva

    private bool isSlowingDown = false; // Indica si la cámara debe disminuir su velocidad
    private float slowDownTimer = 0.0f; // El tiempo restante en la cuenta regresiva

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            Vector3 newPos = transform.position;

            // Calcula la nueva posición de la cámara
            newPos.y = Mathf.Max(target.position.y, minY);
            newPos.x = target.position.x;

            // Verifica si la cámara debe disminuir su velocidad
            if (transform.position.y >= slowDownHeight && !isSlowingDown)
            {
                isSlowingDown = true;
                slowDownTimer = slowDownTime;
                speed /= 2.0f; // Disminuye la velocidad a la mitad
            }

            // Verifica si la cuenta regresiva está activa
            if (isSlowingDown)
            {
                slowDownTimer -= Time.deltaTime;

                // Si la cuenta regresiva termina, restablece la velocidad
                if (slowDownTimer <= 0.0f)
                {
                    isSlowingDown = false;
                    speed *= 2.0f; // Restablece la velocidad original
                }
            }

            // Interpola suavemente la posición actual de la cámara a la nueva posición
            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * speed);
        }
    }
}
