using UnityEngine;
using System.Collections;

public class DesactivarG : MonoBehaviour
{
    public GameObject objeto;
    public float[] tiemposDeEspera = { 1.0f, 2.0f, 3.0f }; // Lista de tiempos de espera posibles

    void Start()
    {
        StartCoroutine(ActivarDesactivarAleatorio());
    }

    IEnumerator ActivarDesactivarAleatorio()
    {
        while (true) // Bucle infinito
        {
            // Elije un tiempo de espera aleatorio de la lista
            float tiempoAleatorio = tiemposDeEspera[Random.Range(0, tiemposDeEspera.Length)];

            // Desactiva el objeto
            objeto.SetActive(false);

            // Espera el tiempo aleatorio especificado
            yield return new WaitForSeconds(tiempoAleatorio);

            // Activa el objeto
            objeto.SetActive(true);

            // Espera antes de repetir
            yield return new WaitForSeconds(tiempoAleatorio);
        }
    }
}
