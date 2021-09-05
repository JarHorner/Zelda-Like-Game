using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableBlock : MonoBehaviour
{
    #region Variables
    public Rigidbody2D rb;
    private Animator playerAnim;
    private float y;
    private float x;

    #endregion

    #region Unity Methods
    void Start()
    {
        playerAnim = FindObjectOfType<PlayerController>().GetComponent<Animator>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        y = rb.transform.position.y;
        x = rb.transform.position.x;
    }

    private void OnCollisionStay2D(Collision2D collider) {

        if (collider.gameObject.tag == "Player")
        {
            playerAnim.SetBool("isPushing", true);
            if (y > rb.transform.position.y || y < rb.transform.position.y)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezeRotation;
            } 
            else if (x > rb.transform.position.x || x < rb.transform.position.x)
            {
                rb.constraints = RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collider) {
        if (collider.gameObject.tag == "Player")
        {
            playerAnim.SetBool("isPushing", false);
            rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        }
    }

    #endregion
}
