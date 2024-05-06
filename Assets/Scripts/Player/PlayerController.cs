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

    public KeyCode moveLeftKey = KeyCode.LeftArrow;
    public KeyCode moveRightKey = KeyCode.RightArrow;
    public KeyCode moveUpKey = KeyCode.UpArrow;
    public KeyCode moveDownKey = KeyCode.DownArrow;


    public GameObject objetoIzquierda;
    public GameObject objetoDerecha;
    public GameObject objetoArriba;
    public GameObject objetoAbajo;


    private bool moviendoIzquierda;
    private bool moviendoDerecha;
    private bool moviendoArriba;
    private bool moviendoAbajo;
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

        float horizontalInput = 0f;
        float verticalInput = 0f;

        if (Input.GetKey(moveLeftKey))
        {
            horizontalInput = -1f;
        }
        else if (Input.GetKey(moveRightKey))
        {
            horizontalInput = 1f;
        }

        if (Input.GetKey(moveDownKey))
        {
            verticalInput = -1f;
        }
        else if (Input.GetKey(moveUpKey))
        {
            verticalInput = 1f;
        }

        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized * speed * Time.deltaTime;

        rb.MovePosition(rb.position + movement);






        if (horizontalInput < 0)
        {
            objetoIzquierda.SetActive(true);
            objetoDerecha.SetActive(false);
            moviendoIzquierda = true;
            moviendoDerecha = false;
        }
        else if (horizontalInput > 0)
        {
            objetoDerecha.SetActive(true);
            objetoIzquierda.SetActive(false);
            moviendoDerecha = true;
            moviendoIzquierda = false;
        }
        else
        {
            objetoIzquierda.SetActive(false);
            objetoDerecha.SetActive(false);
            moviendoIzquierda = false;
            moviendoDerecha = false;
        }

        if (verticalInput < 0)
        {
            objetoAbajo.SetActive(true);
            objetoArriba.SetActive(false);
            moviendoAbajo = true;
            moviendoArriba = false;
        }
        else if (verticalInput > 0)
        {
            objetoArriba.SetActive(true);
            objetoAbajo.SetActive(false);
            moviendoArriba = true;
            moviendoAbajo = false;
        }
        else
        {
            objetoAbajo.SetActive(false);
            objetoArriba.SetActive(false);
            moviendoAbajo = false;
            moviendoArriba = false;
        }
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
