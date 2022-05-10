using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSenseSpikeBlock : MonoBehaviour
{
    #region Variables

    [SerializeField] private Animator animator;
    [SerializeField] private TriggerArea triggerArea;
    private Vector3 min;
    private Vector3 max;
    private IEnumerator triggeredBlock;
    [SerializeField] private float distance;
    [SerializeField] private float durationToHitPlayer;
    [SerializeField] private float durationBack;

    [Tooltip("true is x axis, false is y axis")]
    [SerializeField] private bool direction;

    #endregion

    #region Methods
    //sets min and max distance depending on direction
    void Start () 
    {
        if (direction)
        {
            min = transform.position;
            max = new Vector3(transform.position.x + distance, transform.position.y, transform.position.z);
        }
        else
        {
            min = transform.position;
            max = new Vector3(transform.position.x, transform.position.y + distance, transform.position.z);
        }
    }

    void Update() 
    {
        if (triggerArea.CanMove)
        {
            triggeredBlock = TriggeredBlock();
            StartCoroutine(triggeredBlock);
        }
    }

    IEnumerator TriggeredBlock()
    {
        float timeElapsed = 0;
        while (timeElapsed <= durationToHitPlayer)
        {
            transform.position = Vector3.Lerp(min, max, timeElapsed / durationToHitPlayer);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = max;
    }

    IEnumerator MoveBackToPosition()
    {
        float timeElapsed = 0;
        Vector3 currentPos = transform.position;
        while (timeElapsed <= durationBack)
        {
            transform.position = Vector3.Lerp(currentPos, min, timeElapsed / durationBack);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        transform.position = min;
        triggerArea.CanMove = false;
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Debug.Log("Collided");
        if (other.gameObject.tag == "MovableBlock")
        {
            StopAllCoroutines();
            triggerArea.CanMove = false;
            StartCoroutine(MoveBackToPosition());
        }
    }


    #endregion
}
