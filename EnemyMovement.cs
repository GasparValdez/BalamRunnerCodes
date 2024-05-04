using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    // Velocidad de desplazamiento
    public float velocidad = 5f;

    // Referencia al script DestroyedObjectCounter
    private BasuraCinematica contadorObjetosDestruidos;

    void Start()
    {
        // Obtener una referencia al script DestroyedObjectCounter
        contadorObjetosDestruidos = FindObjectOfType<BasuraCinematica>();
    }

    void Update()
    {
        // La velocidad del enemigo en izquierda
        Vector3 desplazamiento = Vector3.left * velocidad * Time.deltaTime;
        // Desplazamiento del enemigo
        transform.Translate(desplazamiento);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica si el objeto que entró en el trigger tiene la etiqueta "limite"
        if (other.gameObject.CompareTag("Player"))
        {
            // Muestra en la consola qué objeto se destruyó
            Debug.Log("Se destruyó el objeto: " + gameObject.name);

            // Incrementar el contador correspondiente según el tipo de objeto destruido
            if (gameObject.name.Contains("Botella"))
            {
                contadorObjetosDestruidos.IncrementarContadorBotellas();
            }
            else if (gameObject.name.Contains("Saco de basura"))
            {
                contadorObjetosDestruidos.IncrementarContadorSacosaBasura();
            }
            else
            {
                contadorObjetosDestruidos.IncrementarContadorOtrosObjetos();
            }

            // Destruye este objeto (el enemigo)
            Destroy(gameObject);
        }
        if (other.gameObject.CompareTag("Limite"))
        {
            // Destruye este objeto (el enemigo)
            Destroy(gameObject);
        }
    }
}