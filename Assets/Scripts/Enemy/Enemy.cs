using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    public int health;
    public int maxHealth;
    public int baseAttack;
    public float moveSpeed;

    public void ChangeState(EnemyState newState)
    {
        if (currentState != newState)
            currentState = newState;
    }

    public void Knock(Rigidbody2D myRb, float knockTime)
    {
        StartCoroutine(KnockCo(myRb, knockTime));
    }

    private IEnumerator KnockCo(Rigidbody2D myRb, float knockTime)
    {
        if (myRb != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRb.velocity = Vector2.zero;
            Debug.Log("Changing to idle");
            currentState = EnemyState.idle;
            myRb.velocity = Vector2.zero;
        }
    }
}
