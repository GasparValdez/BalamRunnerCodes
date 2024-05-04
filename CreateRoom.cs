using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Windows;

public class CreateRoom : MonoBehaviourPunCallbacks
{
    public TMP_InputField nameRoom;
    public GameObject panelLobby;
    public GameObject failedRoomMessage;
    [SerializeField] private Button[] botones;
    [SerializeField] private TMP_InputField[] inputs;

    public AudioClip failedMessage;

    private void Start()
    {
        for (int i = 0; i < botones.Length; i++)
        {
            int index = i; // Necesario para evitar el problema de cierre en bucles
            botones[i].onClick.AddListener(() => BotonPresionado(index));
        }
    }

    public void OnClickCreateRoom()
    {
        if (!PhotonNetwork.IsConnected) return;

        // Verifica si el campo de entrada est� vac�o
        if (string.IsNullOrWhiteSpace(nameRoom.text))
        {
            failedRoomMessage.SetActive(true); // Activa el mensaje de error
            AudioManager.Instance.ReproducirSonido(failedMessage);
            ActivarDesactivarBotones(false);
            ActivarDesactivarBotonesInputs(false);
            return; // Sale del m�todo sin crear la habitaci�n
        }

        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;

        PhotonNetwork.JoinOrCreateRoom(nameRoom.text, options, TypedLobby.Default);
    }

    public override void OnCreatedRoom()
    {
        Debug.Log("Created Room Succesful");
        panelLobby.SetActive(true);
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        failedRoomMessage.SetActive(true);
        ActivarDesactivarBotones(false);
        AudioManager.Instance.ReproducirSonido(failedMessage);
        Debug.Log(message: "Created room fail");
    }

    public void CloseFailedMessage()
    {
        failedRoomMessage.SetActive(false);
        ActivarDesactivarBotones(true);
        ActivarDesactivarBotonesInputs(true);
    }

    void BotonPresionado(int botonIndex)
    {
        // Verifica si los botones est�n habilitados
        if (failedRoomMessage.activeSelf)
        {
            Debug.Log("Los botones no est�n habilitados actualmente");
        }
        else
        {
            // Realiza las acciones del bot�n aqu�
            Debug.Log("Bot�n " + botonIndex + " presionado");
        }
    }

    // Funci�n para activar/desactivar la capacidad de interactuar con los botones
    private void ActivarDesactivarBotones(bool estado)
    {
        // Deshabilita/ habilita cada bot�n seg�n el estado proporcionado
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].interactable = estado;
        }
    }

    private void ActivarDesactivarBotonesInputs(bool estado)
    {
        for (int i = 0; i < botones.Length; i++)
        {
            botones[i].interactable = estado;
        }

        for (int i = 0; i < inputs.Length; i++)
        {
            inputs[i].interactable = estado;
        }
    }
}
