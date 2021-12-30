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
    private float comboTimer = 0f;
    private bool isMoving = false;
    [SerializeField] private AudioSource movementAudio;
    [SerializeField] private EnemyHealthManager enemyHealthManager;

    [Header("these three varibles can be adjusted at any time")]
    [SerializeField] private float speed = 0f;
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
        target = FindObjectOfType<PlayerController>().transform;

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
                    comboTimer = 3.5f;
                }
            }
        }
    }

    private void EnteredRoom()
    {
        //stop player movement
        //move camera over
        //signal formation animation
        //roar audio
        //move camera back to player
        //player can move again
    }

    #endregion 
}
