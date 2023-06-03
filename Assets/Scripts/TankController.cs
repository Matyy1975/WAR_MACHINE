using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankController : MonoBehaviour
{
    public float moveSpeed = 5f; // Velocidad de movimiento del tanque
    public float rotationSpeed = 10f; // Velocidad de rotación del tanque
    public float cannonRange = 5f; // Radio de disparo del cañón
    public GameObject bulletPrefab; // Prefab de la bala del tanque
    public Transform cannonTransform; // Transform del cañón del tanque
    public GameObject player; // Referencia al objeto del jugador

    private bool isPlayerInSight = false; // Bandera para indicar si el jugador está en el radio de disparo
    private bool isFiring = false; // Bandera para indicar si el tanque está disparando
    private float lastFireTime; // Tiempo del último disparo

    // Update is called once per frame
    void Update()
    {
        // Calcula la distancia entre el tanque y el jugador
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

        // Si el jugador está dentro del radio de disparo del tanque
        if (distanceToPlayer <= cannonRange)
        {
            isPlayerInSight = true;

            // Rota el cañón del tanque para apuntar al jugador
            Vector3 directionToPlayer = player.transform.position - cannonTransform.position;
            float angleToPlayer = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 270f;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, angleToPlayer);
            cannonTransform.rotation = Quaternion.RotateTowards(cannonTransform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Dispara la bala si ha pasado suficiente tiempo desde el último disparo
            if (Time.time - lastFireTime > 2f)
            {
                isFiring = true;
                lastFireTime = Time.time;
                GameObject bullet = Instantiate(bulletPrefab, cannonTransform.position, cannonTransform.rotation);
                Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
                bulletRb.AddForce(cannonTransform.up * 500f);
                Destroy(bullet, 5f);
            }
        }
        else
        {
            isPlayerInSight = false;
        }

        // Si la bala del tanque ha golpeado al jugador, desactiva al jugador
        if (isFiring && distanceToPlayer <= 1f)
        {
            isFiring = false;
            player.SetActive(false);
        }
    }
}
