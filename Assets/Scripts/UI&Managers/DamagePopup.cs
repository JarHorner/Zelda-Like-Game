using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    private float timeInPlace;
    private Color textColor;

    void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    //instantiates the popup prefab in a certain position and number
    public DamagePopup Create(Vector3 position, int damageAmount) 
    {        
        Transform damagePopupTransform = Instantiate(this.gameObject.transform, position, Quaternion.identity);
        DamagePopup damagePopup = damagePopupTransform.GetComponent<DamagePopup>();
        damagePopup.Setup(damageAmount);

        return damagePopup;
    }

    //gives a value to the pop-up and sets disappear time, color and time in place.
    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 0.3f;
        timeInPlace = 0.1f;
    }

    void Update()
    {
        timeInPlace -= Time.deltaTime;
        //when the pop up has stayed in place for long enough, it moves upwards with its size decreasing each second.
        //once the disappear time has reached, the value will start fading out then be destoryed.
        if  (timeInPlace < 0)
        {
            float moveYSpeed = 3f;
            transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;
            textMesh.fontSize = 8;

            disappearTimer -= Time.deltaTime;
            if (disappearTimer < 0)
            {
                //Start disappearing
                float disappearSpeed = 5f;
                textColor.a -= disappearSpeed * Time.deltaTime;
                textMesh.color = textColor;
                textMesh.fontSize = 6;
                if (textColor.a < 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
