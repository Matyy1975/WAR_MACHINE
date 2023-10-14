using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public int damage = 10; // Cantidad de daño que hace la bala

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Accede al script del jugador y le hace daño
            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                player.RecibirDanio(damage);

                // Comprueba si la vida del jugador ha llegado a cero o menos y lo destruye
                if (player.GetVidaActual() <= 0)
                {
                    Destroy(collision.gameObject);
                }
            }

            // Finalmente, destruye la bala
            Destroy(gameObject);
        }
    }
}
