using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AreaTransitions : MonoBehaviour
{

    #region Variables
    private CameraController cam;
    private Enemy[] enemies;
    private GameManager gameManager;
    private PlayerController player;
    [SerializeField] private Vector2 newMinPosition;
    [SerializeField] private Vector2 newMaxPosition;
    [SerializeField] private Vector3 movePlayer;
    [SerializeField] private bool needLocationText = false;
    [SerializeField] private string placeName;
    private LocationCanvas locationCanvas;
    private bool locationAppearing;
    private float animTime = 3f;
    private static bool textUp;
    #endregion

    #region Unity Methods

    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        enemies = GameObject.FindObjectsOfType<Enemy>();
        gameManager = FindObjectOfType<GameManager>();
        player = FindObjectOfType<PlayerController>();
        locationCanvas = GameObject.FindWithTag("DialogCanvas").GetComponent<LocationCanvas>();
    }

    void Update() 
    {
        //if location text is appearing, counts down the anim time to turn it off
        if (locationAppearing)
        {
            animTime -= Time.deltaTime;
            if (animTime <= 0)
            {
                locationCanvas.LocationTextAnim.SetBool("Fading", false);
                locationCanvas.LocationText.text = "";
                locationAppearing = false;
                animTime = 3f;
            }

        }
    }

    //snaps camera to new location, moves player into boxed area and resets any enemies 
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player" && collider.GetType() != typeof(BoxCollider2D))
        {
            //changes camera min/max positions to simulate move somewhere new
            cam.SetMinPosition(newMinPosition);
            cam.SetMaxPosition(newMaxPosition);
            //moves player into area
            collider.transform.position += movePlayer;
            player.LastPlayerLocation = collider.transform.position;

            //if area is changing, but location is already displaying, changes text to newer area
            if (textUp)
            {
                Debug.Log("Here");
                locationAppearing = false;
                locationCanvas.LocationTextAnim.SetBool("Fading", false);
                locationCanvas.LocationText.text = "";
                animTime = 3f;
            }
            //if area is changing, will display location text
            if (needLocationText && !locationAppearing)
            {
                locationCanvas.LocationText.text = placeName;
                locationCanvas.LocationTextAnim.SetBool("Fading", true);
                locationAppearing = true;
                textUp = true;
            }

            //Destorys items that were not picked up
            GameObject[] droppedItems = GameObject.FindGameObjectsWithTag("Item");
            foreach (var item in droppedItems)
            {
                Destroy(item);
            }

            if (gameManager == null)
                gameManager = FindObjectOfType<GameManager>();
            //if the transition is in a dungeon, the enemies are not "respawned".
            if (!gameManager.CurrentScene.Contains("Dungeon"))
            {
                //resets enemies that are out of place
                if (enemies != null)
                {
                    foreach (Enemy enemy in enemies)
                    {
                        EnemyHealthManager eHealthMan = enemy.gameObject.GetComponent<EnemyHealthManager>();
                        Vector3 cords = eHealthMan.StartingCoordinate;
                        enemy.transform.position = cords;
                        enemy.gameObject.SetActive(true);
                        enemy.Health = enemy.MaxHealth;
                    }
                }
            }
        }
    }

    #endregion
}
