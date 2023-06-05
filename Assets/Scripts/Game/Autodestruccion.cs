using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruccion : MonoBehaviour
{
    public float tiempoDestruccion = 2.0f; // Tiempo en segundos para autodestruir el objeto

    void Start()
    {
        // Llamar al método para autodestruir el objeto después del tiempo especificado
        Invoke("DestruirObjeto", tiempoDestruccion);
    }

    void DestruirObjeto()
    {
        // Destruir el objeto
        Destroy(gameObject);
    }
}
