using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeFloor : MonoBehaviour
{
    #region Variables
    private PlayerController player;
    [SerializeField] private Animator changeFloorTransition;
    private float transitionTime = 1.5f;
    [SerializeField] private AudioSource stairs;
    [SerializeField] private GameObject moveLocation;
    private CameraController cam;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;
    private bool changingFloor = false;

    #endregion

    #region Methods

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        if (changingFloor)
        {
            transitionTime -= Time.deltaTime;
            if (transitionTime <= 0f)
            {
                player.transform.position = moveLocation.transform.position;
                //ensures camera follows to players moved position
                cam = FindObjectOfType<CameraController>();
                cam.MinPosition = minPosition;
                cam.MaxPosition = maxPosition;
                changeFloorTransition.ResetTrigger("Start");

                changeFloorTransition.SetTrigger("End");
                changingFloor = false;
                transitionTime = 1.5f;
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player")
        {
            changingFloor = true;
            changeFloorTransition.SetTrigger("Start");
            stairs.Play();
        }
    }

    #endregion
}
