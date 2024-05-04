using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GestorPhoton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        InvokePlayers();
    }

    public void InvokePlayers()
    {
        PhotonNetwork.Instantiate("PlayerMP", new Vector3(-18.29f, -3.84f, -0.1177937f), Quaternion.identity);
    }
}
