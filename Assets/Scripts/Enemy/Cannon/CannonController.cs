using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    public GameObject explosionEffect; // Referencia al efecto de explosión
    public int vidaMaxima = 50; // Vida máxima del cañón
    private int vidaActual;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionRadius = 5f;

    private float lastShootTime;
    private float nextFireTime;
    private bool isPlayerInRange;
    private float levelSpeed;
    private float shootAngle;

    private void Start()
    {
        vidaActual = vidaMaxima;
    }

    private void Update()
    {
        Move();
        AimAndShoot();
    }

    private void Move()
    {
        transform.position += Vector3.down * levelSpeed * Time.deltaTime;
    }

    private void AimAndShoot()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < detectionRadius)
        {
            Vector3 directionToPlayer = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 270f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.AddForce(-transform.up * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    public void RecibirDanio(int cantidad)
    {
        vidaActual -= cantidad;
        if (vidaActual <= 0)
        {
            vidaActual = 0;
            // Aquí puedes agregar la lógica para destruir el cañón con un retraso
            // Utilizamos Invoke para programar la destrucción en 2 segundos, puedes ajustar el tiempo a tu preferencia.
            Invoke("DestruirCannon", 2.0f);
        }
    }

    private void DestruirCannon()
    {
        // Destruir el cañón
        Destroy(gameObject);
    }
}
