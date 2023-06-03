using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Autodestruccion : MonoBehaviour
{
    public float tiempoDestruccion = 2.0f; // Tiempo en segundos para autodestruir el objeto

    void Start()
    {
        // Llamar al m�todo para autodestruir el objeto despu�s del tiempo especificado
        Invoke("DestruirObjeto", tiempoDestruccion);
    }

    void DestruirObjeto()
    {
        // Destruir el objeto
        Destroy(gameObject);
    }
}
