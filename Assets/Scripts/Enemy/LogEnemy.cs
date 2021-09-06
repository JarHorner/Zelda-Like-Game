using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : MonoBehaviour
{

    #region Variables
    public Transform spawnLocation;
    private Animator animator;
    private Transform target;
    private bool moving = false;
    [SerializeField] private AudioSource movementAudio;
    [SerializeField] private AudioSource hitAudio;
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
                followPlayer();
            }
            else if(Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                walkBack();
            }
        }
    }

    //when target gets in range, enemy moves to its loaction
    public void followPlayer() 
    {
        animator.SetBool("isMoving", true);
        //ensures only one exists and plays the movement sound.
        if (!moving)
        {
            movementAudio.Play();
            moving = true;
        }
        //ensures animations are working when sprite is moving
        animator.SetFloat("Horizontal", (target.position.x - transform.position.x));
        animator.SetFloat("Vertical", (target.position.y - transform.position.y));
        //walks enemy to target
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
    }

    //when target gets out of range, enemy moves back to its spawn loaction
    public void walkBack() 
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
            if (moving)
            {
                movementAudio.Stop();
                moving = false;
            }
            animator.SetBool("isMoving", false);
        }
    }

    //pushes the enemy a set amount when in contact with players sword.
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            hitAudio.Play();
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }

    #endregion
}
