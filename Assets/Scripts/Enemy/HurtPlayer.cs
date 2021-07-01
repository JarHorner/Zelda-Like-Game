using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{

    #region Variables
        [SerializeField] private int damageDealt;
        private float waitToHurt = 1.5f;
        private bool isTouching;
        private float waitToHit = 0.0f;
        private HealthManager healthManager;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //if true, counts down to hurt player again. used so damage is not continuous
        if (isTouching) {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                healthManager.HurtPlayer(damageDealt);
                waitToHurt = 1.5f;
            }
        }
        waitToHit -= Time.deltaTime;
    }

    //when collided with player, HurtPlayer function is called (see function in HealthManager script)
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (waitToHit <= 0)
            {
                Debug.Log("Hit");
                healthManager.HurtPlayer(damageDealt);
                waitToHit = 1f;
            }
        }

    }

    //maintains consistant damage when pressed against player
    void OnCollisionStay2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouching = true;
        }
    }

    //needed to reset the consistant damage timer, so it always remains the same
    void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouching = false;
            waitToHurt = 1.5f;
        }
    }

    #endregion
}
