using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerSpawnController : MonoBehaviour
{

    #region Variables
    [SerializeField] private GameObject player;
    private HealthManager healthManager;
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private GameObject spawnLocation;
    private CameraController cam;
    [SerializeField] private string placeName;
    public DialogLocationCanvas locationCanvas;
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
        healthManager = FindObjectOfType<HealthManager>();
        //if player died, and is reviving
        if (healthManager.revive == true) {
            player = GameObject.FindWithTag("Player");
            player.transform.position = spawnLocation.transform.position;
            cam = FindObjectOfType<CameraController>();
        }
        //provides camera with target
        cam.SetTarget(player.transform);
    }

    void Start() {
        //ensures player has full health again after dying
        if (healthManager.CurrHealth <= 0) 
        {
            healthManager.CurrHealth = healthManager.MaxHealth;
        }

        locationCanvas = GameObject.FindWithTag("DialogCanvas").GetComponent<DialogLocationCanvas>();
        StartCoroutine(PlaceNameCo());
    }

    private IEnumerator PlaceNameCo()
    {
        locationCanvas.LocationText.text = placeName;
        locationCanvas.LocationText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        locationCanvas.LocationText.gameObject.SetActive(false);
    }

    #endregion
}
