using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public void PushBack(Transform thisTransform, Rigidbody2D otherRb)
    {
        Debug.Log("Pushed");
        Vector2 difference = otherRb.transform.position - thisTransform.position;
        difference = difference.normalized * 5f;
        otherRb.AddForce(difference, ForceMode2D.Impulse);

        if (otherRb.GetComponent<Enemy>() != null)
        {
            otherRb.GetComponent<Enemy>().currentState = EnemyState.stagger;
            otherRb.GetComponent<Enemy>().Knock(otherRb, 0.2f);
        }
        if (otherRb.GetComponent<PlayerController>() != null)
        {
            otherRb.GetComponent<PlayerController>().currentState = PlayerState.stagger;
            otherRb.GetComponent<PlayerController>().Knock(0.5f);
        }
    }
}
