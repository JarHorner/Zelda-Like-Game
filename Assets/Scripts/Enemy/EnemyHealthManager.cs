using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    #region Variables
    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;
    private Animator animator;
    private bool flashActive;
    [SerializeField] private float flashLength = 0f;
    [SerializeField] private AudioClip hit;
    [SerializeField] private SoundManager soundManager;
    [SerializeField] private AudioClip death;
    [SerializeField] private RandomLoot randomLoot;
    private float flashCounter = 0f;
    //in code, eventually set to 0.5f.
    public float waitToHurt = 0f;
    private SpriteRenderer enemySprite;
    private PlayerStats playerStats;
    [SerializeField] private int expValue;
    private Vector3 startingCoordinates;
    #endregion

    #region Unity Methods

    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        enemySprite = this.gameObject.GetComponent<SpriteRenderer>();
        playerStats = FindObjectOfType<PlayerStats>();
        startingCoordinates = this.transform.position;
    }

    void Update()
    {
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
    public void HurtEnemy(int damageToGive) 
    {
        if (waitToHurt <= 0)
        {
            currHealth -= damageToGive;
            flashActive = true;
            soundManager.Play(hit);
            if (currHealth <= 0) 
            {
                soundManager.Play(death);
                playerStats.SetCurrentExp(expValue);
                //drops loot based on drop table
                randomLoot.DropItem();
                this.gameObject.SetActive(false);
            }
            //starts the flashing of enemy in Update()
            flashCounter = flashLength;
            //gives variable some time so enemy cant be chain hit
            waitToHurt = 0.5f; 

        }
    }

    //used in AreaTransitions script to move enemies back to original position
    public Vector3 getStartingCoordinates() 
    {
        return startingCoordinates;
    }

    //using when enemy is re-actived, to give full hp. (AreaTransitions)
    public int getMaxHealth()
    {
        return maxHealth;
    }

    //used in tandem with getMaxHealth() to set enemies hp to max. (AreaTransitions)
    public void setCurrentHealth(int newHealth)
    {
        currHealth = newHealth;
    }
    #endregion
}
