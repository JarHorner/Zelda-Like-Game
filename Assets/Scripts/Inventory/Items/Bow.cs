using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bow : MonoBehaviour
{
    #region Variables
    private PlayerController player;
    private UIManager uiManager;
    [SerializeField] private InventoryItem bowInvItem;
    [SerializeField] GameObject projectile;
    [SerializeField] private int amtArrows;
    [SerializeField] private int maxAmtArrows;

    #endregion


    #region Methods

    public void ShootArrow()
    {
        uiManager = FindObjectOfType<UIManager>();
        if (bowInvItem.numberHeld != 0)
        {
            player = FindObjectOfType<PlayerController>();
            Vector2 temp = new Vector2(player.Animator.GetFloat("Horizontal"), player.Animator.GetFloat("Vertical"));
            Arrow arrow = Instantiate(projectile, player.gameObject.transform.position, Quaternion.identity).GetComponent<Arrow>();
            arrow.Setup(temp, ShootDirection());
            bowInvItem.numberHeld--;
        }
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
