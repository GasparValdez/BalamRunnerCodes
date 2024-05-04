using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paralax : MonoBehaviour
{
    public float speed = 2f;
    public float resetPosition = 17.58f;
    public float startPosition = -17.58f;

    void Update()
    {
        // Movimiento de la imagen
        transform.Translate(Vector2.right * speed * Time.deltaTime);

        // Si la imagen se ha movido más allá de la posición de reset, la reposicionamos
        if (transform.position.x >= resetPosition)
        {
            RepositionBackground();
        }
    }

    // Reposiciona la imagen a su posición inicial
    void RepositionBackground()
    {
        Vector2 groundOffSet = new Vector2(resetPosition - startPosition, 0);
        transform.position = (Vector2)transform.position - groundOffSet;
    }
}


    

