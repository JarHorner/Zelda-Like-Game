using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    #region Variables
        [SerializeField] private Animator transition;
        [SerializeField] private int sceneIndex;
        private float transitionTime = 1f;
        private PlayerController player;
        private GameManager gameManager;
        private UIManager uIManager;
        public string exitPoint;
    #endregion

    #region Unity Methods

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
        uIManager = FindObjectOfType<UIManager>();
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player") 
        {
            //loads the next level
            player.StartPoint = exitPoint;
            LoadScene();
        }
    }

    public void LoadScene()
    {
        StartCoroutine(LoadLevel(sceneIndex));
    }

    //an iterator that starts the scene transition animation when loading a level
    IEnumerator LoadLevel(int levelIndex)
    {
        transition.SetTrigger("Start");
        Debug.Log("Loading Level");

        // //changes the amt of keys shown in the UI depending on scene (Will add more with more dungeons)
        // if (gameManager.CurrentScene.Contains("Dungeon"))
        // {
        //     //gets the last index (which will be the number of the dungeon)
        //     char dungeonNum = gameManager.CurrentScene[gameManager.CurrentScene.Length - 1];
        //     //converts the char to int
        //     uIManager.ChangeKeyCount(dungeonNum - 0);
        // }
        // else
        // {
        //     uIManager.ChangeKeyCount();
        // }
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    #endregion
}
