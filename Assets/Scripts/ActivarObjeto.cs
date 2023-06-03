using UnityEngine;

public class ActivarObjeto : MonoBehaviour
{
    // Objeto que se activará al presionar la tecla "space"
    public GameObject objetoActivar;

    // Update is called once per frame
    void Update()
    {
        // Si el jugador presiona la tecla "space"
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Activa el objeto
            objetoActivar.SetActive(true);
        }
    }
}
