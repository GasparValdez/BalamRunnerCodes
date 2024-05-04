using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPausa : MonoBehaviour
{
    [SerializeField] private GameObject botonPausa;

    [SerializeField] private GameObject menuPausa;

    private bool juegoPausado = false;
    private bool sonidosPausados = false;






    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) //Si la tecla escape es presionada, se pausa el juego y si se vuelve a presionar se reanuda
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();
            }
        }
    }
    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f; //Detener el tiempo del juego cuando se hace pausa
        sonidosPausados = true;
        AudioListener.pause = true; // Pausar todos los sonidos
        botonPausa.SetActive(false); //Desactivar el boton de pausa cuando se muestra el menu pausa
        menuPausa.SetActive(true); //Activar el menu de pausa cuando se da clic al boton de pausa
    }

    public void Reanudar()
    {
        juegoPausado = false;
        Time.timeScale = 1f; //Reanudar el tiempo del juego cuando se hace reanudar
        sonidosPausados = false;
        AudioListener.pause = false;
        botonPausa.SetActive(true); //Activar el boton de pausa cuando se desactiva el menu pausa
        menuPausa.SetActive(false); //Desactivar el menu de pausa cuando se da clic al boton de pausa
    }

    public void Reiniciar()
    {
        juegoPausado = false;
        Time.timeScale = 1f; //Reiniciar el nivel del juego
        sonidosPausados = false;
        AudioListener.pause = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reiniciar la escena activa del juego
    }

    public void Cerrar()
    {
        juegoPausado = false;
        Time.timeScale = 1f; //Reanudar el tiempo del juego cuando se hace reanudar
        sonidosPausados = false;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void CerrarGuest()
    {
        juegoPausado = false;
        Time.timeScale = 1f; //Reanudar el tiempo del juego cuando se hace reanudar
        sonidosPausados = false;
        AudioListener.pause = false;
        SceneManager.LoadScene("MainMenuGuest");
    }
}
