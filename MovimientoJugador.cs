using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MovimientoJugador : MonoBehaviour
{
    private Rigidbody2D rb2D; //Le damos una referencia al rigidbody en 2D para mover el personaje a trav�s de �l

    [Header("Movimiento")] //Ordena el c�digo

    private float movimientoHorizontal = 0f; //Tener la entrada o el input de las teclas / controles

    [SerializeField] private float velocidadDeMovimiento; //Velocidad a la que queremos que nuestro personaje se mueva. El SerializeField permite que aparezca en el inspector de Unity

    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento; //Le aplica un deslizamiento suave al personaje

    private Vector3 velocidad = Vector3.zero; //Bloqueamos la velocidad en Z en 0, por que no queremos que se mueva en ese eje

    private bool mirandoDerecha = true; //Saber si el personaje est� mirando a la derecha o a la izquierda

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;  //A�ade una fuerza de salto
    [SerializeField] private LayerMask queEsSuelo; //Indica que las superficies sean altas para que nuestro personaje salte
    [SerializeField] private Transform controladorSuelo; //Objeto que se genera en los pies del personaje para generar el siguiente objeto que ser� una caja alrededor de este mismo objeto
    [SerializeField] private Vector3 dimensionesCaja; //Esta es la caja que nos dar� la informaci�n sobre si estamos en el suelo o no.
    [SerializeField] private bool enSuelo; //Variable de tipo falso verdadero para saber si el personaje est� en el suelo o no.

    private bool salto = false; //Variable para el salto

    [SerializeField] private float fuerzaDeReboteEnemigo; // Fuerza del rebote del jugador
    [SerializeField] private float fuerzaDeReboteProyectil; //Fuerza del rebote del jugador



    public AudioClip SaltoFX;

    [Header("Animation")]
    private Animator animator; //Animaciones

  //  [SerializeField] private ParticleSystem particulas;




    private void Start() //Se ejecuta una vez cuando el personaje est� en escena
    {

        rb2D = GetComponent<Rigidbody2D>(); //Toma el RigidBody que tiene nuestro personaje
        animator = GetComponent<Animator>(); //Inicializa las animaciones
    }
    private void Update() //Se ejecuta en ciclo una vez por cuadro
    {
        movimientoHorizontal = Input.GetAxisRaw("Horizontal") * velocidadDeMovimiento; //Tomamos los controles del jugador, toma la direcci�n del control seg�n las flechas del teclado

        animator.SetFloat("Horizontal", Mathf.Abs(movimientoHorizontal)); //Damos a entender que es horizontal, siendo la transici�n de las animaciones de idle y correr


        if (Input.GetButtonDown("Jump"))
        {
            //Si apretamos la tecla para saltar, el personaje saltar�, d�ndolo en verdadero

            salto = true;

            AudioManager.Instance.ReproducirSonido(SaltoFX);

           // particulas.Play();


        }
    }

    private void FixedUpdate() //Permite ejecutar las f�sicas de la misma manera para equipos potentes y no tan potentes
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo); //Estamos en el suelo, mientras que la caja toque algo que sea suelo
        animator.SetBool("enSuelo", enSuelo); //Hacemos que el personaje cuando salte cambie otra vez a la animaci�n idle y viceversa
        //Mover personaje

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto); //Hace que el personaje se mueva en el tiempo seg�n si es un equipo r�pido y lento

        salto = false;  //Hacemos que no siempre se pueda saltar

    }

    private void Mover(float mover, bool saltar) //Mover personaje y hacer que salte
    {
        //Ponemos las funciones del movimiento

        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.velocity.y); //No se altera la velocidad cuando se salte o se mueva el personaje
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento); //Le da un suavizado cuando nuestro personaje celere o frene teniendo en cuenta la velocidad en la que estamos, donde se quiere llegar y que tan r�pido 

        if (mover > 0 && !mirandoDerecha)
        {
            //Hacer que el personaje gire donde deseemos, si se mueve a la derecha o a la izquierda

            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            //Girar

            Girar();
        }

        if (enSuelo && saltar) //Si nuestro personaje est� en el suelo y presionamos saltar, saltar�
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));

        }
    }

   
    public void Jump()
    {
        salto = true;

        AudioManager.Instance.ReproducirSonido(SaltoFX);

    }

    private void Girar()
    {
        //Al hacer que el personaje gire, se multiplica por -1 la escala

        mirandoDerecha = !mirandoDerecha;
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + 180, 0);
        if (enSuelo)
        {
           // particulas.Play();
        }
    }



    private void OnDrawGizmos() //Permite ver la caja que creamos previamente
    {
        Gizmos.color = Color.yellow; //La caja originalmente est� invisible, esto hace que la pintemos para notarla
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja); //Mandamos la posicipon que queremos dibujar la caja y sus dimensiones
    }
}
