
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HurtPlayer : MonoBehaviour
{

    #region Variables
        [SerializeField] private Knockback knockback;
        private Enemy enemy;
        private float waitToHurt = 1.5f;
        private bool isTouching;
        //in code, eventually set to 1f.
        private float waitToHit = 0.0f;
        private HealthManager playerHealthManager;

    #endregion

    #region Unity Methods

    void Start()
    {
        playerHealthManager = FindObjectOfType<HealthManager>();
        enemy = GetComponent<Enemy>();
    }

    void Update()
    {
        //if true, counts down to hurt player again. used so damage is not continuous when enemy is pressed against player.
        if (isTouching) {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                playerHealthManager.DamagePlayer(enemy.BaseAttack);
                waitToHurt = 1.5f;
            }
        }
        if (waitToHit > 0)
            waitToHit -= Time.deltaTime;
    }

    //when collided with player, HurtPlayer function is called (see function in HealthManager script)
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //Placed so player can't repeadly run back and forth into enemy and take damage. gives some leeway.
            if (waitToHit <= 0)
            {
                Debug.Log("Hit");
                knockback.PushBack(this.transform, other.transform.parent.GetComponent<Rigidbody2D>());
                playerHealthManager.DamagePlayer(enemy.BaseAttack);

                waitToHit = 1f;
            }
        }

    }

    //maintains consistant damage when pressed against player
    void OntriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouching = true;
        }
    }

    //needed to reset the consistant damage timer, so it always remains the same
    void OnCtriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            isTouching = false;
            waitToHurt = 1.5f;
        }
    }

    #endregion
}
