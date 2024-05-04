using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using TMPro;

public class PlayerList : MonoBehaviour
{


    public TextMeshProUGUI textinfo;

    //Cuando son cosas externas se necesita un encapsulamiento
    public Player Player { get; private set; }

    public void SetPlayerInfo(Player playerInfo)
    {
        Player = playerInfo;
        textinfo.text = Player.NickName;
    }
}