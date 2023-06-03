using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class avenemy : MonoBehaviour
{
    public float velocidadMovimiento = 5.0f;
    public float speed = 5f;
    public float fireRate = 2f; // Tasa de disparo del enemigo
    public float velocidadBala = 5f; // Velocidad de la bala
    public Rigidbody2D rb;
    public GameObject balaPrefab; // Prefab de la bala

    private bool playerIsDead = false;

    void Start()
    {
        rb.velocity = transform.up * speed;
        InvokeRepeating("Disparar", fireRate, fireRate);
    }

    void Update()
    {
        if (!playerIsDead)
        {
            transform.Translate(Vector2.down * velocidadMovimiento * Time.deltaTime);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bala"))
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            playerIsDead = true;
            col.gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            StartCoroutine(RestartLevel());
        }
    }

    IEnumerator RestartLevel()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void Disparar()
    {
        if (balaPrefab == null) return;
        GameObject bala = Instantiate(balaPrefab, transform.position, Quaternion.identity);
        bala.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -velocidadBala);
    }

}
