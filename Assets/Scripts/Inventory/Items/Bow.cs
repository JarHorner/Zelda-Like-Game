using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    #region Variables
    private PlayerController player;
    private InventoryManager inventoryManager;
    [SerializeField] GameObject projectile;

    #endregion


    #region Methods

    public void ShootArrow()
    {
        player = FindObjectOfType<PlayerController>();
        Debug.Log($"Horizontal: {player.Animator.GetFloat("Horizontal")}  Vertical: {player.Animator.GetFloat("Vertical")}");
        Vector2 temp = new Vector2(player.Animator.GetFloat("Horizontal"), player.Animator.GetFloat("Vertical"));
        Arrow arrow = Instantiate(projectile, player.gameObject.transform.position, Quaternion.identity).GetComponent<Arrow>();
        arrow.Setup(temp, ShootDirection());
    }

    //calculates the proper degree the player is facing, the arrow will shoot in that direction.
    private Vector3 ShootDirection()
    {
        //degree measure of how much to rotate arrow
        float temp = Mathf.Atan2(player.Animator.GetFloat("Vertical"), player.Animator.GetFloat("Horizontal")) * Mathf.Rad2Deg;
        return new Vector3(0, 0, temp + -90);
    }

    #endregion
}
