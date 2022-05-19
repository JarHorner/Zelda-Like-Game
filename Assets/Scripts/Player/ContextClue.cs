using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    #region Variables
    [SerializeField] private List<Sprite> sprites;
    private Dictionary<string, Sprite> contextSprites;
    [SerializeField] private SpriteRenderer sprite;

    #endregion

    #region  Methods

    private void Start() 
    {
        contextSprites = new Dictionary<string, Sprite>();
        for (int i = 0; i < sprites.Count; i++)
        {
            contextSprites.Add(sprites[i].name, sprites[i]);
        }
    }

    public void Speach() 
    {
        sprite.enabled = true;
        sprite.sprite = contextSprites["Speach_0"];
    }

    public void Interact() 
    {
        sprite.enabled = true;
        sprite.sprite = contextSprites["Interact"];
    }

    public void Talk() 
    {
        sprite.enabled = true;
        sprite.sprite = contextSprites["Talk"];
    }

    public void Disappear()
    {
        sprite.enabled = false;
    }

    #endregion
}
