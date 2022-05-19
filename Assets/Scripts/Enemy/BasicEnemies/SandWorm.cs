using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SandWorm : Enemy
{

    #region Variables
    [SerializeField] private List<GameObject> wormSprites;
    private GameObject head;
    private GameObject tail;
    [SerializeField] private Animator headAnimator;
    [SerializeField] private Animator tailAnimator;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 moveDirection;
    private Vector2 lastVelocity;
    private Vector3 lastTailPosition;
    private bool canMove = true;
    private bool tailChanging = false;
    private float hitWaitTime = 0.5f;
    [SerializeField] private float timeBetweenChangeDirectionCounter;
    #endregion

    #region Methods

    //sets up all state.
    void Start()
    {
        head = wormSprites[0];
        tail = wormSprites[wormSprites.Count - 1];
        Vector2 moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        moveDirection *= MoveSpeed;
        rb.velocity = moveDirection;

        lastTailPosition = tail.transform.position;

        headAnimator.SetFloat("Horizontal", moveDirection.x);
        headAnimator.SetFloat("Vertical", moveDirection.y);
        tailAnimator.SetFloat("Horizontal", moveDirection.x);
        tailAnimator.SetFloat("Vertical", moveDirection.y);
    }
    
    //starts to correct the tail if needed, also randomly changes the direction of the worm on a fixed time set. (makes the worm seem more lively)
    void Update() 
    {
        if (lastTailPosition == tail.transform.position && !tailChanging)
        {
            tailChanging = true;
            StartCoroutine(MoveTail());
        }
        else
        {
            lastTailPosition = tail.transform.position;
        }
        if (canMove)
        {
            timeBetweenChangeDirectionCounter -= Time.deltaTime;
            if(timeBetweenChangeDirectionCounter <= 0f)
            {
                moveDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
                moveDirection *= MoveSpeed;
                rb.velocity = moveDirection;
                headAnimator.SetFloat("Horizontal", moveDirection.x);
                headAnimator.SetFloat("Vertical", moveDirection.y);
                timeBetweenChangeDirectionCounter = 4;
            }
        } 
    }

    //waits 2 seconds before correcting the rotation of the tail
    IEnumerator MoveTail()
    {
        yield return new WaitForSeconds(2f);
        tailAnimator.SetFloat("Horizontal", moveDirection.x);
        tailAnimator.SetFloat("Vertical", moveDirection.y);
        tailChanging = false;
    }

    //ensure the body & tail of the worm move correctly behind the head.
    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
        if (canMove)
        {
            for (int i = 1; i < wormSprites.Count; i++)
            {
                int j = i;
                int partAhead = j--;

                if (Vector3.Distance(wormSprites[i].transform.position, wormSprites[j].transform.position) > 0.9f)
                {
                    wormSprites[i].transform.position = Vector3.MoveTowards(wormSprites[i].transform.position, wormSprites[j].transform.position, MoveSpeed * Time.deltaTime);
                }
            }
        }

        if (currentState == EnemyState.idle)
        {
            hitWaitTime -= Time.deltaTime;
            if (hitWaitTime <= 0f)
            {
                canMove = true;
                Debug.Log("Changing state to walk");
                currentState = EnemyState.walk;
                rb.velocity = lastVelocity;
                hitWaitTime = 0.5f;
            }
        }
    }

    //when the worm collides with select gameobjects, it will bounce in a correct physics direction.
    private void OnCollisionEnter2D(Collision2D collision) 
    {
        if (collision.gameObject.tag == "Walls" || collision.gameObject.tag == "Player")
        {
            var speed = lastVelocity.magnitude;
            var direction = Vector2.Reflect(lastVelocity.normalized, 
                                        collision.contacts[0].normal);
    
            rb.velocity = direction * Mathf.Max(speed, 2f);

            moveDirection = direction;

            headAnimator.SetFloat("Horizontal", moveDirection.x);
            headAnimator.SetFloat("Vertical", moveDirection.y);
        }
    }

    //when disables, all children are disabled as well
    private void OnDisable() 
    {
        Debug.Log("destory parent");
        Destroy(this.gameObject.transform.parent.gameObject);
    }

    #endregion
}
