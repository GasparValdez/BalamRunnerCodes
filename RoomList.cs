using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using TMPro;
using Photon.Pun;


public class RoomList : MonoBehaviour
{
    public TextMeshProUGUI textinfo;

    //Cuando son cosas externas se necesita un encapsulamiento
    public RoomInfo RoomInfo { get; private set; }

    public void SetRoomInfo(RoomInfo roomInfo)
    {
        RoomInfo = roomInfo;
        textinfo.text = roomInfo.MaxPlayers + " - " + roomInfo.Name;
    }

    public void OnClick_Button()
    {
        PhotonNetwork.JoinRoom(RoomInfo.Name);
    }



}

