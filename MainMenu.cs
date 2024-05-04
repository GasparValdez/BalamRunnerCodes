using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void Salir()
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
