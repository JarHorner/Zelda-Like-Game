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
    float moveYSpeed;
    float moveXSpeed;

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
        moveYSpeed = 3f;
        moveXSpeed = Random.Range(-2, 2);
    }

    void Update()
    {
        timeInPlace -= Time.deltaTime;
        //when the pop up has stayed in place for long enough, it moves upwards with its size decreasing each second.
        //once the disappear time has reached, the value will start shinking down then be destoryed.
        if (disappearTimer < 0)
        {
            transform.position += new Vector3(moveXSpeed, -moveYSpeed) * Time.deltaTime;
            //Start shrinking
            float disappearSpeed = 8f;
            textMesh.fontSize -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textMesh.fontSize < 0)
            {
                Destroy(gameObject);
            }
        }
        else if  (timeInPlace < 0)
        {
            transform.position += new Vector3(moveXSpeed, moveYSpeed) * Time.deltaTime;
            textMesh.fontSize = 8;

            disappearTimer -= Time.deltaTime;
        }
    }
}
