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
    private CameraController cam;
    [SerializeField] private string placeName;
    private DialogLocationCanvas locationCanvas;
    private bool justSpawned = true;
    #endregion

    #region Unity Methods  

    //if player does not exist, instantiates the player and sets up the camera. if he does exist, finds him and attachs the camera.
    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Spawning Player");
            player = Instantiate(playerPrefab);
            cam = FindObjectOfType<CameraController>();
            player.transform.position = spawnLocation.transform.position;
        }
        else
        {
            Debug.Log("Player Found");
            player = GameObject.FindWithTag("Player");
            cam = FindObjectOfType<CameraController>();
        }
        PlayerController playerController = player.GetComponent<PlayerController>();
        //if player died, and is reviving
        if (playerController.IsReviving == true) {
            player = GameObject.FindWithTag("Player");
            player.transform.position = spawnLocation.transform.position;
            cam = FindObjectOfType<CameraController>();
        }
        //provides camera with target
        cam.SetTarget(player.transform);
    }
    
    private void Update() 
    {
        if (justSpawned)
        {
            locationCanvas = GameObject.FindWithTag("DialogCanvas").GetComponent<DialogLocationCanvas>();
            StartCoroutine(locationCanvas.PlaceNameCo(placeName));
            justSpawned = false;
        }
    }

    #endregion
}
