using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class avenemy2 : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f; // Velocidad de movimiento del enemigo
    public float speed = 5f;
    public float radioDeteccion = 5f; // Radio de detección del jugador
    public Rigidbody2D rb;

    private Transform player; // Referencia al transform del jugador

    void Start()
    {
        rb.velocity = transform.up * speed;
        player = GameObject.FindGameObjectWithTag("Player").transform; // Obtener la referencia al jugador
    }


    void Update()
    {
        // Mover el enemigo hacia abajo
        transform.Translate(Vector2.down * velocidadMovimiento * Time.deltaTime);

        // Verificar si el jugador está dentro del radio de detección
        if (Vector2.Distance(transform.position, player.position) < radioDeteccion)
        {
            // Mover el enemigo hacia el jugador
            transform.position = Vector2.MoveTowards(transform.position, player.position, velocidadMovimiento * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bala"))//Si el objeto con el TAG "BALA" choca con el enemigo
        {
            Destroy(other.gameObject); // destruir el enemigo
            Destroy(gameObject); // destruir la bala
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Destroy(col.gameObject);
            Destroy(gameObject);
        }
    }
}
