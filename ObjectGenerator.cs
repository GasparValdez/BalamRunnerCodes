using Photon.Pun;
using UnityEngine;
using System.Collections;

public class ObjectGenerator : MonoBehaviourPunCallbacks
{
    public GameObject[] objectsToSpawn;
    public int numberOfObjectsToSpawn = 3; // Número de objetos a generar en cada iteración
    public float spawnInterval = 1.5f;
    public float generatorWidth = 15.10185f;
    public float spawnOffsetY = 2f;

    public AudioClip tickSound; // Sonido de tick durante el power-up
    public AudioClip powerUpEndSound; // Sonido al finalizar el power-up
    private AudioSource audioSource;

    // Variables para el power-up
    private bool powerUpActive = false;
    private float powerUpDuration = 10f; // Duración del power-up en segundos
    private float powerUpTimer = 0f;
    private float originalSpawnInterval;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (PhotonNetwork.IsMasterClient)
        {
            StartCoroutine(SpawnObjects());
        }
    }

    private IEnumerator SpawnObjects()
    {
        while (true)
        {
            yield return new WaitForSeconds(powerUpActive ? spawnInterval * 2f : spawnInterval);

            for (int i = 0; i < numberOfObjectsToSpawn; i++)
            {
                GameObject objectToSpawn = objectsToSpawn[Random.Range(0, objectsToSpawn.Length)];
                float spawnPositionX = transform.position.x + Random.Range(-generatorWidth / 2f, generatorWidth / 2f);
                float spawnPositionY = transform.position.y + spawnOffsetY;

                photonView.RPC("SpawnObjectRPC", RpcTarget.All, objectToSpawn.name, spawnPositionX, spawnPositionY);
            }
        }
    }

    private void Update()
    {
        if (powerUpActive)
        {
            powerUpTimer -= Time.deltaTime;
            if (powerUpTimer <= 0f)
            {
                DeactivatePowerUp();
            }
            else
            {
                // Reproducir sonido de tick cada segundo durante el power-up
                if (Mathf.FloorToInt(powerUpTimer + Time.deltaTime) != Mathf.FloorToInt(powerUpTimer))
                {
                    if (audioSource != null && tickSound != null && photonView.IsMine)
                    {
                        audioSource.PlayOneShot(tickSound);
                    }
                }
            }
        }
    }

    [PunRPC]
    private void SpawnObjectRPC(string objectName, float posX, float posY)
    {
        GameObject objectToSpawn = FindObjectByName(objectName);
        if (objectToSpawn != null)
        {
            GameObject spawnedObject = Instantiate(objectToSpawn, new Vector3(posX, posY, 0), Quaternion.identity);
            if (powerUpActive)
            {
                // Aplicar efecto de caída lenta a los objetos generados durante el power-up
                Rigidbody2D rb = spawnedObject.GetComponent<Rigidbody2D>();
                if (rb != null)
                {
                    rb.gravityScale = 0.3f; // Ajusta el valor según lo lento que quieras que caigan los objetos durante el power-up
                }
            }
        }
    }

    private GameObject FindObjectByName(string name)
    {
        foreach (GameObject obj in objectsToSpawn)
        {
            if (obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }

    // Método para activar el power-up
    public void ActivatePowerUp()
    {
        if (!powerUpActive)
        {
            powerUpActive = true;
            powerUpTimer = powerUpDuration;
            originalSpawnInterval = spawnInterval;
            spawnInterval /= 2f; // Modificar la frecuencia de generación durante el power-up

            // Reproducir sonido de inicio del power-up en el cliente local
            if (audioSource != null && tickSound != null && photonView.IsMine)
            {
                audioSource.PlayOneShot(tickSound);
            }

            photonView.RPC("SyncPowerUpState", RpcTarget.Others, true);
        }
    }

    // Método para desactivar el power-up
    private void DeactivatePowerUp()
    {
        if (powerUpActive)
        {
            powerUpActive = false;
            spawnInterval = originalSpawnInterval;
            if (audioSource != null && powerUpEndSound != null && photonView.IsMine)
            {
                // Reproducir sonido al finalizar el power-up en el cliente local
                audioSource.PlayOneShot(powerUpEndSound);
            }
            photonView.RPC("SyncPowerUpState", RpcTarget.Others, false);
        }
    }

    // RPC para sincronizar el estado del power-up
    [PunRPC]
    private void SyncPowerUpState(bool isActive)
    {
        if (isActive)
        {
            ActivatePowerUp();
        }
        else
        {
            DeactivatePowerUp();
        }
    }
}
