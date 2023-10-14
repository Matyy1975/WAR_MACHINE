using UnityEngine;

public class DamageOnTrigger : MonoBehaviour
{
    public int damageAmount = 10; // Cantidad de da�o que se inflige

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Cannon"))
        {
            // Comprobar si el objeto con el que colisionamos tiene el tag "Cannon"
            CannonController cannon = other.gameObject.GetComponent<CannonController>();

            if (cannon != null)
            {
                // Si el objeto tiene un script llamado "CannonController", le hacemos da�o
                cannon.RecibirDanio(damageAmount);
            }

            // Destruir la bala despu�s de la colisi�n
            Destroy(gameObject);
        }
    }
}
