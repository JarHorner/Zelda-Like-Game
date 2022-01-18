using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    #region Variables
    private ArrayList enemies = new ArrayList();
    private PauseGame pauseGame;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource openDoor;
    private bool enemiesDefeated = false;
    private bool doorNotOpen = true;

    #endregion

    #region Methods

    void Start()
    {
        pauseGame = FindObjectOfType<PauseGame>();
        //takes all the children of the door and adds them to ArrayList. Used to decide when the door opens.
        int children = transform.childCount;
        for (int i = 0; i < children; i++)
        {
            enemies.Add(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        //if door is not open, keeps checking if all enemies are defeated.
        if (doorNotOpen)
        {
            AllEnemiesDeactive();
            //if all are defeated, opens door.
            if (enemiesDefeated)
            {
                StartCoroutine(OpenEnemyDoor());
            }
        }
    }

    //loops through ArrayList, checking if each enemy is active. if all are de-activated, allows to Update() to run Coroutine.
    private void AllEnemiesDeactive()
    {
        foreach (GameObject enemy in enemies)
        {
            if (enemy.activeSelf)
            {
                enemiesDefeated = false;
                break;
            }
            else
            {
                enemiesDefeated = true;
            }
        }
    }

    //uses the Pause() function from GameManager to prevent movement and play the animation of door opening.
    IEnumerator OpenEnemyDoor()
    {
        openDoor.Play();
        pauseGame.Pause(false);
        animator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        doorNotOpen = false;
        pauseGame.UnPause();
    }

    #endregion
}
