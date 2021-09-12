using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class DamagePopup : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
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

    public void Setup(int damageAmount)
    {
        textMesh.SetText(damageAmount.ToString());
        textColor = textMesh.color;
        disappearTimer = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 3f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        Debug.Log(disappearTimer);
        if (disappearTimer < 0)
        {
            //Start disappearing
            float disappearSpeed = 5f;
            textColor.a -= disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
