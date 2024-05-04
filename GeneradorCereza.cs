using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneradorCereaza : MonoBehaviour
{
    public GameObject prefabCereza; // Referencia al prefab del saco de basura

    public Transform[] puntosGeneracion;
    public float tiempoEntreGeneraciones = 1.0f;

    public int cantidadCerezaGeneradas = 0; // Variable para contar la cantidad de sacos de basura generados
    public int cantidadCerezaTotal; // Variable para almacenar la cantidad total de sacos de basura a generar

    private const string SacosBasuraKey = "OtrosObjetosDestruidos"; // Clave para obtener la cantidad de sacos de basura desde PlayerPrefs

    void Start()
    {
        // Obtener la cantidad de sacos de basura destruidos de PlayerPrefs
        cantidadCerezaTotal = PlayerPrefs.GetInt(SacosBasuraKey, 0);

        // Iniciar la corutina para generar objetos
        StartCoroutine(GenerarObjetosCoroutine());
    }

    IEnumerator GenerarObjetosCoroutine()
    {
        while (cantidadCerezaGeneradas < cantidadCerezaTotal)
        {
            foreach (Transform punto in puntosGeneracion)
            {
                // Generar un saco de basura en el punto de generación
                GenerarObjeto(prefabCereza, punto.position);
                cantidadCerezaGeneradas++;

                // Si ya hemos generado todos los sacos de basura, salir del bucle
                if (cantidadCerezaGeneradas >= cantidadCerezaTotal)
                    break;
            }

            // Esperar el tiempo especificado antes de generar el siguiente saco de basura
            yield return new WaitForSeconds(tiempoEntreGeneraciones);
        }
    }

    void GenerarObjeto(GameObject prefab, Vector3 punto)
    {
        // Instanciar el prefab en el punto de generación
        Instantiate(prefab, punto, Quaternion.identity);
    }
}
