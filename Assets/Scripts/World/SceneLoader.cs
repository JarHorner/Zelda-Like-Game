using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{

    #region Variables
        public Animator transition;
        [SerializeField] private int sceneIndex;
        private float transitionTime = 1f;
        private PlayerController player;
        public string exitPoint;
    #endregion

    #region Unity Methods

    void Start()
    {
        player = FindObjectOfType<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D collider) 
    {
        if (collider.gameObject.tag == "Player") 
        {
            //loads the next level
            player.startPoint = exitPoint;
            //finds inactive gameobject
            //GameObject obj = this.gameObject.transform.Find("CircleWipe").gameObject;
            //obj.SetActive(true);
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
