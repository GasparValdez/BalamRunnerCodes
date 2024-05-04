using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorC : MonoBehaviour
{
    public GameObject prefabBotella; // Referencia al prefab de la botella

    public Transform[] puntosGeneracion;
    public float tiempoEntreGeneraciones = 1.0f;

    public int cantidadBotellasGeneradas = 0; // Variable para contar la cantidad de botellas generadas
    public int cantidadBotellasTotal; // Variable para almacenar la cantidad total de botellas a generar

    private const string BotellasKey = "BotellasDestruidas"; // Clave para obtener la cantidad de botellas desde PlayerPrefs

    void Start()
    {
        // Obtener la cantidad de botellas destruidas de PlayerPrefs
        cantidadBotellasTotal = PlayerPrefs.GetInt(BotellasKey, 0);

        // Iniciar la corutina para generar objetos
        StartCoroutine(GenerarObjetosCoroutine());
    }

    IEnumerator GenerarObjetosCoroutine()
    {
        while (cantidadBotellasGeneradas < cantidadBotellasTotal)
        {
            foreach (Transform punto in puntosGeneracion)
            {
                // Generar una botella en el punto de generación
                GenerarObjeto(prefabBotella, punto.position);
                cantidadBotellasGeneradas++;

                // Si ya hemos generado todas las botellas, salir del bucle
                if (cantidadBotellasGeneradas >= cantidadBotellasTotal)
                    break;
            }

            // Esperar el tiempo especificado antes de generar la siguiente botella
            yield return new WaitForSeconds(tiempoEntreGeneraciones);
        }
    }

    void GenerarObjeto(GameObject prefab, Vector3 punto)
    {
        // Instanciar el prefab en el punto de generación
        Instantiate(prefab, punto, Quaternion.identity);
    }
}
