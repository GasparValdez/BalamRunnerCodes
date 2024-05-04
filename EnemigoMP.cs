using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemigoMP : MonoBehaviourPunCallbacks
{
    public AudioClip GolpeFX;
   
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GameManagerMP.Instance.PerderVidaMP();
            AudioManager2.Instance.ReproducirSonido(GolpeFX);
           
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limite"))
        {
            Destroy(gameObject);
        }
    }


}
