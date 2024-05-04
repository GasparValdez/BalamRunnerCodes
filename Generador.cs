using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generador : MonoBehaviour
{

    public List<GameObject> Objetos;

    public Transform[] puntosGeneracion;

    public float tiempoEntreGeneraciones = 1.0f;

    private void OnEnable()
    {
        // Iniciamos la corutina para generar objetos cada cierto tiempo
        StartCoroutine(GenerarObjetosCoroutine());
    }
    private void Update()
    {
        
    }
    void Start()
    {
        
    }

    IEnumerator GenerarObjetosCoroutine()
    {
        while (true)
        {
            // Generamos los objetos en los puntos de generación
            foreach (Transform punto in puntosGeneracion)
            {
                // Elegimos un prefab aleatorio de la lista
                GameObject prefabSeleccionado = Objetos[Random.Range(0, Objetos.Count)];
                GenerarObjeto(prefabSeleccionado, punto.position);
            }

            // Esperamos el tiempo especificado antes de la próxima generación
            yield return new WaitForSeconds(tiempoEntreGeneraciones);
        }
    }

    void GenerarObjeto(GameObject prefab, Vector3 punto)
    {
        // Instanciamos el prefab en el punto de generación
        GameObject nuevoObjeto = Instantiate(prefab, punto, Quaternion.identity);

    }
}
