using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;


public class LeaveRoom : MonoBehaviour
{
    public GameObject panelLeave; //Regresa al la escena de crear escena

    public void OnClick_LeaveRoom()
    {
        PhotonNetwork.LeaveRoom(true);
        panelLeave.SetActive(false);
    }
}