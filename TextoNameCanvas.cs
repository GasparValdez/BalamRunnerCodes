using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class TextoNameCanvas : MonoBehaviour
{
    public PhotonView photonView;
    public TextMeshProUGUI textName;


    private void Awake()
    {
        if (photonView.IsMine)
        {
            textName.text = PhotonNetwork.NickName;
        }
        else
        {
            textName.text = photonView.Owner.NickName;

        }
    }

    private void Start()
    {

    }

    private void Update()
    {

    }
}
