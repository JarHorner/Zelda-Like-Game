using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStartPoint : MonoBehaviour
{

    #region Variables
        private PlayerController player;
        private CameraController cam;
        public string pointName; 
        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Placing Player & Camera");
        //places player and camera according to the start point
        player = FindObjectOfType<PlayerController>();
        
        player.moveSpeed = 6f;
        
        if(player.startPoint == pointName)
        {
            player.transform.position = transform.position;

            cam = FindObjectOfType<CameraController>();
            cam.setTarget(player.transform);
            cam.setMinPosition(minPosition);
            cam.setMaxPosition(maxPosition);
        }
    }

    #endregion
}
