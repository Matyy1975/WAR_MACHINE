using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto de salida del proyectil
    public float fireRate = 0.5f; // Intervalo de disparo ajustable
    public float bulletSpeed = 10f; // Velocidad del proyectil

    private float nextFireTime = 0.0f; // Tiempo para el próximo disparo

    void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Shoot();
            nextFireTime = Time.time + fireRate;
        }
    }

    void Shoot()
    {
        // Instanciar el proyectil en el punto de salida
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Agregar velocidad inicial al proyectil para que se mueva hacia adelante
        Rigidbody2D bulletRigidbody = bullet.GetComponent<Rigidbody2D>();
        bulletRigidbody.velocity = firePoint.up * bulletSpeed;
    }
}
