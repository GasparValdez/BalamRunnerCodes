using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BasuraCinematica : MonoBehaviour
{
   // Contar los objetos destruidos
    public int contadorBotellas = 0;
    public int contadorSacosBasura = 0;
    public int contadorOtrosObjetos = 0;

    //guardar en PlayerPrefs
    private const string BotellasKey = "BotellasDestruidas";
    private const string SacosBasuraKey = "SacosBasuraDestruidos";
    private const string OtrosObjetosKey = "OtrosObjetosDestruidos";

    private void Start()
    {
        // Al iniciar la scena lo cargamos
        contadorBotellas = PlayerPrefs.GetInt(BotellasKey, 0);
        contadorSacosBasura = PlayerPrefs.GetInt(SacosBasuraKey, 0);
        contadorOtrosObjetos = PlayerPrefs.GetInt(OtrosObjetosKey, 0);
    }


    // Metodos de incremento 
    public void IncrementarContadorBotellas()
    {
        contadorBotellas++;
        PlayerPrefs.SetInt(BotellasKey, contadorBotellas);
        PlayerPrefs.Save();
    }

    public void IncrementarContadorSacosaBasura()
    {
        contadorSacosBasura++;
        PlayerPrefs.SetInt(SacosBasuraKey, contadorSacosBasura);
        PlayerPrefs.Save();
    }

    public void IncrementarContadorOtrosObjetos()
    {
        contadorOtrosObjetos++;
        PlayerPrefs.SetInt(OtrosObjetosKey, contadorOtrosObjetos);
        PlayerPrefs.Save();
    }

    // Prueba de objetos
    public void MostrarConteo()
    {
        Debug.Log("Total de botellas destruidas: " + contadorBotellas);
        Debug.Log("Total de sacos de basura destruidos: " + contadorSacosBasura);
        Debug.Log("Total de otros objetos destruidos: " + contadorOtrosObjetos);
    }

    public void Reintentar()
    {
        // Eliminar las claves relacionadas con los contadores
        PlayerPrefs.DeleteKey(BotellasKey);
        PlayerPrefs.DeleteKey(SacosBasuraKey);
        PlayerPrefs.DeleteKey(OtrosObjetosKey);

        // Guardar los cambios
        PlayerPrefs.Save();
        SceneManager.LoadScene("Gameplay");
    }

    public void Menu()
    {
        // Eliminar las claves relacionadas con los contadores
        PlayerPrefs.DeleteKey(BotellasKey);
        PlayerPrefs.DeleteKey(SacosBasuraKey);
        PlayerPrefs.DeleteKey(OtrosObjetosKey);

        SceneManager.LoadScene("MainMenuGuest");
    }

}
