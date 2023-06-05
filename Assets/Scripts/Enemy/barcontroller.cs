using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class barcontroller : MonoBehaviour
{
    public GameObject explosionEffect; // Referencia al efecto de explosión

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float rotationSpeed = 1.0f;
    [SerializeField] private float fireRate = 2.0f;
    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private float detectionRadius = 5f;

    private float levelSpeed;
    private float nextFireTime;

    private void Start()
    {
        
    }

    private void Update()
    {
        Move();
        AimAndShoot();
    }

    private void Move()
    {
        transform.position += Vector3.down * levelSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0f);
    }

    private void AimAndShoot()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer < detectionRadius)
        {
            Vector3 directionToPlayer = playerTransform.position - firePoint.position;
            float angle = Mathf.Atan2(directionToPlayer.y, directionToPlayer.x) * Mathf.Rad2Deg - 270f;
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            firePoint.rotation = Quaternion.Slerp(firePoint.rotation, rotation, Time.deltaTime * rotationSpeed);

            if (Time.time >= nextFireTime)
            {
                nextFireTime = Time.time + 1f / fireRate;
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.identity);
                Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
                bulletRB.AddForce(directionToPlayer.normalized * bulletSpeed, ForceMode2D.Impulse);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Si el objeto "barcontroller" es alcanzado por una bala del jugador, destruir el objeto
            Destroy(gameObject);
            // Crear efecto de explosión
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
