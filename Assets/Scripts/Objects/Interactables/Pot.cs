using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pot : MonoBehaviour
{

    #region Varibles
    [SerializeField] InputActionAsset inputMaster;
    private InputAction interact;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource potBreak;
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
    private float dropTime = 0.55f;
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
            dropTime -= Time.deltaTime;
            if (dropTime <= 0)
            {
                rb.velocity = Vector2.zero;
                animator.SetTrigger("Break");
                loot.DropItem();
                StartCoroutine(RemoveRubble());
                thrown = false;
            }
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

            interact.performed -= PickUp;
            interact.Disable();
            interact.performed += Throw;
            interact.Enable();
        }
    }

    //SECOND
    //when able to be thrown, player will throw object.
    private void Throw(InputAction.CallbackContext context)
    {
        this.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, 0);
        float temp = Mathf.Atan2(player.Animator.GetFloat("Vertical"), player.Animator.GetFloat("Horizontal")) * Mathf.Rad2Deg;
        if (temp == 0)
            thrownPos = transform.right;
        else if (temp == 90)
            thrownPos = transform.up;
        else if (temp == 180)
            thrownPos = -transform.right;
        else
            thrownPos = -transform.up;

        this.transform.parent = null;
        polyCollider.isTrigger = false;
        player.IsCarrying = false;
        rb.isKinematic = false;
        rb.AddForce(thrownPos * speed, ForceMode2D.Impulse);
        //changes layer to PlayerProjectile
        this.gameObject.layer = 8;
        thrown = true;

        interact.performed -= Throw;
        interact.Disable();
    }
    

    //drops the obejct and breaks when in contact with other object
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "MovableBlock" || other.gameObject.tag == "Walls")
        {
            thrown = false;
            rb.velocity = Vector2.zero;
            animator.SetTrigger("Break");
            loot.DropItem();
            StartCoroutine(RemoveRubble());
        }
        if (other.gameObject.tag == "Enemy")
        {
            rb.velocity = Vector2.zero;
            other.gameObject.GetComponent<EnemyHealthManager>().DamageEnemy(damage, this.transform);
            animator.SetTrigger("Break");
            loot.DropItem();
            StartCoroutine(RemoveRubble());
        }
    }

    //enables destorying pot with sword, or picking it up
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Sword")
        {
            animator.SetTrigger("Break");
            loot.DropItem();
            StartCoroutine(RemoveRubble());
        }
        if (other.gameObject.tag == "InteractBox")
        {
            pickup = true;
            interact.performed += PickUp;
            interact.Enable();
        }
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if (other.tag == "InteractBox")
        {
            pickup = false;
        }
    }

    //Destroys gameobject after 3 seconds
    private IEnumerator RemoveRubble()
    {
        potBreak.Play();
        sprite.sortingLayerName = "Object";
        sprite.sortingOrder = 1;
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    #endregion
}
