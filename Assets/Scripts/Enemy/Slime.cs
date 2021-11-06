using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : MonoBehaviour
{
    #region Variables
    //variables for movement
    [SerializeField] private float timeBetweenMove;
    private float timeBetweenMoveCounter;
    [SerializeField] private float timeToMove;
    private float timeToMoveCounter;
    private Vector2 moveDirection;
    private bool moving;
    [SerializeField] private float moveSpeed;

    private Rigidbody2D rb;
    private Vector3 position;
    private Vector3 fallingPosition;
    private CapsuleCollider2D capsuleCollider;
    private Animator animator;
    private float formAnimTime;
    private bool fallen;
    private bool falling;
    private bool hitMark;
    private bool canMove;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        fallingPosition = new Vector3(position.x, position.y + 5, 0);
        rb = GetComponent<Rigidbody2D>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        formAnimTime = 0.8f;
        fallen = false;
        falling = false;
        hitMark = false;
        canMove = false;
    }

    void Update()
    {
        //if slime can move, it will move in random directions
        if (canMove)
        {
            if (moving)
            {
                timeToMoveCounter -= Time.deltaTime;
                rb.velocity = moveDirection;
                //transform.position = Vector3.MoveTowards(transform.position, moveDirection, moveSpeed * Time.deltaTime);
                //rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.fixedDeltaTime);
                if(timeToMoveCounter <= 0f)
                {
                    moving = false;
                    animator.SetBool("IsMoving", false);
                    timeBetweenMoveCounter = timeBetweenMove;
                }
            } 
            else {
                timeBetweenMoveCounter -= Time.deltaTime;
                rb.velocity = Vector2.zero;
                if (timeBetweenMoveCounter <= 0f)
                {
                    moving = true;
                    timeToMoveCounter = timeToMove;
                    moveDirection = new Vector2(Random.Range(-1f, 1f) * moveSpeed, Random.Range(-1f, 1f) * moveSpeed).normalized;
                    animator.SetFloat("Horizontal", (moveDirection.x - transform.position.x));
                    animator.SetFloat("Vertical", (moveDirection.y - transform.position.y));
                    animator.SetBool("IsMoving", true);
                }
            }
        } 

        //set true when player enters trigger area, which drops the slime from a higher point and stops when it reaches its original position.
        if (falling)
        {
            this.transform.position = Vector3.MoveTowards(this.transform.position, position, 8f * Time.deltaTime);
            if (transform.position == position)
            {
                Debug.Log("got here");
                falling = false;
                animator.SetTrigger("HitMark");
                hitMark = true;
            }
        }
        //set true when slime hits original position, waiting until animation is done, then enables collider and movement.
        if (hitMark)
        {
            formAnimTime -= Time.deltaTime;
            if (formAnimTime <= 0f)
            {
                animator.SetTrigger("Recovered");
                hitMark = false;
                canMove = true;
            }
        }
    }

    //when player enters detection area, the slime model is moved and starts to fall from the ceiling.
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        
    }
    #endregion
}
