using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using Photon.Pun;

public class CambiarNivel : MonoBehaviour
{
    public void SeleccionNivel(string nombreNivel)
    {
        SceneManager.LoadScene(nombreNivel);
    }

    public void SeleccionNivel(int numeroNivel)
    {
        SceneManager.LoadScene(numeroNivel);
    }

    public void SetMainMenu()
    {
        string userAlias = PlayerPrefs.GetString("UserAlias");
        if (string.IsNullOrEmpty(userAlias))
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenuGuest");
        }
        else
        {
            PhotonNetwork.Disconnect();
            SceneManager.LoadScene("MainMenu");
        }
    }




}
