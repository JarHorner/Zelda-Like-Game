using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    #region Variables
    [SerializeField] private Vector2 speed;
    private PlayerController playerController;
    #endregion

    #region Methods
    private void Start() 
    {
        playerController = FindObjectOfType<PlayerController>();
    }
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.tag == "Player" && other.GetType() != typeof(BoxCollider2D))
        {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(speed);
            Debug.Log($"{this.name} is moving");
        }
        
    }
    #endregion

}
