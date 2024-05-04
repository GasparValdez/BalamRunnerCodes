using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class ButtonManager : MonoBehaviour
{
    public GameObject[] objectsToActivate;
    public GameObject[] objectsToDesactivate;


    // Contar los objetos destruidos
    public int contadorBotellas = 0;
    public int contadorSacosBasura = 0;
    public int contadorOtrosObjetos = 0;

    //guardar en PlayerPrefs
    private const string BotellasKey = "BotellasDestruidas";
    private const string SacosBasuraKey = "SacosBasuraDestruidos";
    private const string OtrosObjetosKey = "OtrosObjetosDestruidos";

    private void Start()
    {
        // Al iniciar la scena lo cargamos
        contadorBotellas = PlayerPrefs.GetInt(BotellasKey, 0);
        contadorSacosBasura = PlayerPrefs.GetInt(SacosBasuraKey, 0);
        contadorOtrosObjetos = PlayerPrefs.GetInt(OtrosObjetosKey, 0);
    }

    public void GoToGameplay()
    {
        // Eliminar las claves relacionadas con los contadores
        PlayerPrefs.DeleteKey(BotellasKey);
        PlayerPrefs.DeleteKey(SacosBasuraKey);
        PlayerPrefs.DeleteKey(OtrosObjetosKey);

        // Guardar los cambios
        PlayerPrefs.Save();

        SceneManager.LoadScene("Gameplay");
    }

    public void ShowCredits()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj1 in objectsToDesactivate)
        {
            obj1.SetActive(false);
        }
    }

    public void HideCredits()
    {
        foreach (GameObject obj in objectsToActivate)
        {
            obj.SetActive(true);
        }

        foreach (GameObject obj1 in objectsToDesactivate)
        {
            obj1.SetActive(false);
        }
    }

    public void GoToMainMenu()
    {

        // Eliminar las claves relacionadas con los contadores
        PlayerPrefs.DeleteKey(BotellasKey);
        PlayerPrefs.DeleteKey(SacosBasuraKey);
        PlayerPrefs.DeleteKey(OtrosObjetosKey);

        // Guardar los cambios
        PlayerPrefs.Save();

    
        SceneManager.LoadScene("MainMenuGuest");
    }

    public void ExitGame()
    {
        Application.Quit();
        Debug.Log("Exit Game");
    }

    public void Multiplayer()
    {
        SceneManager.LoadScene("MultiplayerLobby");
    }
}
