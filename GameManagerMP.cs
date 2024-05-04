using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class GameManagerMP : MonoBehaviourPunCallbacks
{
    public static GameManagerMP Instance { get; private set; }

    public HUDMP hud;

    public int PuntosTotales { get; set; }

    public int vidas;


   

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Debug.Log("Cuidado! Mas de un GameManager en escena.");
            Destroy(gameObject);
        }
    }

    [PunRPC]
    public void RPC_SumarPuntos(int puntosASumar)
    {
        PuntosTotales += puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);
    }

    public void SumarPuntos(int puntosASumar)
    {
       // PuntosTotales += puntosASumar;
       // hud.ActualizarPuntos(PuntosTotales);

        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_SumarPuntos", RpcTarget.All, puntosASumar);
        }

    }
        

    public int ObtenerVidas()
    {
        return vidas;
    }

    public void PerderVida()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 1");
        }

        hud.DesactivarVida(vidas);
    }

    [PunRPC]
    public void RPC_PerderVidaMP()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            photonView.RPC("CambiarEscenaGameOver", RpcTarget.All);
        }

        hud.DesactivarVida(vidas);
    }

    [PunRPC]
    private void CambiarEscenaGameOver()
    {
        // Se detiene la sincronización de la red
        PhotonNetwork.DestroyPlayerObjects(PhotonNetwork.LocalPlayer);
        PhotonNetwork.LeaveRoom();

        // Se carga la escena de Game Over
        SceneManager.LoadScene("GameOverMP");
    }

    public void PerderVidaMP()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_PerderVidaMP", RpcTarget.All);
        }
    }

    public void PerderVidaLvl1Guest()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 1Guest");
        }

        hud.DesactivarVida(vidas);
    }

    public void PerderVidaLvl2Guest()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 6Guest");
        }

        hud.DesactivarVida(vidas);
    }

    [PunRPC]
    public bool RPC_RecuperarVida()
    {
        if (vidas == 3)
        {
            return false;
        }

        hud.ActivarVida(vidas);
        vidas += 1;
        return true;
    }

    public bool RecuperarVida()
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_RecuperarVida", RpcTarget.All);
            return true;
        }
        else
        {
            return false; // Otra opción podría ser lanzar una excepción o realizar otra acción apropiada aquí.
        }
    }

    [PunRPC]
    public void RPC_MultiplicarPuntos(int puntosASumar) //Powerup de la bosa que duplica los puntos actuales y los actualiza
    {
        PuntosTotales *= 2 * puntosASumar;
        hud.ActualizarPuntos(PuntosTotales);

        if (!hud.EstaParpadeando())
        {
            hud.IniciarParpadeo();
        }
        else
        {
            hud.RestartParpadeo();
        }
    }

    public void MultiplicarPuntos(int puntosASumar)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("RPC_MultiplicarPuntos", RpcTarget.All, puntosASumar);
        }
    }

    public void LineaMuerte() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 1");
    }

    public void LineaMuerte1Guest() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 1Guest");
    }

    public void LineaMuerte2Guest() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 6Guest");
    }

    public void NextLevel()
    {
        SceneManager.LoadScene("Escenario2");
    }

    public void Level1()
    {
        SceneManager.LoadScene("Escenario1");
    }

    public void Level1Guest()
    {
        SceneManager.LoadScene("Escenario1Guest");
    }

    public void Level2Guest()
    {
        SceneManager.LoadScene("Escenario6Guest");
    }

    public void Level2()
    {
        SceneManager.LoadScene("Escenario2");
    }

    public void Level3()
    {
        SceneManager.LoadScene("Escenario3");
    }

    public void Level4()
    {
        SceneManager.LoadScene("Escenario4");
    }

    public void Level5()
    {
        SceneManager.LoadScene("Escenario5");
    }

    public void Level6()
    {
        SceneManager.LoadScene("Escenario6");
    }

    public void Felicitaciones()
    {
        SceneManager.LoadScene("Felicitaciones");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void LineaMuerte2() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 2");
    }


    public void PerderVidaTutorial()
    {
        vidas -= 1;

        if (vidas == 0)
        {

            SceneManager.LoadScene(SceneManager.GetActiveScene().name); //Reiniciar la escena activa del juego
        }

        hud.DesactivarVida(vidas);
    }

    public void PerderVida2()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 2");
        }

        hud.DesactivarVida(vidas);
    }

    public void LineaMuerte3() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 3");
    }

    public void LineaMuerte4() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 4");
    }

    public void LineaMuerte5() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 5");
    }

    public void LineaMuerte6() //Limite del vacío al tocarlo para reiniciar el nivel
    {
        SceneManager.LoadScene("gameover 6");
    }

    public void PerderVida3()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 3");
        }

        hud.DesactivarVida(vidas);
    }

    public void PerderVida4()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 4");
        }

        hud.DesactivarVida(vidas);
    }

    public void PerderVida5()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 5");
        }

        hud.DesactivarVida(vidas);
    }

    public void PerderVida6()
    {
        vidas -= 1;

        if (vidas == 0)
        {
            // Reiniciamos el nivel.
            SceneManager.LoadScene("gameover 6");
        }

        hud.DesactivarVida(vidas);
    }
}