using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState{
    idle,
    walk,
    attack,
    stagger,
    dead
}

public class Enemy : MonoBehaviour
{
    public EnemyState currentState;
    private int health;
    
    [SerializeField] private FloatValue maxHealth;
    [SerializeField] private FloatValue baseAttack;
    [SerializeField] private float moveSpeed;
    [SerializeField] private bool isKnockable;

    void Awake() 
    {
        health = maxHealth.InitalValue;
        Debug.Log("Health " + health);
    }

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

    public int Health
    {
        get { return health; }
        set { health = value; }
    }

    public int MaxHealth
    {
        get { return maxHealth.InitalValue; }
    }

    public int BaseAttack
    {
        get { return baseAttack.InitalValue; }
    }

    public float MoveSpeed
    {
        get { return moveSpeed; }
    }

    public bool IsKnockable
    {
        get { return isKnockable; }
    }
}
