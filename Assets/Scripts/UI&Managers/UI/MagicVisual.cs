using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagicVisual : MonoBehaviour
{
    #region Variables
    public static MagicSystem magicSystemStatic;
    [SerializeField] private int magic;
    private float periodicTime = 0.1f;
    private float fullMagicAnimTime = 0.4f;
    private PlayerController player;
    private bool isRecovering = false;
    [SerializeField] private Sprite magic0Sprite;
    [SerializeField] private Sprite magic1Sprite;
    [SerializeField] private Sprite magic2Sprite;
    [SerializeField] private Sprite magic3Sprite;
    [SerializeField] private Sprite magic4Sprite;
    private List<MagicImage> magicImageList;
    private MagicImage currentMagic;
    private MagicSystem magicSystem;
    #endregion

    #region Methods

    void Awake() 
    {
        magicImageList = new List<MagicImage>();
    }

    void Start() 
    {
        if (magicSystemStatic == null)
        {
            Debug.Log("Start health");
            magicSystem = new MagicSystem(magic);
            player = FindObjectOfType<PlayerController>();
            SetMagicSystem(magicSystem);
            currentMagic = magicImageList[magicImageList.Count-1];
        }
    }

    void Update() 
    {
        periodicTime -= Time.deltaTime;
        if (periodicTime < 0f)
        {
            RecoveringAnimatedPeriodic();
            periodicTime = 0.1f;
        }
        fullMagicAnimTime -= Time.deltaTime;
        if (fullMagicAnimTime < 0f)
        {
            fullMagicAnimTime = 0.4f;
        }
    }

    public void SetMagicSystem(MagicSystem magicSystem)
    {
        this.magicSystem = magicSystem;
        magicSystemStatic = magicSystem;

        List<MagicSystem.Magic> magicList = magicSystem.MagicList;
        int row = 0;
        int col = 0;
        int colMax = 8;
        float rowColSize = 75f;
        for (int i=0; i< magicList.Count; i++)
        {
            MagicSystem.Magic magic = magicList[i];
            Debug.Log(magic.Fragments);
            Vector2 magicAnchoredPos = new Vector2(col * rowColSize, -row * rowColSize);
            CreateMagicImage(magicAnchoredPos).SetMagicFragments(magic.Fragments);
            
            col++;
            if (col >= colMax)
            {
                row++;
                col = 0;  
            }
            
        }
        magicSystem.OnUsed += HealthSystem_OnUsed;
        magicSystem.OnRecovered += HealthSystem_OnRecovered;
        magicSystem.OnEmpty += HealthSystem_OnEmpty;
    }

    private void HealthSystem_OnUsed(object sender, System.EventArgs e)
    {
        //hearts health system was damaged
        RefreshAllMagic();
    }

    private void HealthSystem_OnRecovered(object sender, System.EventArgs e)
    {
        //hearts health system was healed
        isRecovering = true;
    }
    private void HealthSystem_OnEmpty(object sender, System.EventArgs e)
    {
        //hearts health system is dead
        Debug.Log("Out of Magic!");
    }

    private void RefreshAllMagic()
    {
        List<MagicSystem.Magic> magicList = magicSystem.MagicList;
        for (int i=0; i< magicImageList.Count; i++)
        {
            MagicImage magicImage = magicImageList[i];
            MagicSystem.Magic magic = magicList[i];
            magicImage.SetMagicFragments(magic.Fragments);
            if (magicImage.Fragments != 0)
                currentMagic = magicImage;
        }
    }

    private void RecoveringAnimatedPeriodic()
    {
        if (isRecovering)
        {
            bool fullyRecovered = true;
            List<MagicSystem.Magic> magicList = magicSystem.MagicList;
            for (int i=0; i< magicList.Count; i++)
            {
                MagicImage magicImage = magicImageList[i];
                MagicSystem.Magic magic = magicList[i];
                if (magicImage.Fragments != magic.Fragments)
                {
                    //visual is different from logic
                    magicImage.AddMagicVisualFragment();
                    fullyRecovered = false;
                    break;
                }
                if (magicImage.Fragments != 0)
                    currentMagic = magicImage;
            }
            if (fullyRecovered)
            {
                isRecovering = false;
            }
        }
    }
    private MagicImage CreateMagicImage(Vector2 anchoredPos)
    {
        //create game object
        GameObject heartGameObject = new GameObject("Heart", typeof(Image), typeof(Animation));
        //set as child of this transform
        heartGameObject.transform.SetParent(transform);
        heartGameObject.transform.localPosition = Vector3.zero;

        //locate and size heart
        heartGameObject.GetComponent<RectTransform>().anchoredPosition = anchoredPos;
        heartGameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(65, 60);

        //set heart sprite
        Image heartImageUI = heartGameObject.GetComponent<Image>();
        heartImageUI.sprite = magic0Sprite;
        
        MagicImage heartImage = new MagicImage(this, heartImageUI, heartGameObject.GetComponent<Animation>());
        magicImageList.Add(heartImage);

        return heartImage;
    }

    //represents a single heart
    public class MagicImage
    {
        private int fragments;
        private Image magicImage;
        private MagicVisual magicVisual;
        private Animation animation;

        public MagicImage(MagicVisual magicVisual, Image magicImage, Animation animation)
        {
            this.magicVisual = magicVisual;
            this.magicImage = magicImage;
            this.animation = animation;
        }

        public void SetMagicFragments(int fragments)
        {
            this.fragments = fragments;
            Debug.Log(magicImage);
            switch (fragments)
            {
                case 0: magicImage.sprite = magicVisual.magic4Sprite; break;
                case 1: magicImage.sprite = magicVisual.magic3Sprite; break;
                case 2: magicImage.sprite = magicVisual.magic2Sprite; break;
                case 3: magicImage.sprite = magicVisual.magic1Sprite; break;
                case 4: magicImage.sprite = magicVisual.magic0Sprite; break;
            }
        }

        public void AddMagicVisualFragment()
        {
            SetMagicFragments(fragments + 1);
        }

        public int Fragments
        {
            get { return fragments; }
        }
        
    }

    #endregion
}
