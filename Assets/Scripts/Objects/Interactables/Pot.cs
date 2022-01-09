using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pot : MonoBehaviour
{

    #region Varibles
    [SerializeField] private Animator animator;
    [SerializeField] private RandomLoot loot;
    #endregion

    #region Methods

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag == "Sword")
        {
            animator.SetTrigger("Break");
            loot.DropItem();
            StartCoroutine(RemoveRubble());
        }
    }

    private IEnumerator RemoveRubble()
    {
        yield return new WaitForSeconds(3f);
        Destroy(this.gameObject);
    }

    #endregion
}
