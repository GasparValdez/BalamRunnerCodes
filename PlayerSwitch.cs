using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitch : MonoBehaviour
{
    [Header("Jaguar")]
    public MovimientoJugador playerController1;
    public CapsuleCollider2D colliderPlayer1;
    public Rigidbody2D rbPlayer1;
    public Camera cameraPlayer1;
   public GameObject jumpButton;

    [Header("Delfin")]
    public DelfinMovement playerController2;
    public CapsuleCollider2D colliderPlayer2;
    public Rigidbody2D rbPlayer2;
    public Camera cameraPlayer2;
    public GameObject joystick;

    private bool player1Active = true;

    public Animator transicion;


    // Variable para controlar si el tiempo está pausado
    private bool tiempoPausado = false;



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.Log("Cambio");
            // Detener el tiempo por 1 segundo

            StartCoroutine(PausarTiempo());

        }
    }

    public void Awake()
    {
        DesactivarCamera2();
    }

    public void SwitchPlayer()
    {
        player1Active = !player1Active; // Cambiar el estado del jugador activo

        if (player1Active)
        {
            // Activar jugador 1 y desactivar jugador 2
            playerController1.enabled = true;
            colliderPlayer1.enabled = true;
            rbPlayer1.simulated = true;
            cameraPlayer1.enabled = true;
           jumpButton.SetActive(true);

            playerController2.enabled = false;
            colliderPlayer2.enabled = false;
            rbPlayer2.simulated = false;
            cameraPlayer2.enabled = false;
            joystick.SetActive(false);
        }
        else
        {
            // Activar jugador 2 y desactivar jugador 1
            playerController1.enabled = false;
            colliderPlayer1.enabled = false;
            rbPlayer1.simulated = false;
            cameraPlayer1.enabled = false;
           jumpButton.SetActive(false);

            playerController2.enabled = true;
            colliderPlayer2.enabled = true;
            rbPlayer2.simulated = true;
            cameraPlayer2.enabled = true;
           joystick.SetActive(true);
        }
    }

    public void DesactivarCamera2()
    {
        cameraPlayer2.enabled = false;
    }


    public IEnumerator PausarTiempo()
    {
        transicion.SetTrigger("Cambio");

        // Esperar 1 segundo (tiempo real)
        yield return new WaitForSecondsRealtime(1f);

        // Pausar el tiempo
        Time.timeScale = 0f;
        tiempoPausado = true;

        // Esperar 1 segundo (tiempo real)
        yield return new WaitForSecondsRealtime(1f);

        // Reanudar el tiempo
        Time.timeScale = 1f;
        tiempoPausado = false;

        SwitchPlayer();
    }


}
