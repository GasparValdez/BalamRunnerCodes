using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class SaltoRegulable : MonoBehaviour
{
    [Header("Referencias")]

    private Rigidbody2D rb2D;
    private Animator animator;

    [Header("Movimiento")]
    [SerializeField] private float velocidadDeMovimiento;


    [Header("Salto")]
    [SerializeField] private float fuerzaSalto;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector2 dimensionesCaja;
    [SerializeField] private LayerMask queEsSuelo;

    private bool enSuelo;
    private bool saltar;

    [Header("Salto Regulable")]
    [Range(0, 1)][SerializeField] private float multiplicadorCancelarSalto;
    [SerializeField] private float multiplicadorGravedad;
    private float escalaGravedad;
    private bool botonSaltoArriba = true;


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        escalaGravedad = rb2D.gravityScale;
    }

    private void Update()
    {
        rb2D.velocity = new Vector2(velocidadDeMovimiento, rb2D.velocity.y);

        if (Input.GetButton("Jump"))
        {
            saltar = true;
        }

        if (Input.GetButtonUp("Jump"))
        {
            BotonSaltoArriba();
        }
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0, queEsSuelo);
        animator.SetBool("enSuelo", enSuelo);
    }

    private void FixedUpdate()
    {
        if(saltar && botonSaltoArriba && enSuelo)
        {
            Saltar();
        }

        if(rb2D.velocity.y < 0 && !enSuelo)
        {
            rb2D.gravityScale = escalaGravedad * multiplicadorGravedad;
        }
        else
        {
            rb2D.gravityScale = escalaGravedad;
        }

        saltar = false;
    }

    private void Saltar()
    {
        rb2D.AddForce(Vector2.up * fuerzaSalto, ForceMode2D.Impulse);
        enSuelo = false;
    }

    private void BotonSaltoArriba()
    {
        if(rb2D.velocity.y > 0)
        {
            rb2D.AddForce(Vector2.down * rb2D.velocity.y * (1 - multiplicadorCancelarSalto), ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

}
