using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;
using Photon.Realtime;


public class RedController2 : MonoBehaviourPunCallbacks //Se llamaa el PunCallBBacks pq será multijugador
{
    public TextMeshProUGUI textStatus;

    private void Start()
    {
        OnClick_ConectButton();
        SetNickname();
        PhotonNetwork.AutomaticallySyncScene = true;
    }

    private void SetNickname()
    {
        string userAlias = PlayerPrefs.GetString("UserAlias");
        if (string.IsNullOrEmpty(userAlias))
        {
            PhotonNetwork.NickName = "Guest" + Random.Range(0, 999);
        }
        else
        {
            PhotonNetwork.NickName = userAlias;
        }
    }

    public void OnClick_ConectButton()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    public override void OnConnectedToMaster()
    {
        textStatus.text = "Succesfull";
        if (!PhotonNetwork.InLobby)
            PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        textStatus.text = "Disconnected";
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("Joined Lobby");
    }
}