using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowDownPW : MonoBehaviour
{

    public AudioClip SlowDownPwFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            // Buscar el generador de objetos por etiqueta
            GameObject objectGenerator = GameObject.FindWithTag("ObjectGenerator");

            if (objectGenerator != null)
            {
                // Obtener la componente ObjectGenerator del generador encontrado
                ObjectGenerator generatorComponent = objectGenerator.GetComponent<ObjectGenerator>();

                if (generatorComponent != null)
                {
                    // Activa el power-up en el generador de objetos
                    generatorComponent.ActivatePowerUp();
                    AudioManager2.Instance.ReproducirSonido(SlowDownPwFX);
                }
                else
                {
                    Debug.LogWarning("No se encontró el componente ObjectGenerator en el objeto con la etiqueta 'ObjectGenerator'.");
                }
            }
            else
            {
                Debug.LogWarning("No se encontró ningún objeto con la etiqueta 'ObjectGenerator'.");
            }

            PhotonView photonView = GetComponent<PhotonView>();
            if (photonView != null && photonView.IsMine)
            {
                PhotonNetwork.Destroy(gameObject); // Destruye el objeto que activó el power-up
            }
        }
        
    }
}
