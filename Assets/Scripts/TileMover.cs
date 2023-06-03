using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TileMover : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Vector3 stopPosition;
    public bool isMoving = false;

    private void Update()
    {
        if (isMoving && transform.position.y > stopPosition.y)
        {
            transform.position -= new Vector3(0, moveSpeed * Time.deltaTime, 0);
        }
    }
}
