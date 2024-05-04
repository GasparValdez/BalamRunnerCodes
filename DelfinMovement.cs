using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelfinMovement : MonoBehaviour
{
    public float velocidad = 5f; // Velocidad de movimiento del objeto
                                 //  public AudioClip SwimFX;

    public DynamicJoystick joystick; // Referencia al joystick táctil

    void Update()
    {
        // Obtener la entrada del teclado
        float movimientoVertical = Input.GetAxis("Vertical");
        //AudioManager.Instance.ReproducirSonido(SwimFX);
        float movimientoVertical1 = joystick.Vertical;

        // Calcular el desplazamiento basado en la entrada y la velocidad
        Vector3 movimiento = new Vector3(0f, movimientoVertical, 0f) * velocidad * Time.deltaTime;

        // Aplicar el movimiento al objeto
        transform.Translate(movimiento);

        if (Mathf.Abs(movimientoVertical1) > 0.1f)
        {
            // Calcular el desplazamiento basado en la entrada y la velocidad
            Vector3 movimiento1 = new Vector3(0f, movimientoVertical1, 0f) * velocidad * Time.deltaTime;

            // Aplicar el movimiento al objeto
            transform.Translate(movimiento1);
        }
    }
}
