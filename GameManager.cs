using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public HUD hud;
    public int PuntosTotales { get; set; }
    public int puntosPartida { get; set; } // Variable para almacenar los puntos de partida

    private int highScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.Log("¡Cuidado! Más de un GameManager en escena.");
        }

        // Cargar el highscore almacenado en PlayerPrefs
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        hud.ActualizarHighScore(highScore); // Actualizar el HUD con el highscore cargado

        // Cargar los puntos de partida almacenados en PlayerPrefs
        puntosPartida = PlayerPrefs.GetInt("PuntosPartida", 0);
    }

    public void SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);

        // Verificar si se ha superado el highscore
        if (PuntosTotales > highScore)
        {
            highScore = PuntosTotales;
            PlayerPrefs.SetInt("HighScore", highScore);
            hud.ActualizarHighScore(highScore); // Actualizar el HUD con el nuevo highscore
        }

        // Actualizar los puntos de partida
        puntosPartida = PuntosTotales;
        PlayerPrefs.SetInt("PuntosPartida", puntosPartida);
    }

    // Método para obtener el highscore actual
    public int GetHighScore()
    {
        return highScore;
    }
}
