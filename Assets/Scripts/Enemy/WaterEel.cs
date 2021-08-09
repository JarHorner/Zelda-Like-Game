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
    private float timeToAttack = 2.2f;
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
        //depending on where the target is, the enemy will either follow or walk back
        if (target != null)
        {
            if (Vector3.Distance(target.position, transform.position) <= maxRange && Vector3.Distance(target.position, transform.position) >= minRange) 
            {
                animator.SetBool("PlayerIsClose", true);
                if (timeToAttack <= 0) {
                    shoot();
                    timeToAttack = 2.2f;
                }
                timeToAttack -= Time.deltaTime;
            }
            if(Vector3.Distance(target.position, transform.position) >= maxRange)
            {
                animator.SetBool("PlayerIsClose", false);
                timeToAttack = 2.2f;
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
        AnimatorClipInfo[] currentClipInfo = animator.GetCurrentAnimatorClipInfo(0);

        int w = animator.GetCurrentAnimatorClipInfo(0).Length;
        string[] clipName = new string[w];
        for (int i = 0; i < w; i += 1)
        {      
            clipName[i] = animator.GetCurrentAnimatorClipInfo(0)[i].clip.name;
            Debug.Log(clipName[i]);
        }
        Debug.Log(projectileSpawnLoc.position);

        if (currentClipInfo[1].clip.name == "Eel_Attack_Left" && currentClipInfo[0].clip.name != "Eel_Attack_Down" 
                && currentClipInfo[0].clip.name != "Eel_Attack_Up")
        {
            if (projectileSpawnLoc.localPosition.x == 0f)
                projectileSpawnLoc.localPosition = new Vector3(-0.55f, 1.3f, 0f);
            Debug.Log("Changed Left");
        }
        else if (currentClipInfo[1].clip.name == "Eel_Attack_Right" && currentClipInfo[0].clip.name != "Eel_Attack_Down" 
                && currentClipInfo[0].clip.name != "Eel_Attack_Up")
        {
            projectileSpawnLoc.localPosition = new Vector3(0.55f, 1.3f, 0f);
            Debug.Log("Changed Right");
        }
        else if (currentClipInfo[0].clip.name == "Eel_Attack_Down" || currentClipInfo[0].clip.name == "Eel_Attack_Up")
        {
            if (projectileSpawnLoc.localPosition.x == -0.55f || projectileSpawnLoc.localPosition.x == 0.55f)
            {
                projectileSpawnLoc.localPosition = new Vector3(0f, 1.3f, 0f);
                Debug.Log("back to center");
            }
        }
        Instantiate(bulletPrefab, projectileSpawnLoc.position, transform.rotation);
    }

    #endregion
}
