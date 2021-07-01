using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{

    #region Variables
        private PlayerController player;
        private CameraController cam;
        public string pointName; 
        public Vector2 minPosition;
        public Vector2 maxPosition;
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Placing Player & Camera");
        //places player and camera according to the start point
        player = FindObjectOfType<PlayerController>();

        if(player.startPoint == pointName)
        {
            player.transform.position = transform.position;

            cam = FindObjectOfType<CameraController>();
            cam.target = player.transform;
            cam.minPosition = minPosition;
            cam.maxPosition = maxPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #endregion
}
