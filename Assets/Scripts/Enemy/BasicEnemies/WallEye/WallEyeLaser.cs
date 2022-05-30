using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallEyeLaser : MonoBehaviour
{

    #region Varibles

    [SerializeField] private FloatValue damage;

    #endregion

    #region Methods

    private void Start() 
    {
        Debug.Log(PlayerController.player);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "HitBox")
        {
            Debug.Log("Hit");
            Knockback.PushBack(this.transform, other.transform.parent.GetComponent<Rigidbody2D>());
            PlayerController.player.DamagePlayer(damage.InitalValue);
        }
    }

    #endregion
}
