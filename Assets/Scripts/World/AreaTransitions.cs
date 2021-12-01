using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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
    [SerializeField] private bool needLocationText = false;
    [SerializeField] private string placeName;
    private DialogLocationCanvas locationCanvas;
    private bool locationAppearing;
    private float animTime = 3f;
    private static bool textUp;
    #endregion

    #region Unity Methods

    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        gameManager = FindObjectOfType<GameManager>();
        locationCanvas = GameObject.FindWithTag("DialogCanvas").GetComponent<DialogLocationCanvas>();
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
            lastPlayerLocation = collider.transform.position;

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
