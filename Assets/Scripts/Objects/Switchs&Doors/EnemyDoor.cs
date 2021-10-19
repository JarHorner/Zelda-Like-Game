using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDoor : MonoBehaviour
{
    #region Variables
    private ArrayList enemies = new ArrayList();
    private GameManager gameManager;
    [SerializeField] private Animator animator;
    [SerializeField] private AudioSource openDoor;
    private bool enemiesDefeated = false;
    private bool doorNotOpen = true;

    #endregion

    #region Methods
    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        int children = transform.childCount;
        for (int i = 0; i < children; i++)
        {
            enemies.Add(transform.GetChild(i).gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (doorNotOpen)
        {
            AllEnemiesDeactive();
            if (enemiesDefeated)
            {
                StartCoroutine(OpenEnemyDoor());
            }
        }
    }

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

    IEnumerator OpenEnemyDoor()
    {
        //pauses game while animations play (so player cannot move)
        openDoor.Play();
        gameManager.Pause(false);
        animator.SetBool("Open", true);
        yield return new WaitForSeconds(1f);
        doorNotOpen = false;
        gameManager.UnPause();
    }

    #endregion
}
