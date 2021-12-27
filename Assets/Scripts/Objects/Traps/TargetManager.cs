using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TargetManager : MonoBehaviour
{
    #region Variable
    [Header("unlock can be 'Key', 'Platform', 'Door'")]
    [SerializeField] private string unlock;
    [SerializeField] private GameObject unlockedObject;

    [Header("Other Information")]
    [SerializeField] private List<Target> targets;
    private bool allTargetsHit = false;
    private bool unlocked = false;

    #endregion

    #region Methods

    void Update() 
    {
        //if all targets are hit, plays the method according to what the targets unlock.
        if (allTargetsHit)
        {
            switch(unlock)
            {
                case "Door":
                    OpenDoor();
                    unlocked = true;
                    break;
                case "Key":
                    KeyAppear();
                    unlocked = true;
                    break;
                case "Platform":
                    PlatformAppear();
                    unlocked = true;
                    break;
            }
        }

        if (!unlocked)
        {
            AllTargetsHit();
        }
    }

    //checks if each target in List is hit
    private void AllTargetsHit()
    {
        allTargetsHit = targets.Any(o => o.Hit != false);
    }

    private void OpenDoor()
    {
        unlockedObject.GetComponent<TargetDoor>().DoorOpen = true;
        this.enabled = false;
    }

    private void KeyAppear()
    {
        unlockedObject.GetComponent<CapsuleCollider2D>().enabled = true;
        unlockedObject.GetComponent<SpriteRenderer>().enabled = true;
        this.enabled = false;
    }

    private void PlatformAppear()
    {
        unlockedObject.transform.GetChild(0).gameObject.SetActive(true);
        foreach(Transform child in unlockedObject.transform.GetChild(1))
        {
            child.gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        this.enabled = false;
    }

    #endregion
}
