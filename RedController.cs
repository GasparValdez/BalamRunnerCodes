using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Windows;

public class RedController : MonoBehaviourPunCallbacks
{
    
    public GameObject mensajeExito;
    public GameObject mensajeFracaso;

    public AudioClip MessageErrorFX;
    public AudioClip MessageSuccessFX;

    [SerializeField] private Button[] botones;
   

    private void Start()
    {
        //OnClick_ConnectButton();
        PhotonNetwork.NickName = "Test" + Random.Range(0, 999); //Nombre del jugador

        // Suscribe la función BotonPresionado al evento onClick de cada botón
        for (int i = 0; i < botones.Length; i++)
        {
            int index = i; // Necesario para evitar el problema de cierre en bucles
            botones[i].onClick.AddListener(() => BotonPresionado(index));
        }
    }


    public void OnClick_ConnectButton()
    {
        PhotonNetwork.ConnectUsingSettings(); //Permite conectarse al servidor
    }

    public void CloseMessageError()
    {
        mensajeFracaso.SetActive(false);
        ActivarDesactivarBotones(true);
    }

    public void CloseMessageSuccess()
    {
        mensajeExito.SetActive(false);
        SceneManager.LoadScene("MultijugadorLobby");

    }

    public override void OnConnectedToMaster() //Cuando se conecta al servidor
    {
        
        mensajeExito.SetActive(true); //Marca una conexión exitosa
        AudioManager.Instance.ReproducirSonido(MessageSuccessFX);
        if (!PhotonNetwork.InLobby) //Si el jugador ya esta conectado
        PhotonNetwork.JoinLobby(); //Entra al lobby


        if (mensajeExito.activeSelf)
        {
            ActivarDesactivarBotones(false);
        }
    }

    public override void OnDisconnected(DisconnectCause cause) //Cuando se desconecta del servidor
    {
        mensajeFracaso.SetActive(true); //Marca estado desconectado
        AudioManager.Instance.ReproducirSonido(MessageErrorFX);
        if (mensajeFracaso.activeSelf)
        {
            ActivarDesactivarBotones(false);
        }
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }

    void BotonPresionado(int botonIndex)
    {
        // Verifica si los botones están habilitados
        if (mensajeFracaso.activeSelf)
        {
            Debug.Log("Los botones no están habilitados actualmente");
        }
        else
        {
            // Realiza las acciones del botón aquí
            Debug.Log("Botón " + botonIndex + " presionado");
        }
    }

    private void ActivarDesactivarBotonesInputs(bool estado)
    {
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].interactable = estado;
        }

       
    }

    // Función para activar/desactivar la capacidad de interactuar con los botones
    private void ActivarDesactivarBotones(bool estado)
    {
        // Deshabilita/ habilita cada botón según el estado proporcionado
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].interactable = estado;
        }
    }
}
