using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ControllerView : MonoBehaviour
{
    public MonoBehaviour[] ignorar;
    private PhotonView _photonView;
   


    private void Awake()
    {
        _photonView = GetComponent<PhotonView>();
    }


    private void Start()
    {
        if (!_photonView.IsMine)
        {
            foreach (var codigo in ignorar)
            {
                codigo.enabled = false;
                PlayerPrefs.SetString("PlayerSelected", "Clend1");
            }
            
        }
        else
        {
            PlayerPrefs.SetString("PlayerSelected", "Clend2");
        }
    }


}
