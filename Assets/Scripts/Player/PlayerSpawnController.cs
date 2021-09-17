using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{

    #region Variables
    [SerializeField] private GameObject player;
    private HealthManager healthManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spawnLocation;
    private CameraController cam;
    #endregion

    #region Unity Methods  

    void Awake()
    {
        //if player does not exist, instantiates the player and sets up the camera. if he does finds him and attechs camera
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Spawning Player");
            player = Instantiate(playerPrefab);
            cam = FindObjectOfType<CameraController>();
            player.transform.position = spawnLocation.transform.position;
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            cam = FindObjectOfType<CameraController>();
        }
        healthManager = FindObjectOfType<HealthManager>();
        if (healthManager.revive == true) {
            player = GameObject.FindWithTag("Player");
            player.transform.position = spawnLocation.transform.position;
            cam = FindObjectOfType<CameraController>();
        }
        cam.setTarget(player.transform);
    }

    void Start() {
        //ensures player has full health again after dying
        if (healthManager.getCurrentHealth() <= 0) 
        {
            healthManager.setCurrentHealth(healthManager.getMaxHealth());
        }
    }

    #endregion
}
