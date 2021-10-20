using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestAppear : MonoBehaviour
{
    #region Variables
    private ArrayList enemies = new ArrayList();
    [SerializeField] private CircleCollider2D circleCollider;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private SpriteRenderer spriteRenderer;
    private bool enemiesDefeated = false;
    private bool chestNotVisible = true;

    #endregion

    #region Methods

    void Start()
    {
        int children = transform.childCount;
        //add each enemy to a ArrayList. skips 0 because 0 is the item in the chest, not an enemy
        for (int i = 1; i < children; i++)
        {
            enemies.Add(transform.GetChild(i).gameObject);
        }
    }

    void Update()
    {
        //if chest is not visable, keeps checking if all enemies are defeated.
        if (chestNotVisible)
        {
            AllEnemiesDeactive();
            //if all are defeated, chest appears.
            if (enemiesDefeated)
            {
                spriteRenderer.enabled = true;
                boxCollider.enabled = true;
                circleCollider.enabled = true;
                chestNotVisible = false;
            }
        }
    }

    //loops through ArrayList, checking if each enemy is active. if all are de-activated, allows to Update() to activate the chest.
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

    #endregion
}
