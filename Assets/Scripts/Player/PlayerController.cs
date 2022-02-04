using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public enum PlayerState 
{
    walk,
    swim,
    attack,
    interact,
    dead,
    stagger,
    menu
}

public class PlayerController : MonoBehaviour
{
    #region Variables
    public PlayerState currentState;
    //used to keep track of the players last location
    private static Vector2 lastPlayerLocation;
    [SerializeField] private InputActionAsset inputMaster;
    private InputAction move, attack, useItem1, useItem2, pause, inventory;
    private GameManager gameManager;
    public float moveSpeed;
    private float waitToLoad = 1.8f;
    private float attackCounter = 0.25f;
    private float shootCounter = 0f;
    private float flashCounter = 0.8f;
    private float flashLength = 0.8f;
    private bool isMoving = false;
    private bool isSwimming = false;
    private bool isCarrying = false;
    private bool isReviving = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private CapsuleCollider2D hitBox;
    [SerializeField] private CapsuleCollider2D physicBox;
    [SerializeField] private GameObject interactBox;
    [SerializeField] private Animator animator;
    public string startPoint;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource hitAudioSource;
    [SerializeField] private AudioClip walkingSound;
    [SerializeField] private AudioClip swimmingSound;
    [SerializeField] private AudioClip deathSound;
    [SerializeField] private DamagePopup damagePopup;
    [SerializeField] private ParticleSystem deathBurst;
    private Vector2 movement;
    private static bool playerExists;
    private bool flashActive;
    private bool onConveyor = false;
    private string firstKey = "";
    private int movingVertical, movingHorizontal;
    #endregion

    #region Unity Methods

    void Awake() 
    {
        gameManager = FindObjectOfType<GameManager>();
        lastPlayerLocation = new Vector2(0, 0);
        Debug.Log(lastPlayerLocation);

        var playerActionMap = inputMaster.FindActionMap("Player");

        move = playerActionMap.FindAction("Movement");
        attack = playerActionMap.FindAction("Attack");
        pause = playerActionMap.FindAction("Pause");
        inventory = playerActionMap.FindAction("Inventory");
        useItem1 = playerActionMap.FindAction("useItem1");
        useItem2 = playerActionMap.FindAction("useItem2");
    }

    //Singleton affect code
    void Start() 
    {
        if (!playerExists)
        {
            playerExists = true;
            //ensures same player object is not destoyed when loading new scences
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy (gameObject);
        }
        currentState = PlayerState.walk;
        physicBox.enabled = true;
        hitBox.enabled = true;
        animator.SetFloat("Horizontal", 0);
        animator.SetFloat("Vertical", -1);
    }
    private void OnEnable() 
    {
        move.Enable();
        move.performed += PlayerMoving;
        move.canceled += PlayerMoving;
        attack.Enable();
        attack.performed += PlayerAttack;
        pause.Enable();
        pause.performed += gameManager.OpenPauseMenu;
        inventory.Enable();
        inventory.performed += gameManager.OpenInventoryMenu;
        useItem1.Enable();
        useItem1.performed += gameManager.UseItem;
        useItem2.Enable();
        useItem2.performed += gameManager.UseItem;
    }

    private void OnDisable() 
    {
        move.Disable();
        move.performed -= PlayerMoving;
        move.canceled -= PlayerMoving;
        attack.Disable();
        attack.performed -= PlayerAttack;
        pause.Disable();
        pause.performed -= gameManager.OpenPauseMenu;
        inventory.Disable();
        inventory.performed -= gameManager.OpenInventoryMenu;
        useItem1.Disable();
        useItem1.performed -= gameManager.UseItem;
        useItem2.Disable();
        useItem2.performed -= gameManager.UseItem;
    }

