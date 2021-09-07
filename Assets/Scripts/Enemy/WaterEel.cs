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
    private Transform projectileSpawnLoc;
    public GameObject bulletPrefab;
    [SerializeField] private AudioSource movementSound;
    [SerializeField] private AudioSource shootingSound;
    private bool hasRisen = false;
    private float timeToAttack = 2.15f;
    [SerializeField] private float maxRange = 0f;
    [SerializeField] private float minRange = 0f;

    #endregion
    
    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        projectileSpawnLoc = this.transform.Find("Projectile");
    }

    // Update is called once per frame
    void Update()
    {
        target = FindObjectOfType<PlayerController>().transform;
        StartCoroutine(trackPlayer());
        //depending on where the target is, the enemy will either rise and start shooting, or sink and wait
        if (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange) 
            {
                animator.SetBool("PlayerIsClose", true);
                if (!hasRisen)
                {
                    movementSound.Play();
                    hasRisen = true;
                }
                if (timeToAttack <= 0) {
                    shoot();
                    timeToAttack = 2.15f;
                }
                timeToAttack -= Time.deltaTime;
            }
            if(Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                animator.SetBool("PlayerIsClose", false);
                if (hasRisen)
                {
                    movementSound.Play();
                    hasRisen = false;
                }
                timeToAttack = 2.15f;
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
        bulletPrefab.GetComponent<SpriteRenderer>().sortingOrder = 2;
        SpriteRenderer sprite = this.GetComponent<SpriteRenderer>();

        //ensures that the bullet goes behind the eel sprite that shots upwards
        if (sprite.sprite.name == "Water_Eel_3")
        {
            bulletPrefab.GetComponent<SpriteRenderer>().sortingOrder = 0;
            Debug.Log("Bullet Behind");
        }
        shootingSound.Play();
        Instantiate(bulletPrefab, projectileSpawnLoc.position, projectileSpawnLoc.rotation);
    }

    #endregion
}
