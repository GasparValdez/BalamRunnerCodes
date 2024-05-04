using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class GestorTiempo : MonoBehaviourPunCallbacks, IPunObservable
{
    private float tiempoTranscurrido;
    public TextMeshProUGUI tiempoTexto;

    void Start()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            tiempoTranscurrido = PlayerPrefs.GetFloat("TiempoTranscurrido", 0f); // Obtener el tiempo guardado desde PlayerPrefs
        }
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            tiempoTranscurrido += Time.deltaTime;
            photonView.RPC("ActualizarTiempo", RpcTarget.AllBuffered, tiempoTranscurrido);
        }
    }

    [PunRPC]
    void ActualizarTiempo(float tiempo)
    {
        tiempoTranscurrido = tiempo;
        MostrarTiempo();

        if (PhotonNetwork.IsMasterClient)
        {
            // Solo el cliente maestro guarda el tiempo transcurrido en PlayerPrefs
            PlayerPrefs.SetFloat("TiempoTranscurrido", tiempoTranscurrido);
            PlayerPrefs.Save();
        }
    }

    void MostrarTiempo()
    {
        int horas = (int)(tiempoTranscurrido / 3600f);
        int minutos = (int)((tiempoTranscurrido % 3600f) / 60f);
        int segundos = (int)(tiempoTranscurrido % 60f);
        tiempoTexto.text = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(tiempoTranscurrido);
        }
        else
        {
            tiempoTranscurrido = (float)stream.ReceiveNext();
            MostrarTiempo();

            if (!PhotonNetwork.IsMasterClient)
            {
                // Si no es el cliente maestro, guardamos el tiempo transcurrido en PlayerPrefs también
                PlayerPrefs.SetFloat("TiempoTranscurrido", tiempoTranscurrido);
                PlayerPrefs.Save();
            }
        }
    }

   
}
