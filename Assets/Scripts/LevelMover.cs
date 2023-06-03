using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelMover : MonoBehaviour
{
    public GameObject tilemap;
    public float speed = 5f;
    public float levelLimit = -10f;
    public float stopTime = 2f; // Tiempo de parada en segundos
    public Vector3 stopPosition; // Posición donde la cámara se detendrá

    private bool isStopped = false;
    private float stopTimer = 0f;

    private void Update()
    {
        if (!isStopped)
        {
            Vector3 newPosition = tilemap.transform.position;
            newPosition.y -= speed * Time.deltaTime;
            tilemap.transform.position = newPosition;

            if (newPosition.y < levelLimit)
            {
                // Activar la condición de "Game Over" o reiniciar el nivel
            }

            if (newPosition.y <= stopPosition.y)
            {
                isStopped = true;
                stopTimer = stopTime;
            }
        }
        else
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer <= 0f)
            {
                isStopped = false;
            }
        }
    }
}
