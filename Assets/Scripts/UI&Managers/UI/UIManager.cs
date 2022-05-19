using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    private static UIManager _instance;

    public static UIManager Instance { get { return _instance; } }
    
    void Awake() 
    {
        //Singleton Effect
        if (_instance != null && _instance != this)
        {
            Debug.Log($"Destroyed {this.gameObject}");
            Destroy (this.gameObject);
        }
        else
        {
            _instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
}
