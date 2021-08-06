using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthManager : MonoBehaviour
{

    #region Variables
    public int currHealth;
    public int maxHealth;
    private Animator animator;
    private bool flashActive;
    [SerializeField] private float flashLength = 0f;
    [SerializeField] private AudioClip death;
    private float flashCounter = 0f;
    private SpriteRenderer enemySprite;
    private SoundManager soundManager;
    private PlayerStats playerStats;
    [SerializeField] private int expValue;
    private Vector3 startingCoordinates;
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        enemySprite = this.gameObject.GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
        playerStats = FindObjectOfType<PlayerStats>();
        startingCoordinates = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //if true, starts process of changing the players alpha level to flash when hit
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

    public void HurtEnemy(int damageToGive) 
    {
        currHealth -= damageToGive;
        flashActive = true;
        flashCounter = flashLength;
        if (currHealth <= 0) 
        {
            soundManager.Play(death);
            playerStats.currentExp += expValue;
            this.gameObject.SetActive(false);
            //Destroy(gameObject);
        }
    }

    //used in AreaTransitions script to move enemies back to original position
    public Vector3 getStartingCoordinates() 
    {
        return startingCoordinates;
    }

    #endregion
}
