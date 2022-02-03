using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    #region Variables
    //variables for movement
    [SerializeField] private float timeBetweenMove;
    private float timeBetweenMoveCounter;
    [SerializeField] private float timeToMove;
    private float timeToMoveCounter;
    private Vector2 moveDirection;
    private bool moving;
    private Rigidbody2D rb;
    private AudioSource audioSource;
    [SerializeField] private AudioClip movement;
    [SerializeField] private AudioClip fallingCollision;
    private Vector3 position;
    private Vector3 fallingPosition;
    private Animator animator;
    private float formAnimTime;
    private Transform target;
    [SerializeField] private float senseRange;
    private bool hitMark;
    private bool falling;
    private bool fallen;
    private bool canMove;
    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        position = transform.position;
        fallingPosition = new Vector3(position.x, position.y + 5, 0);
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        formAnimTime = 0.8f;
        hitMark = false;
        falling = false;
        fallen = false;
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
                    moveDirection = new Vector2(Random.Range(-1f, 1f) * MoveSpeed, Random.Range(-1f, 1f) * MoveSpeed).normalized;
                    audioSource.clip = movement;
                    audioSource.Play();
                    animator.SetFloat("Horizontal", (moveDirection.x - transform.position.x));
                    animator.SetFloat("Vertical", (moveDirection.y - transform.position.y));
                    animator.SetBool("IsMoving", true);
                }
            }
        } 
        else
        {
            target = FindObjectOfType<PlayerController>().transform;

            //set true when player enters sense distance, which drops the slime from a higher point and stops when it reaches its original position.
            if (Vector3.Distance(target.position, transform.position) <= senseRange && !fallen)
            {
                this.transform.position = fallingPosition;
                falling = true;
                fallen = true;
                animator.SetTrigger("Fall");
                this.GetComponent<SpriteRenderer>().enabled = true;
            }
            if (falling)
            {
                this.transform.position = Vector3.MoveTowards(this.transform.position, position, 8f * Time.deltaTime);
                if (transform.position == position)
                {
                    Debug.Log("got here");
                    animator.SetTrigger("HitMark");
                    audioSource.clip = fallingCollision;
                    audioSource.Play();
                    hitMark = true;
                    falling = false;
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
                    currentState = EnemyState.walk;
                }
            }
        }
    }
    #endregion
}
