using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BolsaPWMP : MonoBehaviour
{
    public int valor = 1; //Empieza el valor por uno
    private GameManagerMP gameManager;
    public AudioClip BolsaFX;
    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManagerMP>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontr� el GameManager en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Si el objeto 2d colisione con el jugador, tocar� la moneda; sumar� el punto y se destruir� la moneda
        {
            if (gameManager != null)
            {
                gameManager.MultiplicarPuntos(valor);
                Destroy(this.gameObject);
                AudioManager2.Instance.ReproducirSonido(BolsaFX);
            }
            else
            {
                Debug.LogWarning("GameManager no asignado en el objeto item.");
            }
        }
        else
        {
            if (collision.gameObject.CompareTag("Limite"))
            {
                Destroy(gameObject);
            }
        }

    }

}
