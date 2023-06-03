using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MechController : MonoBehaviour
{
    public Transform bulletSpawnPoint;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10f;
    public float fireRate = 1f;
    public float range = 10f;

    private Transform playerTransform;
    private float timeSinceLastFire;

    // Start is called before the first frame update
    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        timeSinceLastFire = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        // Comprueba si el jugador está dentro del rango de disparo
        if (Vector3.Distance(transform.position, playerTransform.position) <= range)
        {
            // Actualiza el temporizador de disparo
            timeSinceLastFire += Time.deltaTime;

            // Dispara si el temporizador de disparo ha alcanzado el límite
            if (timeSinceLastFire >= fireRate)
            {
                Fire();
                timeSinceLastFire = 0f;
            }
        }
    }

    void Fire()
    {
        // Instancia un objeto de bala y le da velocidad en la dirección del jugador
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector3 bulletDirection = (playerTransform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = bulletDirection * bulletSpeed;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si la colisión ocurrió con la bala del jugador y destruye el mecha si es así
        if (other.gameObject.CompareTag("Bala"))
        {
            Destroy(gameObject);
            Destroy(other.gameObject); // Agregado para destruir la bala
        }
    }
}
