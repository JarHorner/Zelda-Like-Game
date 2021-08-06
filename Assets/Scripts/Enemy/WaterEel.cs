using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterEel : MonoBehaviour
{
    #region Variables
    public Transform spawnLocation;
    private Animator animator;
    private Transform target;
    private GameObject bullet;
    public GameObject bulletPrefab;
    private float timeToAttack = 1.7f;
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
        StartCoroutine(trackPlayer());
        //depending on where the target is, the enemy will either follow or walk back
        if (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange) 
            {
                animator.SetBool("PlayerIsClose", true);
                if (timeToAttack <= 0) {
                    shoot();
                    timeToAttack = 2f;
                }
                timeToAttack -= Time.deltaTime;
            }
            if(Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                animator.SetBool("PlayerIsClose", false);
                timeToAttack = 1.7f;
            }
        }
    }

    //when target gets in range, enemy shoots at targets location
    IEnumerator trackPlayer() 
    {
        //ensures animations are working when sprite is moving
        animator.SetFloat("Horizontal", (target.position.x - transform.position.x));
        animator.SetFloat("Vertical", (target.position.y - transform.position.y));
        yield return null;
    }

    public void shoot() {
        Instantiate(bulletPrefab, transform.position, transform.rotation);
    }

    #endregion
}
