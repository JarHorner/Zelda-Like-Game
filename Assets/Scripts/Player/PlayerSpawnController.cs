using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSpawnController : MonoBehaviour
{

    #region Variables
    [SerializeField] private GameObject player;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spawnLocation;
    private PlayerController playerController;
    private CameraController cam;
    [SerializeField] private string placeName;
    private LocationCanvas locationCanvas;
    private bool justSpawned = true;
    #endregion

    #region Unity Methods  

    //if player does not exist, instantiates the player and sets up the camera. if he does exist, finds him and attachs the camera.
    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Spawning Player");
            PlayerController.PlayerExists = false;
            player = Instantiate(playerPrefab);
            cam = FindObjectOfType<CameraController>();
        }
        else
        {
            Debug.Log("Player Found");
            player = GameObject.FindWithTag("Player");
            cam = FindObjectOfType<CameraController>();
        }

        if (!SaveSystem.LoadedGame)
        {
            player.transform.position = spawnLocation.transform.position;
        }
        else
        {
            Vector2 newLocation = new Vector2(SaveSystem.currentPlayerData.lastPosition[0], SaveSystem.currentPlayerData.lastPosition[1]);
            player.transform.position = newLocation;
        }
        playerController = player.GetComponent<PlayerController>();
        //if player died, and is reviving
        if (playerController.IsReviving == true) {
            player.transform.position = spawnLocation.transform.position;
            cam = FindObjectOfType<CameraController>();
        }
        //provides camera with target
        cam.SetTarget(player.transform);
    }
    
    private void Update() 
    {
        //Displays name of area on UI.
        if (justSpawned)
        {
            locationCanvas = GameObject.FindWithTag("DialogCanvas").GetComponent<LocationCanvas>();
            StartCoroutine(locationCanvas.PlaceNameCo(placeName));
            justSpawned = false;
        }

        if (playerController.IsReviving == true)
        {
            HealthVisual.healthSystemStatic.Heal(12);
            playerController.IsReviving = false;
        }
    }

    #endregion
}
