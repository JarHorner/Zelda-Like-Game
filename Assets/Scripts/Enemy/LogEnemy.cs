using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogEnemy : MonoBehaviour
{

    #region Variables
    public Transform spawnLocation;
    public int expValue = 10;
    private Animator animator;
    private Transform target;
    private Vector3 startingCoordinates;
    [SerializeField] private float speed = 0f;
    [SerializeField] private float maxRange = 0f;
    [SerializeField] private float minRange = 0f;
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startingCoordinates = this.transform.position;
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
            animator.SetBool("isMoving", false);
        }
    }

    //used in AreaTransitions script to move enemies back to original position
    public Vector3 getStartingCoordinates() 
    {
        return startingCoordinates;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Sword")
        {
            Vector2 difference = transform.position - other.transform.position;
            transform.position = new Vector2(transform.position.x + difference.x, transform.position.y + difference.y);
        }
    }

    #endregion
}
