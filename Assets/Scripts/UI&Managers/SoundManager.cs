using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    #region Variables
    private Vector3 centerPoint = new Vector3(0,0,0);
    [SerializeField] private AudioClip backgroundMusic;
    private GameObject gameObj;
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        //starts by playing background music when entering scene
        PlayBackground(backgroundMusic);
    }

    //creates a new GameObject, gives it a AudioSource component and adds the parameter as the clip then plays it.
    //Used for sound effects
    public void Play(AudioClip clip)
    {
        GameObject newSound = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        newSound.name = clip.name;
        newSound.transform.parent = gameObject.transform;

        newSound.AddComponent(typeof(AudioSource));
        AudioSource source = newSound.GetComponent<AudioSource>();
        source.clip = clip;
        source.priority = 0;
        source.Play();
    }

    //creates a new GameObject, gives it a AudioSource component and adds the parameter as the clip then plays it.
    //Used for sound effects (Difference is the audio loops)
    public void Loop(AudioClip clip)
    {
        GameObject newSound = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        newSound.name = clip.name;
        newSound.transform.parent = gameObject.transform;

        newSound.AddComponent(typeof(AudioSource));
        AudioSource source = newSound.GetComponent<AudioSource>();
        source.clip = clip;
        source.priority = 0;
        source.loop = true;
        source.Play();
    }

    //Finds the GameObject with the same name as the sound that needs to stop, then destroys the GameObject. Help clear up memory.
    public void Stop(AudioClip clip)
    {
        int space = clip.ToString().IndexOf(' ');
        string clipName = clip.ToString().Substring(0, space);
        GameObject sound = GameObject.Find(clipName);
        Debug.Log(clipName);

        Destroy(sound);
    }

    //creates a new GameObject, gives it a AudioSource component and adds the parameter as the clip then plays it.
    //Used for background music. difference between this and Play are the volume and priority levels, also looping.
    private void PlayBackground(AudioClip clip)
    {
        GameObject newSound = Instantiate(new GameObject(), transform.position, Quaternion.identity);
        newSound.name = clip.name;
        newSound.transform.parent = gameObject.transform;

        newSound.AddComponent(typeof(AudioSource));
        AudioSource source = newSound.GetComponent<AudioSource>();
        source.clip = clip;
        source.volume = 0.2f;
        source.loop = true;
        source.Play();
    }
    #endregion
}
