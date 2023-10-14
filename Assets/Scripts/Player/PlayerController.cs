using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject explosionEffect;
    public float speed = 5f;
    public float horizontalLimit = 2.5f;
    public float verticalLimit = 4.5f;
    public int score;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;

    public int vidaMaxima = 100;
    private int vidaActual;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent <Rigidbody2D>();
        vidaActual = vidaMaxima;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D bulletRb = bullet.GetComponent<Rigidbody2D>();
            bulletRb.velocity = new Vector2(0, bulletSpeed);
        }

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 movement = new Vector2(horizontalInput, verticalInput);

        Vector3 clampedPosition = transform.position;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, -horizontalLimit, horizontalLimit);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, -verticalLimit, verticalLimit);
        transform.position = clampedPosition;

        rb.velocity = movement * speed;
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("avenemy"))
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);

            // Reducir la vida del jugador cuando colisiona con un enemigo
            TomarDanio(10); // Puedes ajustar la cantidad de daño según tus necesidades

            if (vidaActual <= 0)
            {
                // El jugador se queda sin vida, puedes agregar aquí lo que deseas hacer en caso de derrota.
                gameObject.SetActive(false);
            }

            Destroy(col.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("enemy"))
        {
            Destroy(col.gameObject);

            TomarDanio(5); // Reducir la vida cuando colisiona con un enemigo

            if (vidaActual <= 0)
            {
                // El jugador se queda sin vida, puedes agregar aquí lo que deseas hacer en caso de derrota.
                gameObject.SetActive(false);
            }
        }
    }

    void TomarDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Max(0, vidaActual);
    }
    public int GetVidaActual()
    {
        return vidaActual;
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        vidaActual = Mathf.Max(0, vidaActual);
    }
}
