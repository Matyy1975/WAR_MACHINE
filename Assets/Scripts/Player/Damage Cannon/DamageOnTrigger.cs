using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    public int damageAmount = 10; // Cantidad de daño que se inflige

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cannon"))
        {
            // Comprobar si el objeto con el que colisionamos tiene el tag "Cannon"
            CannonController cannon = other.gameObject.GetComponent<CannonController>();

            if (cannon != null)
            {
                // Si el objeto tiene un script llamado "CannonController", le hacemos daño
                cannon.RecibirDanio(damageAmount);
            }

            // Destruir la bala después de la colisión
            Destroy(gameObject);
        }
    }
}
