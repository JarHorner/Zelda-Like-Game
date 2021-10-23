using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitions : MonoBehaviour
{

    #region Variables
    //used to keep track of the players last location
    [SerializeField] private static Vector2 lastPlayerLocation;
    private CameraController cam;
    private GameObject[] enemies;
    private GameManager gameManager;
    [SerializeField] private Vector2 newMinPosition;
    [SerializeField] private Vector2 newMaxPosition;
    [SerializeField] private Vector3 movePlayer;


    #endregion

    #region Unity Methods

    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameManager = FindObjectOfType<GameManager>();
    }

    //snaps camera to new location, moves player into boxed area and resets any enemies 
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player") 
        {
            //changes camera min/max positions to simulate move somewhere new
            cam.SetMinPosition(newMinPosition);
            cam.SetMaxPosition(newMaxPosition);
            //moves player into area
            collider.transform.position += movePlayer;
            lastPlayerLocation = collider.transform.position;

            GameObject[] droppedItems = GameObject.FindGameObjectsWithTag("Item");
            foreach (var item in droppedItems)
            {
                Destroy(item);
            }

            //if the transition is in a dungeon, the enemies are not "respawned".
            if (!gameManager.CurrentScene.Contains("Dungeon"))
            {
                //resets enemies that are out of place
                if (enemies != null)
                {
                    foreach (GameObject go in enemies)
                    {
                        EnemyHealthManager eHealthMan = go.gameObject.GetComponent<EnemyHealthManager>();
                        Vector3 cords = eHealthMan.getStartingCoordinates();
                        go.transform.position = cords;
                        go.gameObject.SetActive(true);
                        eHealthMan.setCurrentHealth(eHealthMan.getMaxHealth());
                    }
                }
            }
        }
    }

    public Vector2 LastPlayerLocation
    {
        get { return lastPlayerLocation; }
    }

    #endregion
}
