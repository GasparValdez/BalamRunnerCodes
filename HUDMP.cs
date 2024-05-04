using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class HUDMP : MonoBehaviour
{
    public static HUDMP Instance { get; private set; }

    public TextMeshProUGUI puntos;
    public GameObject[] vidas;
    private Coroutine parpadeoCoroutine;
    private bool parpadeando = false;

    private string savePoints;
    public bool EstaParpadeando()
    {
        return parpadeando;
    }

    public void IniciarParpadeo()
    {
        if (parpadeoCoroutine == null)
        {
            parpadeoCoroutine = StartCoroutine(ParpadearTexto());
        }
    }

    public void DetenerParpadeo()
    {
        if (parpadeoCoroutine != null)
        {
            StopCoroutine(parpadeoCoroutine);
            parpadeoCoroutine = null;
        }
        parpadeando = false;
    }

    private IEnumerator ParpadearTexto()
    {
        parpadeando = true;
        Color originalColor = puntos.color;
        Color doradoColor = Color.yellow;

        float parpadeoDuration = 1f; // Duración total del parpadeo (1 segundo)
        float elapsedTime = 0f; // Tiempo transcurrido

        while (elapsedTime < parpadeoDuration)
        {
            puntos.color = doradoColor;
            yield return new WaitForSeconds(0.5f);
            puntos.color = originalColor;
            yield return new WaitForSeconds(0.5f);
            elapsedTime += 1f; // Aumentar el tiempo transcurrido en 1 segundo
        }

        puntos.color = originalColor; // Restaurar el color original al final del parpadeo
        parpadeando = false;
        parpadeoCoroutine = null;
    }

    public void RestartParpadeo()
    {
        DetenerParpadeo();
        IniciarParpadeo();
    }

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
        puntos.text = GameManagerMP.Instance.PuntosTotales.ToString();

    }

    public void ActualizarPuntos(int puntosTotales)
    {
        puntos.text = puntosTotales.ToString();
        DetenerParpadeo();
        // Guardar los puntos en PlayerPrefs
        PlayerPrefs.SetInt("puntosGuardados", puntosTotales);

        // Asegúrate de llamar a Save para que los cambios se guarden de manera efectiva
        PlayerPrefs.Save();
    }

    public void ActualizarVidas(int cantidadVidas)
    {
        // Desactivar todas las vidas
        for (int i = 0; i < vidas.Length; i++)
        {
            vidas[i].SetActive(false);
        }

        // Activar la cantidad de vidas correcta
        for (int i = 0; i < cantidadVidas; i++)
        {
            vidas[i].SetActive(true);
        }
    }

    public void DesactivarVida(int indice)
    {
        vidas[indice].SetActive(false);
    }

    public void ActivarVida(int indice)
    {
        vidas[indice].SetActive(true);
    }

   
}