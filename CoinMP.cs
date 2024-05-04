using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinMP : MonoBehaviour
{

    public int valor = 1; //Empieza el valor por uno
    private GameManagerMP gameManager;
    public AudioClip MonedaFX;

    private void Awake()
    {
        gameManager = GameObject.FindObjectOfType<GameManagerMP>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Si el objeto 2d colisione con el jugador, tocará la moneda; sumará el punto y se destruirá la moneda
        {
            if (gameManager != null)
            {
                gameManager.SumarPuntos(valor);
                Destroy(this.gameObject);
                AudioManager2.Instance.ReproducirSonido(MonedaFX);
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
