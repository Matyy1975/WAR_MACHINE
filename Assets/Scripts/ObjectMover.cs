using UnityEngine;

public class ObjectMover : MonoBehaviour
{
    public GameObject[] objectsToMove;
    public float speed = 5f;
    public float levelLimit = -10f;
    public float stopTime = 2f; // Tiempo de parada en segundos
    public Vector3 stopPosition; // Posición donde los objetos se detendrán

    private bool isStopped = false;
    private float stopTimer = 0f;

    private void Update()
    {
        if (!isStopped)
        {
            foreach (GameObject obj in objectsToMove)
            {
                Vector3 newPosition = obj.transform.position;
                newPosition.y -= speed * Time.deltaTime;
                obj.transform.position = newPosition;
            }

            if (objectsToMove.Length > 0 && objectsToMove[0].transform.position.y < levelLimit)
            {
                // Activar la condición de "Game Over" o reiniciar el nivel
            }

            if (objectsToMove.Length > 0 && objectsToMove[0].transform.position.y <= stopPosition.y)
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
