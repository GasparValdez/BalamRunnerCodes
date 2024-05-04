using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

public class LeaveMatch : MonoBehaviour
{
    public GameObject messageLeave;

    public Button[] buttons;

    public AudioClip MessageQuitFX;

    [SerializeField] private GameManagerMP gameManagerMP;
    [SerializeField] private HUDMP hudMP;
    [SerializeField] private GestorTiempo gestorTiempo;
    public void LeaveCurrentMatch()
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

    private void DeshabilitarInteraccionInputsYBotones(bool estado)
    {
       
        foreach (Button button in buttons)
        {
            button.interactable = !estado;
        }
    }

    public void CloseMessageLeave()
    {
        messageLeave.SetActive(false);
        DeshabilitarInteraccionInputsYBotones(false);
    }
    public void ShowMessageLeave()
    {
        messageLeave.SetActive(true);
        AudioManager2.Instance.ReproducirSonido(MessageQuitFX);
        DeshabilitarInteraccionInputsYBotones(true);
    }
}
