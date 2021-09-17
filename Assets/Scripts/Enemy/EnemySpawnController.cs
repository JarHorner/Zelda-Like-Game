using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{

    #region Variables
    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject spawnLocation;
    private int? enemyID;
    #endregion

    #region Unity Methods

    //is called when the script instance is being loaded.
    void Awake() {
        //gets objects instance ID for easier spawning/resetting
        enemyID = this.transform.parent.GetInstanceID();
        //detatches the spawn of the enemy when loaded, so spawn location does not move with enemy 
        this.transform.parent = null;

        if (enemyID != null) 
        {
            //following code is used to automatically reset the positions of enemies when transitioning/changing scenes
            Object[] allObjects = Object.FindObjectsOfType<GameObject>();
            foreach (GameObject go in allObjects)
            {
                //checking for certain instance ID
                if (go.GetInstanceID() == enemyID) 
                {
                    enemy = go;
                    enemy.transform.position = spawnLocation.transform.position;
                    break;
                }
            }
        }
        else
        {
            //spawn enemy in scene and position them at spawn location
            enemy = Instantiate(enemyPrefab);
            enemy.transform.position = spawnLocation.transform.position;
        }
    }

    #endregion
}
