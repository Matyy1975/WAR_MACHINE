using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Nombre de la escena a la que deseas cambiar

    public void ChangeScene()
    {
        // Cambiar a la escena con el nombre especificado
        SceneManager.LoadScene(sceneName);
    }

    public void QuitGame()
    {
        // Salir del juego
        Application.Quit();
    }
}
