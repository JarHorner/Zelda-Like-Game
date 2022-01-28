using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockEnemy : MonoBehaviour
{

    #region Variables
    [SerializeField] private Transform spawnLocation;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform target;
    private bool isMoving = false;
    private float awakenAnimTime = 1f;
    [SerializeField] private AudioSource movementAudio;

    //these three varibles can be adjusted at any time
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxRange = 0f;
    [SerializeField] private float minRange = 0f;
    #endregion

    #region Unity Methods

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        target = FindObjectOfType<PlayerController>().transform;

        //depending on where the target is, the enemy will either follow or walk back
        if (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange) 
            {
                FollowPlayer();
            }
            //this prevents WalkBack() from being used every frame even with enemy in same place.
            else if (this.transform.position == spawnLocation.position)
            {
                animator.SetBool("isMoving", false);
            }
            else if(Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                WalkBack();
            }
        }
    }

    //when target gets in range, enemy moves to its loaction
    public void FollowPlayer() 
    {
        animator.SetBool("isMoving", true);
        //ensures only one exists and plays the movement sound.
        if (!isMoving)
        {
            movementAudio.Play();
            isMoving = true;
        }
        awakenAnimTime -= Time.deltaTime;
        if (awakenAnimTime <= 0f)
        {
            //ensures animations are working when sprite is moving
            animator.SetFloat("Horizontal", (target.position.x - transform.position.x));
            animator.SetFloat("Vertical", (target.position.y - transform.position.y));
            //walks enemy to target
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            AnimatorClipInfo[] clipInfo = animator.GetCurrentAnimatorClipInfo(0);
            if (clipInfo[0].clip.name == "Rock_Enemy_Walk_Down")
            {
                spriteRenderer.sortingLayerName = "Player";
                spriteRenderer.sortingOrder = 0;
            }
            else
            {
                spriteRenderer.sortingLayerName = "Enemy";
                spriteRenderer.sortingOrder = 1;
            }
        }
    }

    //when target gets out of range, enemy moves back to its spawn loaction
    public void WalkBack() 
    {
        animator.SetBool("isMoving", true);

        //ensures animations are working when sprite is moving
        animator.SetFloat("Horizontal", (spawnLocation.position.x - transform.position.x));
        animator.SetFloat("Vertical", (spawnLocation.position.y - transform.position.y));
        //walks enemy back
        transform.position = Vector3.MoveTowards(transform.position, spawnLocation.transform.position, speed * Time.deltaTime);
        //starts idle animation when destination is reached
        if (transform.position == spawnLocation.transform.position) 
        {
            //is moving is true when log reaches spawnpoint, makes it false and stops movement sound.
            if (isMoving)
            {
                movementAudio.Stop();
                isMoving = false;
            }
            awakenAnimTime = 1f;
            animator.SetBool("isMoving", false);
        }
    }

    #endregion
}
