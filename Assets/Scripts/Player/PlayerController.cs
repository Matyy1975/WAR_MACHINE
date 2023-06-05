using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject explosionEffect; // efecto de explosión cuando el jugador colisiona

    public float speed = 5f;
    public float horizontalLimit = 2.5f;
    public float verticalLimit = 4.5f;
    public int score;
    public GameObject bulletPrefab; // Referencia al prefab de la bala
    public float bulletSpeed = 10f; // Velocidad a la que se mueve la bala

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            // Crea una instancia de la bala en la posición del jugador
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            // Obtener el componente Rigidbody2D de la bala y establecer su velocidad
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(0, bulletSpeed);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        // Limit the player's movement to a certain area
        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -horizontalLimit, horizontalLimit);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -verticalLimit, verticalLimit);
        transform.position = clampedPosition;

        // Move the player based on input
        rb.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("avenemy"))
        {
            // Instancia un efecto de explosión en la posición del jugador
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            gameObject.SetActive(false); // Desactivar el jugador en vez de destruirlo

            Destroy(col.gameObject); // destruir el avión enemigo
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            Destroy(col.gameObject);
            gameObject.SetActive(false);
        }
        else if (col.gameObject.CompareTag("Portal"))
        {
            
        }
        else if (col.gameObject.CompareTag("Bullet"))
        {
            // Destruye la bala
            Destroy(col.gameObject);

            // Desactiva el componente PlayerController del jugador
            GetComponent<PlayerController>().enabled = false;

            // Desactiva el objeto del jugador
            gameObject.SetActive(false);
        }
    }






}
