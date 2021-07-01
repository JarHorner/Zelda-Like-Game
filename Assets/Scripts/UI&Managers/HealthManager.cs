using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthManager : MonoBehaviour
{

    #region Variables
    public int currHealth;
    public int maxHealth;
    private float waitToLoad = 2f;
    private bool reloading;
    private Animator animator;
    private Rigidbody2D rb;
    [SerializeField] private AudioClip death;
    private SoundManager soundManager;
    private bool flashActive;
    [SerializeField] private float flashLength = 0f;
    private float flashCounter = 0f;
    private SpriteRenderer playerSprite;
    public bool revive;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        animator = this.gameObject.GetComponent<Animator>();
        playerSprite = this.gameObject.GetComponent<SpriteRenderer>();
        soundManager = FindObjectOfType<SoundManager>().GetComponent<SoundManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //called when player is dead, resetting variables changed at death and re-loading scene
        if (reloading)
        {
            Debug.Log("Waiting to load");
            waitToLoad -= Time.deltaTime;
            currHealth = 0;
            if (waitToLoad <= 0)
            {
                animator.SetBool("isDead", false);
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

    //Method that calculates the amount of health player has left after hit, active the flashing and starts process of reloading scene at death
    public void HurtPlayer(int damageNum)
    {
        currHealth -= damageNum;
        flashActive = true;
        flashCounter = flashLength;

        if (currHealth <= 0)
        {
            //starts death animation
            Debug.Log("Dying!");
            if (soundManager != null)
                soundManager.Play(death);
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            rb.isKinematic = true;
            animator.SetBool("isDead", true);
            reloading = true;
        }
    }

    #endregion
}
