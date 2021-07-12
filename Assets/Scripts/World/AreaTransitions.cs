using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransitions : MonoBehaviour
{

    #region Variables
    private CameraController cam;
    private LogEnemy[] enemies;
    public Vector2 newMinPosition;
    public Vector2 newMaxPosition;
    public Vector3 movePlayer;


    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main.GetComponent<CameraController>();
        enemies = FindObjectsOfType<LogEnemy>();
    }

    //snaps camera to new location, moves player into boxed area and resets any enemies 
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player") 
        {
            //changes camera min/max positions to simulate move somewhere new
            cam.minPosition = newMinPosition;
            cam.maxPosition = newMaxPosition;
            //moves player into area
            collider.transform.position += movePlayer;
            //resets enemies that are out of place
            if (enemies != null)
            {
                foreach (LogEnemy go in enemies)
                {
                    EnemyHealthManager eHealthMan = go.gameObject.GetComponent<EnemyHealthManager>();
                    Vector3 cords = go.getStartingCoordinates();
                    go.transform.position = cords;
                    go.gameObject.SetActive(true);
                    eHealthMan.currHealth = eHealthMan.maxHealth;
                }
            }

        }
    }

    #endregion
}
