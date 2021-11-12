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
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);
    }

    #endregion
}
