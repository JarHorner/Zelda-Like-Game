using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lanturn : MonoBehaviour
{
    #region Variables
    private bool isLit;
    #endregion

    #region Methods

    public void LightArea()
    {
        GameObject player = FindObjectOfType<PlayerController>().gameObject;
        GameObject playerLanturn = Instantiate(this.gameObject, player.transform.position, Quaternion.identity);
        playerLanturn.transform.parent = player.transform;
        isLit = true;
    }

    public void RemoveLanturn()
    {
        if (IsLit)
        {
            GameObject playerLanturn = FindObjectOfType<Lanturn>().gameObject;
            Destroy(playerLanturn);
            isLit = false;
        }
    }

    public bool IsLit
    {
        get { return isLit; }
        set { isLit = value; }
    }

    #endregion
}
