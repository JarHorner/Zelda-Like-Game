using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shrub : MonoBehaviour
{

    #region Varibles
    [SerializeField] InputActionAsset inputMaster;
    private InputAction interact;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource shrubCut;
    [SerializeField] private RandomLoot loot;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private PolygonCollider2D polyCollider;
    [SerializeField] private SpriteRenderer sprite;
    private PlayerController player;
    private int damage = 1;
    private Vector3 thrownPos;
    private bool pickup = false;
    private bool thrown = false;
    private float speed = 10f;
    private float airTime = 0.5f;
    private float dropTime = 0.12f;
    #endregion

    #region Methods

    void Start() 
    {
        player = FindObjectOfType<PlayerController>();

        var playerActionMap = inputMaster.FindActionMap("Player");

        interact = playerActionMap.FindAction("Interact");
    }

    void Update() 
    {
        //THIRD
        //this ensures the object moves correctly, and breaks when it collides or hits target location.
        if (thrown)
        {
            airTime -= Time.deltaTime;
            if (airTime <= 0)
            {
                dropTime -= Time.deltaTime;
                rb.gravityScale += 1f;
                if (dropTime <= 0)
                {
                    rb.velocity = Vector2.zero;
                    rb.gravityScale = 0;
                    animator.SetTrigger("Thrown");
                    loot.DropItem();
                    StartCoroutine(RemoveLeaves());
                    thrown = false;
                }
            }
        }
    }

    private void PickupOrThrow(InputAction.CallbackContext context)
    {
        if (pickup && !player.IsCarrying)
        {
            PickUp(context);
        }
        else
        {
            Throw(context);
        }
    }

    //FIRST
    //When able to be picked up, player lifts object above head.
    private void PickUp(InputAction.CallbackContext context)
    {
        if (pickup && !player.IsCarrying)
        {
            this.transform.position =  new Vector3(player.transform.position.x, (player.transform.position.y + 1), 0);
            this.transform.parent = player.transform;
            player.IsCarrying = true;
            polyCollider.isTrigger = true;
            sprite.sortingLayerName = "Player";
            pickup = false;
        }
    }

    //SECOND
    //when able to be thrown, player will throw object.
    private void Throw(InputAction.CallbackContext context)
    {
        float temp = Mathf.Atan2(player.Animator.GetFloat("Vertical"), player.Animator.GetFloat("Horizontal")) * Mathf.Rad2Deg;
        if (temp == 0)
        {
            thrownPos = transform.right;
        }
        else if (temp > 0 && temp <= 135)
        {
            thrownPos = transform.up;
        }
        else if (temp == 180)
        {
            thrownPos = -transform.right;
        }
        else
        {
            thrownPos = -transform.up;
        }

        this.transform.parent = null;
        polyCollider.isTrigger = false;
        player.IsCarrying = false;
        rb.isKinematic = false;
        rb.AddForce(thrownPos * speed, ForceMode2D.Impulse);
        //changes layer to PlayerProjectile
        this.gameObject.layer = 8;
        thrown = true;
    }
    

    //drops the obejct and breaks when in contact with other object
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (thrown)
        {
            if (other.gameObject.tag == "Object" || other.gameObject.tag == "MovableBlock" || other.gameObject.tag == "Walls")
            {
                thrown = false;
                rb.velocity = Vector2.zero;
                animator.SetTrigger("Thrown");
                loot.DropItem();
                StartCoroutine(RemoveLeaves());
            }
            if (other.gameObject.tag == "Enemy")
            {
                rb.velocity = Vector2.zero;
                other.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damage, this.transform);
                animator.SetTrigger("Thrown");
                loot.DropItem();
                StartCoroutine(RemoveLeaves());
            }
        }
    }

    //enables destorying pot with sword, or picking it up
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Sword")
        {
            animator.SetTrigger("Cut");
            loot.DropItem();
            shrubCut.Play();
            sprite.sortingLayerName = "Object";
            sprite.sortingOrder = 1;
        }
        if (other.gameObject.tag == "InteractBox")
        {
            pickup = true;
            interact.performed += PickupOrThrow;
            interact.Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "InteractBox" && !player.IsCarrying)
        {
            pickup = false;
            interact.performed -= PickupOrThrow;
            interact.Disable();
        }
    }

    //Destroys gameobject after 3 seconds
    private IEnumerator RemoveLeaves()
    {
        shrubCut.Play();
        sprite.sortingLayerName = "Object";
        sprite.sortingOrder = 1;
        interact.performed -= PickupOrThrow;
        interact.Disable();
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    #endregion
}