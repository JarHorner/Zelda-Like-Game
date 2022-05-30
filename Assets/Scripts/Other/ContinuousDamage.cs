using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinuousDamage : MonoBehaviour
{
    #region Variables

    private PlayerController player;
    [SerializeField] private FloatValue damageDealt;
    //[SerializeField] private Knockback knockback;
    private bool isTouching;
    private float waitToHurt = 1f;

    //in code, eventually set to 1f.
    private float waitToHit = 0.0f;

    #endregion

    #region Methods
    void Start() 
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
        //if true, counts down to hurt player again. used so damage is not continuous when enemy is pressed against player.
        if (isTouching)
        {
            waitToHurt -= Time.deltaTime;
            if (waitToHurt <= 0)
            {
                player.DamagePlayer(damageDealt.InitalValue);
                waitToHurt = 1f;
            }
        }
        if (waitToHit > 0)
            waitToHit -= Time.deltaTime;
    }

    public void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "HitBox")
        {
            //Placed so player can't repeadly run back and forth into enemy and take damage. gives some leeway.
            if (waitToHit <= 0)
            {
                Debug.Log("Hit");
                player.DamagePlayer(damageDealt.InitalValue);
                Knockback.PushBack(this.transform, other.transform.parent.GetComponent<Rigidbody2D>());
                waitToHit = 1f;
            }
        }
    }

    //maintains consistant damage when pressed against player
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.tag == "HitBox")
        {
            isTouching = true;
        }
    }

    //needed to reset the consistant damage timer, so it always remains the same
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "HitBox")
        {
            isTouching = false;
            waitToHurt = 1f;
        }
    }

    #endregion
}