    void Update()
    {
        if (move.triggered && currentState != PlayerState.stagger)
        {
            currentState = PlayerState.walk;
        }
        if (currentState == PlayerState.dead)
        {
            StartCoroutine(PlayerDead());
        }
        //this code activates when player is damaged, causing the flashing of the sprite
        if (flashActive)
        {
            //if true, starts process of changing the players alpha level to flash when hit
            DamageFlashing.SpriteFlashing(flashLength, flashCounter, this.gameObject.GetComponent<SpriteRenderer>());
            flashCounter -= Time.deltaTime;
            if (flashCounter < 0)
            {
                flashCounter = flashLength;
                flashActive = false;
            }
        }

        //needed to stops player from picking up multiple objects
        if (isCarrying)
        {
            interactBox.SetActive(false);
        }
        else 
        {
            interactBox.SetActive(true);
        }

        //plays swimming sound if in water and moving, does not if already playing
        if (isSwimming && isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = swimmingSound;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        //when player has bow, ensures shooting arrows cannot be spammed
        if (shootCounter > 0f)
        {
            shootCounter -= Time.deltaTime;
        }

        if (isMoving)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.clip = walkingSound;
                audioSource.Play();
            }
        }
        else
        {
            audioSource.Stop();
        }

        //depending on the direction the player is moving, when moving diagonally, the player faces the same direction.
        if (movingHorizontal == 1)
        {
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);
        }
        else if (movingHorizontal == -1)
        {
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
        }
        else if (movingVertical == 1)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", 1);
        }
        else if (movingVertical == -1)
        {
            animator.SetFloat("Horizontal", 0);
            animator.SetFloat("Vertical", -1);
        }
        else
        {
            movingVertical = 0;
            movingHorizontal = 0;
        }
    }

    //this is where the actual movement happens. better for performance, not tying movement to framerate.
    void FixedUpdate()
    {
        if (currentState == PlayerState.walk || currentState == PlayerState.swim)
        {
            //enables movement
            rb.MovePosition(rb.position + movement.normalized * moveSpeed * Time.fixedDeltaTime * Time.timeScale);
        }
    }

    //calculates the amount of health player has left after hit, active the flashing and starts process of reloading scene if dead
    public void DamagePlayer(int damageNum)
    {
        hitAudioSource.Play();
        //positions damage text, and creates the popup
        Vector3 popupLocation = new Vector3(this.transform.position.x, this.transform.position.y + 1, 0);
        damagePopup.Create(popupLocation, damageNum);
        HealthVisual.healthSystemStatic.Damage(damageNum);
        flashActive = true;
    }

    private IEnumerator PlayerDead()
    {
        currentState = PlayerState.walk;
        rb.isKinematic = true;
        //spawns particles on death
        ParticleSystem partSys = Instantiate(deathBurst, transform.position, transform.rotation);
        partSys.Play(true);
        audioSource.clip = deathSound;
        audioSource.Play();
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        physicBox.enabled = false;
        hitBox.enabled = false;
        bool deathWhileSwimming = animator.GetCurrentAnimatorStateInfo(0).IsTag("Swimming");
        //if player was swimming when died, this variable is true and when spawns again, wont be swimming.
        animator.SetBool("isDead", true);
        animator.SetBool("isSwimming", false);

        yield return new WaitForSeconds(waitToLoad);

        animator.SetBool("isDead", false);
        //checks to see if current animation state is swimming to reset animations before respawn
        if (deathWhileSwimming)
        {
            animator.SetBool("isSwimming", false);
            animator.Play("Walk Idle", 0, 1f);
        }
        rb.isKinematic = false;
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        Debug.Log("Loaded!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        physicBox.enabled = true;
        hitBox.enabled = true;
        isReviving = true;
    }

    private void PlayerMoving(InputAction.CallbackContext context)
    {
        //gets input
        movement.x = move.ReadValue<Vector2>().x;
        movement.y = move.ReadValue<Vector2>().y;

        if (movement != Vector2.zero)
        {
            //sets new directions from the movement and sets bool, to play walking sound
            DetermineFacingDirection();
            isMoving = true;
        } 
        else
        {
            isMoving = false;
        }
        animator.SetFloat("Speed", movement.sqrMagnitude);
    }

    private void CheckFirstKey(string direction)
    {
        bool keyStillPressed = false;
        switch (direction)
        {
            case "up":
                keyStillPressed = Keyboard.current.upArrowKey.isPressed;
                break;
            case "down":
                keyStillPressed = Keyboard.current.downArrowKey.isPressed;
                break;
            case "left":
                keyStillPressed = Keyboard.current.leftArrowKey.isPressed;
                break;
            case "right":
                keyStillPressed =  Keyboard.current.rightArrowKey.isPressed;
                break;
        }
        if (!keyStillPressed)
        {
            firstKey = "";
        }
    }

    private void DetermineFacingDirection()
    {
        var pressedKey = Keyboard.current;
        CheckFirstKey(firstKey);
        if (firstKey == "")
        {
            if (pressedKey.upArrowKey.isPressed)
            {
                movingVertical = 1;
                movingHorizontal = 0;
                firstKey = "up";
            }
            else if (pressedKey.downArrowKey.isPressed)
            {
                movingVertical = -1;
                movingHorizontal = 0;
                firstKey = "down";
            }
            else if (pressedKey.leftArrowKey.isPressed)
            {
                movingHorizontal = -1;
                movingVertical = 0;
                firstKey = "left";
            }
            else if (pressedKey.rightArrowKey.isPressed)
            {
                movingHorizontal = 1;
                movingVertical = 0;
                firstKey = "right";
            }
        }
        else
        {
            if ((pressedKey.rightArrowKey.isPressed && firstKey == "up") || (pressedKey.leftArrowKey.isPressed && firstKey == "up"))
            {
                movingVertical = 1;
                movingHorizontal = 0;
            }
            else if ((pressedKey.rightArrowKey.isPressed && firstKey == "down") || (pressedKey.leftArrowKey.isPressed && firstKey == "down"))
            {
                movingVertical = -1;
                movingHorizontal = 0;
            }
            else if ((pressedKey.upArrowKey.isPressed && firstKey == "left") || (pressedKey.downArrowKey.isPressed && firstKey == "left"))
            {
                movingHorizontal = -1;
                movingVertical = 0;
            }
            else if ((pressedKey.upArrowKey.isPressed && firstKey == "right") || (pressedKey.downArrowKey.isPressed && firstKey == "right"))
            {
                movingHorizontal = 1;
                movingVertical = 0;
            }
        }
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
    }

    private void PlayerAttack(InputAction.CallbackContext context)
    {
        StartCoroutine(AttackCo());
    }

    //if not currently attacking, starts the animation, if attack button has been pressed again, another attack
    //animation is qued up. Creating smooth attacking if the attack button is spammed.
    private IEnumerator AttackCo()
    {
        currentState = PlayerState.attack;
        animator.SetBool("isAttacking", true);

        yield return new WaitForSeconds(attackCounter);

        animator.SetBool("isAttacking", false);
        currentState = PlayerState.walk;
    }

    public void Knock(float knockTime)
    {
        StartCoroutine(KnockCo(knockTime));
    }

    private IEnumerator KnockCo(float knockTime)
    {
        yield return new WaitForSeconds(knockTime);
        rb.velocity = Vector2.zero;
        currentState = PlayerState.walk;
        rb.velocity = Vector2.zero;
    }

    //enables regular movement when colliding with OutOfWater trigger when leaving water.
    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Water") 
        {
            //sets swimming animation and new player speed
            animator.SetBool("isSwimming", true);
            currentState = PlayerState.swim;
            isSwimming = true;
            moveSpeed = 4f;
        }
        else if (collider.gameObject.tag == "OutOfWater")
        {
            animator.SetBool("isSwimming", false);
            if (isSwimming)
                isSwimming = false;
            moveSpeed = 5f;
        }
    }

    //Getters and Setters for player variables.

   public Vector2 LastPlayerLocation
    {
        get { return lastPlayerLocation; }
        set { lastPlayerLocation = value; }
    }

    public Vector2 Movement
    {
        get { return movement; }
        set { movement = value; }
    }

    public Animator Animator
    {
        get { return animator; }
    }
    public string StartPoint
    {
        get { return startPoint; }
        set { startPoint = value; }
    }
    public float ShootCounter
    {
        get { return shootCounter; }
        set { shootCounter = value; }
    }
    public bool IsCarrying
    {
        get { return isCarrying; }
        set { isCarrying = value; }
    }
    public bool IsReviving
    {
        get { return isReviving; }
    }
    public bool OnConveyor
    {
        get { return onConveyor; }
        set { onConveyor = value; }
    }
    #endregion
}
