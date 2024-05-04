using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int valor = 1; //Empieza el valor por uno
    public GameManager gameManager;
    public AudioClip MonedaFX;

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) //Si el objeto 2d colisione con el jugador, tocará la moneda; sumará el punto y se destruirá la moneda
        {
            GameManager gameManager = FindObjectOfType<GameManager>();
            gameManager.SumarPuntos(valor);
            Destroy(this.gameObject);
            AudioManager.Instance.ReproducirSonido(MonedaFX);
            
        }
    }
}
