using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{

    #region Variables
    [SerializeField] private int currHealth;
    [SerializeField] private int maxHealth;
    private float waitToLoad = 1.8f;
    private bool reloading;
    private Animator animator;
    private bool animBeforeDeath;
    private Rigidbody2D rb;
    private CapsuleCollider2D capsuleCollider;
    [SerializeField] private AudioClip death;
    [SerializeField] ParticleSystem deathBurst;
    [SerializeField] private AudioSource hit;
    [SerializeField] private DamagePopup damagePopup;
    private SoundManager soundManager;
    private bool flashActive;
    [SerializeField] private float flashLength = 0f;
    private float flashCounter = 0f;
    private SpriteRenderer playerSprite;
    public bool revive;

    #endregion

    #region Unity Methods

    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        playerSprite = this.gameObject.GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
        //used when save button is working
        if (PlayerPrefs.GetInt("PlayerCurrHp") != 0)
            currHealth = PlayerPrefs.GetInt("PlayerCurrHp");
    }

    void Update()
    {
        //called when player is dead, resetting variables changed at death and re-loading scene
        if (reloading)
        {
            waitToLoad -= Time.deltaTime;
            currHealth = 0;
            if (waitToLoad <= 0)
            {
                animator.SetBool("isDead", false);
                //checks to see if current animation state is swimming to reset animations before respawn
                if (animBeforeDeath) 
                {
                    animator.SetBool("isSwimming", false);
                    animator.Play("Walk Idle", 0, 1f);
                }
                rb.isKinematic = false;
                rb.constraints = RigidbodyConstraints2D.FreezeRotation;
                reloading = false;
                waitToLoad = 2f;
                Debug.Log("Loaded!");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                revive = true;
            }
        }

        //if true, starts process of changing the players alpha level to flash when hit
        if (flashActive && !reloading)
        {
            if (flashCounter > flashLength * .99f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.2f);
            }
            else if (flashCounter > flashLength * .82f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .66f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.2f);
            }
            else if (flashCounter > flashLength * .49f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > flashLength * .33f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.2f);
            }
            else if (flashCounter > flashLength * .16f)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
            }
            else if (flashCounter > 0)
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 0.2f);
            }
            else
            {
                playerSprite.color = new Color(playerSprite.color.r, playerSprite.color.g, playerSprite.color.b, 1f);
                flashActive = false;
            }
            flashCounter -= Time.deltaTime;
        }
    }

    //calculates the amount of health player has left after hit, active the flashing and starts process of reloading scene if dead
    public void DamagePlayer(int damageNum)
    {
        hit.Play();
        //positions damage text, and creates the popup
        Vector3 popupLocation = new Vector3(this.transform.position.x, this.transform.position.y + 1, 0);
        damagePopup.Create(popupLocation, damageNum);
        currHealth -= damageNum;
        flashActive = true;
        flashCounter = flashLength;

        if (currHealth <= 0)
        {
            //spawns particles on death
            ParticleSystem partSys = Instantiate(deathBurst, transform.position, transform.rotation);
            partSys.Play(true);
            //starts death animation
            Debug.Log("Dying!");
            //if player was swimming when died, this variable is true and when spawns again, wont be swimming.
            animBeforeDeath = animator.GetCurrentAnimatorStateInfo(0).IsTag("Swimming");
            if (soundManager != null)
                soundManager.Play(death);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.isKinematic = true;
            animator.SetBool("isDead", true);
            animator.SetBool("isSwimming", false);
            reloading = true;
        }
    }

    public void Heal(int healAmt) 
    {
        currHealth += healAmt;
        if (currHealth > maxHealth)
        {
            currHealth = maxHealth;
        }
    }

    public int CurrHealth
    {
        get { return currHealth; }
        set { currHealth = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    #endregion
}
