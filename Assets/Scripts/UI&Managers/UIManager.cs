using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class UIManager : MonoBehaviour
{

    #region Variables
        private HealthManager healthManager;
        public Slider healthBar;
        public TMP_Text hpText;
        public GameObject pauseScreen;
        public GameObject inventoryScreen;
    #endregion

    #region Unity Methods

    // Start is called before the first frame update
    void Start()
    {
        healthManager = FindObjectOfType<HealthManager>();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.maxValue = healthManager.maxHealth;
        healthBar.value = healthManager.currHealth;
        //changes text of HP bar depending on previous calculations
        hpText.text = $"HP: {healthManager.currHealth}/{healthManager.maxHealth}";
    }

    #endregion
}
