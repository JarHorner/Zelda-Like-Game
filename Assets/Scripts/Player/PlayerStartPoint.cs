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
        //places player and camera according to the start point
        player = FindObjectOfType<PlayerController>();
        
        player.moveSpeed = 6f;
        
        if(player.startPoint == pointName)
        {
            Debug.Log("Placing Player & Camera");
            player.transform.position = transform.position;

            cam = FindObjectOfType<CameraController>();
            cam.SetTarget(player.transform);
            cam.SetMinPosition(minPosition);
            cam.SetMaxPosition(maxPosition);
        }
    }

    #endregion
}
