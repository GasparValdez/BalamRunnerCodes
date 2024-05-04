using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaMP : MonoBehaviourPun
{
    public AudioClip VidaFX;
    

    private void OnTriggerEnter2D(Collider2D other) //Hace que cuando toque el jugador a el collider de las vidas, esta se regenere en el HUD
    {
        if (other.gameObject.CompareTag("Player"))
        {
            bool vidaRecuperada = GameManagerMP.Instance.RecuperarVida();
            if (vidaRecuperada)
            {
                Destroy(this.gameObject);
                AudioManager2.Instance.ReproducirSonido(VidaFX);
            }
        }
        else
        {
            if (other.gameObject.CompareTag("Limite"))
            {
                Destroy(this.gameObject);
            }
        }
    }

   
}
