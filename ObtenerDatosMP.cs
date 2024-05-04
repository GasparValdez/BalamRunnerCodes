using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Photon.Pun;

public class ObtenerDatosMP : MonoBehaviourPunCallbacks
{
    public TextMeshProUGUI tiempoObtenido;
    public TextMeshProUGUI puntajeObtenido;

    private void Start()
    {
        if (!PhotonNetwork.IsMasterClient) // Si no es el maestro, obtener tiempo desde PlayerPrefs
        {
            ObtenerTiempo();
        }
        else // Si es el maestro, obtener tiempo y enviarlo a los demás jugadores
        {
            int puntosGuardados = PlayerPrefs.GetInt("puntosGuardados");
            float tiempoGuardado = PlayerPrefs.GetFloat("TiempoTranscurrido", 0f);

            // Puedes obtener las horas, minutos y segundos dividiendo el tiempo guardado
            int horas = (int)(tiempoGuardado / 3600f);
            int minutos = (int)((tiempoGuardado % 3600f) / 60f);
            int segundos = (int)(tiempoGuardado % 60f);

            // Formatear el tiempo en una cadena de texto
            string tiempoFormateado = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);

            tiempoObtenido.text = tiempoFormateado;
            puntajeObtenido.text = puntosGuardados.ToString();
        }
    }

    private void ObtenerTiempo()
    {
        // Obtener el tiempo acumulado guardado en PlayerPrefs
        int puntosGuardados = PlayerPrefs.GetInt("puntosGuardados");
        float tiempoGuardado = PlayerPrefs.GetFloat("TiempoTranscurrido", 0f);

        // Puedes obtener las horas, minutos y segundos dividiendo el tiempo guardado
        int horas = (int)(tiempoGuardado / 3600f);
        int minutos = (int)((tiempoGuardado % 3600f) / 60f);
        int segundos = (int)(tiempoGuardado % 60f);

        // Formatear el tiempo en una cadena de texto
        string tiempoFormateado = string.Format("{0:00}:{1:00}:{2:00}", horas, minutos, segundos);

        // Imprimir el tiempo obtenido en la consola para verificar
        Debug.LogFormat("Tiempo guardado: {0}", tiempoFormateado);

        // Asignar el tiempo formateado al TextMeshPro
        tiempoObtenido.text = tiempoFormateado;
        puntajeObtenido.text = puntosGuardados.ToString();
    }
}
