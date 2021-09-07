using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Volume2D : MonoBehaviour
{
    #region Variables
    private Transform listenerTransform;
    public AudioSource audioSource;
    public float minDist = 0.5f;
    public float maxDist = 12f;

    public float maxVolume;

    #endregion

    #region Unity Methods
    void start()
    {
        listenerTransform = FindObjectOfType<PlayerController>().transform;
    }

    // ensures volume of a sound drops off according to the distance between sound and player.
    void Update()
    {
        listenerTransform = FindObjectOfType<PlayerController>().transform;
        float dist = Vector3.Distance(transform.position, listenerTransform.position);
 
        if(dist < minDist)
        {
            audioSource.volume = maxVolume;
        }
        else if(dist > maxDist)
        {
            audioSource.volume = 0;
        }
        else
        {
            audioSource.volume = maxVolume - ((dist - minDist) / (maxDist - minDist));
        }
    }
    #endregion
}
