using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5.0f; // Velocidad de movimiento ajustable

    void Update()
    {
        // Mover el jugador hacia arriba
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
