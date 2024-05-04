using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using UnityEngine.UI;

public class PlayerListMenu : MonoBehaviourPunCallbacks
{
    public Transform goContent;
    public PlayerList playerList;
    private List<PlayerList> _listing = new List<PlayerList>();

    public Button startGameButton; // Agrega la referencia al botón en el Inspector

    private int connectedPlayers = 0; // Variable para seguir el número de jugadores conectados

    public override void OnEnable()
    {
        base.OnEnable();
        GetCurrentRoomPlayers();
    }

    public override void OnDisable()
    {
        base.OnDisable();

        for (int i = 0; i < _listing.Count; i++)
        {
            Destroy(_listing[i].gameObject);
        }
        _listing.Clear();
    }

    private void GetCurrentRoomPlayers()
    {
        if (!PhotonNetwork.IsConnected) return;
        if (PhotonNetwork.CurrentRoom == null || PhotonNetwork.CurrentRoom.Players == null) return;
        connectedPlayers = PhotonNetwork.CurrentRoom.PlayerCount; // Actualiza el número de jugadores conectados

        foreach (KeyValuePair<int, Player> playerInfo in PhotonNetwork.CurrentRoom.Players)
        {
            AddPlayerListing(playerInfo.Value);
        }

        startGameButton.interactable = PhotonNetwork.IsMasterClient && connectedPlayers == 2; // Habilita el botón si el jugador es maestro y hay dos jugadores conectados
    }

    private void AddPlayerListing(Player player)
    {
        int index = _listing.FindIndex(x => x.Player == player);

        if (index != -1)
        {
            _listing[index].SetPlayerInfo(player);
        }
        else
        {
            PlayerList list = Instantiate(playerList, goContent);
            if (list != null)
            {
                list.SetPlayerInfo(player);
                _listing.Add(list);
            }
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        AddPlayerListing(newPlayer);
        connectedPlayers++; // Incrementa el número de jugadores conectados cuando un nuevo jugador entra
        startGameButton.interactable = PhotonNetwork.IsMasterClient && connectedPlayers == 2; // Actualiza la interactividad del botón
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        int index = _listing.FindIndex(x => x.Player == otherPlayer);

        if (index != -1)
        {
            Destroy(_listing[index].gameObject);
            _listing.RemoveAt(index);
        }

        connectedPlayers--; // Disminuye el número de jugadores conectados cuando un jugador sale
        startGameButton.interactable = PhotonNetwork.IsMasterClient && connectedPlayers == 2; // Actualiza la interactividad del botón
    }

    public void OnClick_StartGame()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.LoadLevel("Minijuego");
        }
    }
}
