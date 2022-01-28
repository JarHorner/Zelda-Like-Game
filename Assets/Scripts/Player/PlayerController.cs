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
    private float attackCounter = 0.25f;
    private float shootCounter = 0f;
    private bool isMoving = false;
    private bool isSwimming = false;
    private bool isCarrying = false;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject interactBox;
    [SerializeField] private Animator animator;
    public string startPoint;
    [SerializeField] private  AudioSource swingSound;
    [SerializeField] private  AudioSource walkingSound;
    [SerializeField] private  AudioSource swimmingSound;
    [SerializeField] private AudioClip[] swingClips;
    private Vector2 movement;
    private static bool playerExists;
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
        if (move.triggered)
        {
            currentState = PlayerState.walk;
        }
        if (currentState == PlayerState.dead)
        {
            rb.constraints = RigidbodyConstraints2D.FreezeAll;
            //if player was swimming when died, this variable is true and when spawns again, wont be swimming.
            animator.SetBool("isDead", true);
            animator.SetBool("isSwimming", false);
        }

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
            if (!swimmingSound.isPlaying)
            {
                swimmingSound.Play();
            }
        }
        else
        {
            swimmingSound.Stop();
        }

        //when player has bow, ensures shooting arrows cannot be spammed
        if (shootCounter > 0f)
        {
            shootCounter -= Time.deltaTime;
        }

        if (isMoving)
        {
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
            else
            {
                walkingSound.Stop();
            }
        }

        //depending on the direction the player is moving, when moving diagonally, the player faces the same direction.
        if (movingHorizontal == 1)
        {
            animator.SetFloat("Horizontal", 1);
            animator.SetFloat("Vertical", 0);
            Debug.Log(firstKey);
        }
        else if (movingHorizontal == -1)
        {
            animator.SetFloat("Horizontal", -1);
            animator.SetFloat("Vertical", 0);
             Debug.Log(firstKey);
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
                Debug.Log("up arrow first key");
                keyStillPressed = Keyboard.current.upArrowKey.isPressed;
                break;
            case "down":
                Debug.Log("down arrow first key");
                keyStillPressed = Keyboard.current.downArrowKey.isPressed;
                break;
            case "left":
                Debug.Log("left arrow first key");
                keyStillPressed = Keyboard.current.leftArrowKey.isPressed;
                break;
            case "right":
                Debug.Log("right arrow first key");
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
        //randomizes sounds used when attacking.
        swingSound.clip = swingClips[Random.Range(0, swingClips.Length)];
        swingSound.Play();

        yield return new WaitForSeconds(attackCounter);

        animator.SetBool("isAttacking", false);
        currentState = PlayerState.walk;
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
    public bool OnConveyor
    {
        get { return onConveyor; }
        set { onConveyor = value; }
    }
    #endregion
}
