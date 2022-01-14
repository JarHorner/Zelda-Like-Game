using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    #region Varibles
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
    private bool canThrow = false;
    private bool thrown = false;
    private float speed = 10f;
    private float dropTime = 0.55f;
    #endregion

    #region Methods

    void Start() 
    {
        player = FindObjectOfType<PlayerController>();
    }

    void Update() 
    {
        //THIRD
        //this ensures the object moves correctly, and breaks when it collides or hits target location.
        if (thrown)
        {
            dropTime -= Time.deltaTime;
            //this.transform.position = Vector2.MoveTowards(this.transform.position, thrownPos, speed * Time.deltaTime);
            if (dropTime <= 0)
            {
                rb.velocity = Vector2.zero;
                animator.SetTrigger("Break");
                loot.DropItem();
                StartCoroutine(RemoveRubble());
                thrown = false;
            }
        }
        //SECOND
        //when able to be thrown, player will throw object.
        if (canThrow)
        {
            if (Input.GetButtonDown("Interact"))
            {
                this.transform.position =  new Vector3(player.transform.position.x, player.transform.position.y, 0);
                float temp = Mathf.Atan2(player.Animator.GetFloat("Vertical"), player.Animator.GetFloat("Horizontal")) * Mathf.Rad2Deg;
                Debug.Log("Rotation: " + temp);
                if (temp == 0)
                    //thrownPos = new Vector3((player.transform.position.x + 4f), (player.transform.position.y - 0.2f), 0);
                    thrownPos = transform.right;
                else if (temp == 90)
                    //thrownPos = new Vector3((player.transform.position.x), (player.transform.position.y + 4f), 0);
                    thrownPos = transform.up;
                else if (temp == 180)
                    //thrownPos = new Vector3((player.transform.position.x - 4f), (player.transform.position.y - 0.2f), 0);
                    thrownPos = -transform.right;
                else
                    //thrownPos = new Vector3((player.transform.position.x), (player.transform.position.y - 4f), 0);
                    thrownPos = -transform.up;

                this.transform.parent = null;
                polyCollider.isTrigger = false;
                player.IsCarrying = false;
                rb.isKinematic = false;
                rb.AddForce(thrownPos * speed, ForceMode2D.Impulse);
                //changes layer to PlayerProjectile
                this.gameObject.layer = 8;
                canThrow = false;
                thrown = true;
            }
        }
        //FIRST
        //When able to be picked up, player lifts object above head.
        if (pickup)
        {
            if (Input.GetButtonDown("Interact") && !player.IsCarrying)
            {
                this.transform.position =  new Vector3(player.transform.position.x, (player.transform.position.y + 1), 0);
                this.transform.parent = player.transform;
                player.IsCarrying = true;
                polyCollider.isTrigger = true;
                sprite.sortingLayerName = "Player";
                pickup = false;
                canThrow = true;
            }
        }
    }

    //drops the obejct and breaks when in contact with other object
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if (other.gameObject.tag == "Object" || other.gameObject.tag == "MovableBlock" || other.gameObject.tag == "Walls")
        {
            thrown = false;
            rb.velocity = Vector2.zero;
            //this.transform.position =  new Vector3(this.transform.position.x, (this.transform.position.y - 0.5f), 0);
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
