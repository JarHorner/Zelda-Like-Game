using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : MonoBehaviour
{

    #region Variables
    [SerializeField] private Transform spawnLocation;
    private Animator animator;
    private Transform target;
    private bool isMoving = false;
    [SerializeField] private AudioSource movementAudio;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxRange = 0f;
    [SerializeField] private float minRange = 0f;
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
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
        //ensures animations are working when sprite is moving
        animator.SetFloat("Horizontal", (target.position.x - transform.position.x));
        animator.SetFloat("Vertical", (target.position.y - transform.position.y));
        //walks enemy to target
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    //when target gets out of range, enemy moves back to its spawn loaction
    public void WalkBack() 
    {
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
            animator.SetBool("isMoving", false);
        }
    }

    #endregion
}
