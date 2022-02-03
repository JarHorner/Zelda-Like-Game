using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockGolem : MonoBehaviour
{
    #region Variables
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Transform target;

    //starts off at zero so he attacks upon sight, changed in code after first attack
    private float comboTimer = 2f;
    // private bool isMoving = false;
    [SerializeField] private AudioSource movementAudio;
    [SerializeField] private EnemyHealthManager enemyHealthManager;
    [SerializeField] private GameObject earthSpikes;
    private Vector3 earthSpikeLocation;

    [Header("these three varibles can be adjusted at any time")]
    // [SerializeField] private float speed = 0f;
    [SerializeField] private float spawnRange = 0f;
    [SerializeField] private float groundPoundMaxRange = 0f;
    [SerializeField] private float groundPoundMinRange = 0f;
    [SerializeField] private float comboMaxRange = 0f;
    [SerializeField] private float comboMinRange = 0f;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (EnteredRoom())
        {
            if (target != null)
            {
                if (Vector3.Distance(target.position, transform.position) <= groundPoundMaxRange && Vector3.Distance(target.position, transform.position) >= groundPoundMinRange) 
                {
                    comboTimer -= Time.deltaTime;
                    if (comboTimer <= 0f)
                    {
                        animator.SetTrigger("GroundPoundAttack");
                        comboTimer = 3.5f;
                    }
                }
                if (Vector3.Distance(target.position, transform.position) <= comboMaxRange && Vector3.Distance(target.position, transform.position) >= comboMinRange) 
                {
                    comboTimer -= Time.deltaTime;
                    if (comboTimer <= 0f)
                    {
                        animator.SetTrigger("12ComboAttack");
                        StartCoroutine(ComboSpikes());
                        comboTimer = 3.5f;
                    }
                }
            }
        }
    }

    IEnumerator ComboSpikes()
    {
        yield return new WaitForSeconds(0.5f);
        int spikes = -4;
        for (int i = 2; i >= spikes; i--)
        {
            if (i % 2 == 0)
            {
                earthSpikeLocation = new Vector3(this.transform.position.x - 4f, this.transform.position.y + i, 0f);
                
                GameObject spike = Instantiate(earthSpikes, earthSpikeLocation, Quaternion.identity) as GameObject;
                spike.transform.parent = GameObject.Find("EarthSpikes").transform;
            }
        }
        for (int i = 2; i >= spikes; i--)
        {
            if (i % 2 == 1 || i % 2 == -1)
            {
                earthSpikeLocation = new Vector3(this.transform.position.x - 5.5f, this.transform.position.y + i, 0f);
                
                GameObject spike = Instantiate(earthSpikes, earthSpikeLocation, Quaternion.identity) as GameObject;
                spike.transform.parent = GameObject.Find("EarthSpikes").transform;
            }
        }
        for (int i = 2; i >= spikes; i--)
        {
            if (i % 2 == 0)
            {
                earthSpikeLocation = new Vector3(this.transform.position.x - 7f, this.transform.position.y + i, 0f);
                
                GameObject spike = Instantiate(earthSpikes, earthSpikeLocation, Quaternion.identity) as GameObject;
                spike.transform.parent = GameObject.Find("EarthSpikes").transform;
            }
        }
    }

    private bool EnteredRoom()
    {
        target = FindObjectOfType<PlayerController>().transform;
        if (Vector3.Distance(target.position, transform.position) <= spawnRange)
        {
            animator.SetTrigger("PlayerEntered");
            return true;
        }
        else
        {
            return false;
        }
        //stop player movement
        //move camera over
        //signal formation animation
        //roar audio
        //move camera back to player
        //player can move again
    }

    #endregion 
}
