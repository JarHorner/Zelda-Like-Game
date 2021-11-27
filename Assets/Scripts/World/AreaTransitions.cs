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
    [SerializeField] private GameObject locationText;
    private TMP_Text placeText;
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
        if (collider.gameObject.tag == "Player" && collider.GetType() != typeof(BoxCollider2D)) 
        {
            //changes camera min/max positions to simulate move somewhere new
            cam.SetMinPosition(newMinPosition);
            cam.SetMaxPosition(newMaxPosition);
            //moves player into area
            collider.transform.position += movePlayer;
            lastPlayerLocation = collider.transform.position;

            if (needLocationText)
            {
                placeText = locationText.GetComponent<TMP_Text>();
                StartCoroutine(PlaceNameCo());
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

    private IEnumerator PlaceNameCo()
    {
        placeText.text = placeName;
        locationText.SetActive(true);
        yield return new WaitForSeconds(3f);
        locationText.SetActive(false);
    }

    public Vector2 LastPlayerLocation
    {
        get { return lastPlayerLocation; }
    }

    #endregion
}
