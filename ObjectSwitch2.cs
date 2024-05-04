using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSwitch2 : MonoBehaviour
{
    public PlayerSwitch PlayerSwitch;
    // Velocidad de desplazamiento
    public float velocidad = 5f;
    public GameObject[] objetosADesactivar;

    public GameObject[] objetosAActivar;

    // Método para desactivar los objetos
    public void DesactivarGeneradoresDelfin()
    {
        foreach (GameObject objeto in objetosADesactivar)
        {
            objeto.SetActive(false);
        }
    }

    public void ActivarGeneradoresJaguar()
    {
        foreach (GameObject objeto in objetosAActivar)
        {
            objeto.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {

    }



    void Update()
    {
        // La velocidad del enemigo en izquierda
        Vector3 desplazamiento = Vector3.left * velocidad * Time.deltaTime;
        // Desplazamiento del enemigo
        transform.Translate(desplazamiento);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerSwitch.StartCoroutine(PlayerSwitch.PausarTiempo());
            DesactivarGeneradoresDelfin();
            ActivarGeneradoresJaguar();
            // Destruir este objeto (el enemigo)
            Destroy(gameObject);
        }
        else if (collision.gameObject.CompareTag("Limite"))
        {
            // Destruir este objeto (el enemigo) si colisiona con el límite
            Destroy(gameObject);
        }
    }

}
