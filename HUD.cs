using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HUD : MonoBehaviour
{
    public static HUD Instance { get; private set; }
    public TextMeshProUGUI puntos;
    public TextMeshProUGUI highScoreText; // Nuevo campo para mostrar el highscore
    public TextMeshProUGUI puntosPartidaText; // Nuevo campo para mostrar los puntos de partida


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("Cuidado! Más de un HUD en escena.");
        }
    }

    void Update()
    {
        puntos.text = GameManager.Instance.PuntosTotales.ToString();
        puntosPartidaText.text = GameManager.Instance.puntosPartida.ToString(); // Actualiza los puntos de partida
    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
    }

    // Método para actualizar el highscore en el HUD
    public void ActualizarHighScore(int highScore)
    {
        highScoreText.text = "" + highScore.ToString();
    }


}
