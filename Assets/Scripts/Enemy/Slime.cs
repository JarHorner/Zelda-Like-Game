using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    #region Variables
    private Vector3 position;
    private Vector3 fallingPosition;
    private CircleCollider2D circleCollider;
    private CapsuleCollider2D capsuleCollider;
    private Rigidbody2D rb;
    private Animator animator;
    private float formAnimTime;
    private bool fallen;
    private bool falling;
    private bool hitMark;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        fallingPosition = new Vector3(position.x, position.y + 5, 0);
        circleCollider = GetComponent<CircleCollider2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        formAnimTime = 0.7f;
        fallen = false;
        falling = false;
        hitMark = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (falling)
        {
            float speed = 8f * Time.deltaTime;
            this.transform.position = Vector3.MoveTowards(this.transform.position, position, speed);
            if (transform.position == position)
            {
                Debug.Log("got here");
                falling = false;
                animator.SetTrigger("HitMark");
                hitMark = true;
                capsuleCollider.enabled = true;
            }
        }
        if (hitMark)
        {
            formAnimTime -= Time.deltaTime;
            if (formAnimTime <= 0f)
            {
                animator.SetTrigger("Recovered");
                hitMark = false;
            }
        }
        //if player is within certain range
        //slime will drop from high on y axis down and animate out
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Player" && !fallen)
        {
            fallen = true;
            this.transform.position = fallingPosition;
            animator.SetTrigger("Fall");
            falling = true;
            this.GetComponent<SpriteRenderer>().enabled = true;
        }
    }
    #endregion
}
