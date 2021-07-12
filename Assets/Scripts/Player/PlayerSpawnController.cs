using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnController : MonoBehaviour
{

    #region Variables
    public GameObject player;
    private HealthManager healthManager;
    public GameObject playerPrefab;
    public GameObject spawnLocation;
    private CameraController cam;
    #endregion

    #region Unity Methods  

    void Awake()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Debug.Log("Spawning Player");
            player = Instantiate(playerPrefab);
            cam = FindObjectOfType<CameraController>();
            player.transform.position = spawnLocation.transform.position;
            cam.target = player.transform;
        }
        else
        {
            player = GameObject.FindWithTag("Player");
            cam = FindObjectOfType<CameraController>();
            cam.target = player.transform;
        }
        healthManager = FindObjectOfType<HealthManager>();
        if (healthManager.revive == true) {
            player = GameObject.FindWithTag("Player");
            player.transform.position = spawnLocation.transform.position;
            cam = FindObjectOfType<CameraController>();
            cam.target = player.transform;
        }
    }

    void Start() {
        //ensures player has full health again after dying
        if (healthManager.currHealth <= 0) 
        {
            healthManager.currHealth = healthManager.maxHealth;
        }
    }

    #endregion
}
