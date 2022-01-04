using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthSpikes : MonoBehaviour
{

    #region Variables
    [SerializeField] private Animator animator;
    private float timeRisen = 1f;

    #endregion

    #region Methods

    void Update() 
    {
        timeRisen -= Time.deltaTime;
        if (timeRisen <= 0f)
        {
            animator.SetBool("Sink", true);
        }
    }

    #endregion
}
