using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    #region Variable
    private bool hit = false;
    [SerializeField] private AudioSource audioSource;
    #endregion

    #region Methods

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Projectile")
        {
            audioSource.Play();
            hit = true;
            audioSource.enabled = false;
        }
    }

    public bool Hit 
    {
        get { return hit; }
    }


    #endregion
}
