using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region Variable
    public bool hit = false;
    [SerializeField] private AudioSource audioSource;
    //if bool is true, target will move within set area
    [SerializeField] private bool isMoving;

    [Header("If 'isMoving' is true, these fields need to be filled")]
    [SerializeField] private Transform farEnd;
    private Vector3 pointA;
    private Vector3 pointB;
    [SerializeField] private float SecondsForOneLength = 0.0f;
    #endregion

    #region Methods

    void Start() 
    {
        if (isMoving)
        {
            pointA = transform.position;
            pointB = farEnd.position;
        }
    }

    void Update() 
    {
        if (isMoving)
        {
            transform.position = Vector3.Lerp(pointA, pointB,
            Mathf.SmoothStep(0f, 1f, 
                Mathf.PingPong(Time.time/SecondsForOneLength, 1f)
            ) );
        }
        if (hit == true && !audioSource.isPlaying)
            audioSource.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Projectile")
        {
            audioSource.Play();
            hit = true;
        }
    }

    public bool Hit 
    {
        get { return hit; }
    }


    #endregion
}
