using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    #region Variables
    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;
    [SerializeField] private Animator animator;
    private bool isHit;
    private float knockbackTime = 0.2f;
    private Rigidbody2D rb;
    private bool flashActive;
    [SerializeField] private float flashLength = 0f;
    [SerializeField] private AudioClip hit;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip death;
    [SerializeField] ParticleSystem deathBurst;
    [SerializeField] private RandomLoot randomLoot;
    public Weakness weaknesses;
    private float flashCounter = 0f;
    //in code, eventually set to 0.5f.
    public float waitToHurt = 0f;
    [SerializeField] private SpriteRenderer enemySprite;
    private PlayerStats playerStats;
    [SerializeField] private int expValue;
    private Vector3 startingCoordinates;
    #endregion

    #region Unity Methods

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerStats = FindObjectOfType<PlayerStats>();
        startingCoordinates = this.transform.position;
    }

    void Update()
    {
        if (isHit)
        {
            knockbackTime -= Time.deltaTime;
            if (knockbackTime <= 0f)
            {
                rb.velocity = Vector3.zero;
                isHit = false;
                knockbackTime = 0.2f;
            }
        }

        //updates timer for enemy to take damage again, but on counts down if over 0
        if (waitToHurt > 0)
        {
            waitToHurt -= Time.deltaTime;
        }

        //if true, starts process of changing the enemies alpha level to flash when hit
        if (flashActive)
        {
            if (flashCounter > flashLength * .99f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0.2f);
            }
            else if (flashCounter > flashLength * .82f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .66f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0.2f);
            }
            else if (flashCounter > flashLength * .49f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .33f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0.2f);
            }
            else if (flashCounter > flashLength * .16f)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
            }
            else if (flashCounter > 0)
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 0.2f);
            }
            else
            {
                enemySprite.color = new Color(enemySprite.color.r, enemySprite.color.g, enemySprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
    }

    //causes damage to the enemies health, and if the enemies health is 0, player awarded exp and gameobject is deactivated. (Safer and easier then destroying)
    public void DamageEnemy(int damageToGive, Transform weaponTrans) 
    {
        if (waitToHurt <= 0)
        {
            currHealth -= damageToGive;
            
            flashActive = true;
            soundManager.Play(hit);
            if (currHealth <= 0) 
            {
                //spawns particles on death
                ParticleSystem partSys = Instantiate(deathBurst, transform.position, transform.rotation);
                partSys.Play(true);
                soundManager.Play(death);
                playerStats.SetCurrentExp(expValue);

                this.gameObject.SetActive(false);

                //drops loot based on drop table
                randomLoot.DropItem();
            }
            //starts the flashing of enemy in Update()
            flashCounter = flashLength;
            //gives variable some time so enemy cant be chain hit
            waitToHurt = 0.5f; 
        }
    }

    public void DamageBoss(int damageToGive, Transform weaponTrans) 
    {
        if (waitToHurt <= 0)
        {
            currHealth -= damageToGive;

            flashActive = true;
            soundManager.Play(hit);
            animator.SetTrigger("Hurt");
            if (currHealth <= 0) 
            {
                //spawns particles on death
                ParticleSystem partSys = Instantiate(deathBurst, transform.position, transform.rotation);
                partSys.Play(true);
                soundManager.Play(death);
                playerStats.SetCurrentExp(expValue);

                StartCoroutine(PlayOutDeathAnimation());
                //drops loot based on drop table
                randomLoot.DropItem();
            }
            //starts the flashing of enemy in Update()
            flashCounter = flashLength;
            //gives variable some time so enemy cant be chain hit
            waitToHurt = 0.5f; 
        }
    }

    IEnumerator PlayOutDeathAnimation()
    {
        animator.SetBool("Death", true);
        yield return new WaitForSeconds(2f);
        //if no parent, just sets object off, if has parent, sets parent off
        if (transform.parent == null)
            this.gameObject.SetActive(false);
        else
            this.gameObject.transform.parent.gameObject.SetActive(false);
    }

    //used in AreaTransitions script to move enemies back to original position
    public Vector3 StartingCoordinate
    {
        get { return startingCoordinates; }
    }

    public bool IsHit
    {
        set { isHit = value; }
    }

    //using when enemy is re-actived, to give full hp. (AreaTransitions)
    public int MaxHealth
    {
        get { return maxHealth; }
    }

    //used in tandem with getMaxHealth() to set enemies hp to max. (AreaTransitions)
    public int CurrentHealth
    {
        get { return currHealth; }
        set {currHealth = value; }
    }
    #endregion
}
