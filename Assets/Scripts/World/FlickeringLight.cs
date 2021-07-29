using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class FlickeringLight : MonoBehaviour
{
    #region Variables
        private Light2D flickeringLight;
        private PlayerController player;
        private bool isRunning = false;

    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        flickeringLight = this.GetComponent<Light2D>();
        player = FindObjectOfType<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate() 
    {
        if(!isRunning) StartCoroutine(Flicker());
    }

    IEnumerator Flicker() 
    {
        isRunning = true;
        float locationDiff = Vector3.Distance(player.transform.position, this.transform.position);
        while (locationDiff < 16)
        {
            flickeringLight.intensity = 0.7f;
            flickeringLight.pointLightInnerRadius = 1f;
            yield return new WaitForSeconds(0.1f);

            flickeringLight.intensity = 0.8f;
            flickeringLight.pointLightInnerRadius = 1.16f;
            yield return new WaitForSeconds(0.1f);

            flickeringLight.intensity = 0.9f;
            flickeringLight.pointLightInnerRadius = 1.33f;
            yield return new WaitForSeconds(0.1f);

            flickeringLight.intensity = 1f;
            flickeringLight.pointLightInnerRadius = 1.5f;
            yield return new WaitForSeconds(0.1f);

            flickeringLight.intensity = 0.9f;
            flickeringLight.pointLightInnerRadius = 1.33f;
            yield return new WaitForSeconds(0.1f);

            flickeringLight.intensity = 0.8f;
            flickeringLight.pointLightInnerRadius = 1.16f;
            yield return new WaitForSeconds(0.1f);

            locationDiff = Vector3.Distance(player.transform.position, this.transform.position);
        }
        isRunning = false;
    }

    #endregion
}
