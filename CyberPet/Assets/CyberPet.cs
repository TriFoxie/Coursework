using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CyberPet : MonoBehaviour
{
    [Header("Constants")]
    public float DECREASE;

    [Header("Pets")]
    public GameObject FatPetObj;
    public GameObject HappyPetObj;
    public GameObject CleanPetObj;
    [Space]
    public GameObject FatHudObj;
    public GameObject HappyHudHudObj;
    public GameObject CleanHudObj;
    [Space]
    public GameObject FatArrow;
    public GameObject HappyArrow;
    public GameObject CleanArrow;

    [Header("Particles")]
    public GameObject HeartParticle;
    public GameObject Drumstick;
    public GameObject Bubble;

    [Header("Animators")]
    public Animator ControlMenuAnimator;
    public Animator FAnimator;
    public Animator HAnimator;
    public Animator CAnimator;

    [Header("Messages")]
    public GameObject NoCoins;
    public Text Popup;

    [Header("Hud Sections")]
    public Text CoinsHud;
    public Text FatStats;
    public Text HappyStats;
    public Text CleanStats;
    [Space]
    public Slider FatHealthSlider;
    public Slider FatHappySlider;
    public Slider FatCleanSlider;
    public Slider FatFoodSlider;
    [Space]
    public Slider HappyHealthSlider;
    public Slider HappyHappySlider;
    public Slider HappyCleanSlider;
    public Slider HappyFoodSlider;
    [Space]
    public Slider CleanHealthSlider;
    public Slider CleanHappySlider;
    public Slider CleanCleanSlider;
    public Slider CleanFoodSlider;
    [Space]
    public GameObject GameOver;

    public Fatty FatPet = new Fatty();
    public Happy HappyPet = new Happy();
    public Clean CleanPet = new Clean();

    [Header("Random stuff")]
    private int coins = 0;
    public string error;
    public bool errorUp = false;
    private Pet selectedPet;
    public List<string> errorList = new List<string>();
    private Vector3 FatPopup3 = new Vector3(0, 6.5f, 0);
    private Vector3 HappyPopup3 = new Vector3(1.5f, 6.5f, 0);
    private Vector3 CleanPopup3 = new Vector3(-1.5f, 6.5f, 0);
    private Vector3 Active3 = new Vector3();
    private System.Random rnd = new System.Random();
    private bool ControlMenuActive = false;
    private string allStats;

    [Header("Counters")]
    private float PlayCool = 0.0f;
    private float FeedCool = 0.0f;
    private float ComfortCool = 0.0f;
    private float CleanCool = 0.0f;
    private float ControlMenuCool = 0.0f;
    private float BuyPetCool = 0.0f;
    private float PopupTimer = 0.0f;
    private float timeAlive = 0.0f;
    private int mapsUnlocked = 0;

    public void ErrorMessage()
    {
        Instantiate(NoCoins);
        errorUp = true;
    }
    //To display an error popup, set "error" to the text of the error needed.

    public void SpawnParticles(GameObject particle, int amount)
    {
        Vector3 SpawnLocation = Active3;
        float x = 0.0f;
        float y = 0.0f;
        float z = 0.0f;

        while (amount >= 0)
        {
            SpawnLocation += new Vector3(x, y, z);
            Instantiate(particle);
            particle.transform.position = SpawnLocation;
            SpawnLocation = Active3;
            if (x <= 0.5f) { x += 0.1f; y += 0.1f; z += 0.1f; }
            else { x -= 0.1f; y -= 0.1f; z -= 0.1f; }
            amount -= 1;
        }
    }

    public void BuyPet()
    {
        if (coins >= 20)
        {
            coins -= 20;
            Debug.Log("Pet Bought");
            if (!HappyPetObj.activeInHierarchy) { HappyPetObj.SetActive(true); }
            else if (!FatPetObj.activeInHierarchy) { FatPetObj.SetActive(true); }
            else if (!CleanPetObj.activeInHierarchy) { CleanPetObj.SetActive(true); }
            else { error = "All pets bought!"; }
        }
        else
        {
            error = "No coins left!";
        }
    }

    public void OpenControls()
    {
        if (ControlMenuActive) { ControlMenuAnimator.SetBool("IsActive", false); ControlMenuActive = false; }//Close Controls
        else { ControlMenuAnimator.SetBool("IsActive", true); ControlMenuActive = true; }//Open Controls
    }

    public void endStatsDump(ref float[] list)
    {
        float[] temp = { mapsUnlocked, timeAlive, coins };
        list = temp;
        Destroy(gameObject);
    }

    private void Start()
    {
        FatPetObj.SetActive(false);
        HappyPetObj.SetActive(false);
        CleanPetObj.SetActive(false);
        error = "none";
        coins = 100;
        HappyArrow.SetActive(false); FatArrow.SetActive(false); CleanArrow.SetActive(false);
        timeAlive = 0;
    }

    private void Update()
    {

        switch (selectedPet)
        {
            case Happy: Active3 = HappyPopup3; 
                FAnimator.SetBool("Selected", false); 
                CAnimator.SetBool("Selected", false); 
                HAnimator.SetBool("Selected", true); 
                break;
            case Clean: Active3 = CleanPopup3; 
                FAnimator.SetBool("Selected", false); 
                CAnimator.SetBool("Selected", true); 
                HAnimator.SetBool("Selected", false); 
                break;
            case Fatty: Active3 = FatPopup3; 
                FAnimator.SetBool("Selected", true); 
                CAnimator.SetBool("Selected", false); 
                HAnimator.SetBool("Selected", false); 
                break;
        }

        //game over if health < 10%
        allStats = FatPet.ReturnStatsVisual("all");
        allStats += HappyPet.ReturnStatsVisual("all");
        allStats += CleanPet.ReturnStatsVisual("all");

        if (allStats.Contains(" 0 ") || allStats.Contains("-"))
        {
            Debug.Log("Game Over");
            GameOver.SetActive(true);
        }

        //Counters/Timers/Cooldowns
        timeAlive += Time.deltaTime;
        PlayCool -= Time.deltaTime;
        FeedCool -= Time.deltaTime;
        CleanCool -= Time.deltaTime;
        ComfortCool -= Time.deltaTime;
        ControlMenuCool -= Time.deltaTime;
        BuyPetCool -= Time.deltaTime;

        //fatupdates
        if (FatPetObj.activeInHierarchy)
        {
            FatPet.ChangeStat("food", (FatPet.ReturnStats("food") - (DECREASE * Time.deltaTime)));
            FatPet.ChangeStat("happy", (FatPet.ReturnStats("happy") - (DECREASE * Time.deltaTime)));
            FatPet.ChangeStat("clean", (FatPet.ReturnStats("clean") - (DECREASE * Time.deltaTime)));
            FatPet.health = ((FatPet.ReturnStats("food") + FatPet.ReturnStats("happy") + FatPet.ReturnStats("clean")) / 3.0f) - 16;
        }
        //happyupdates
        if (HappyPetObj.activeInHierarchy)
        {
            HappyPet.ChangeStat("food", (HappyPet.ReturnStats("food") - (DECREASE * Time.deltaTime)));
            HappyPet.ChangeStat("happy", (HappyPet.ReturnStats("happy") - (DECREASE * Time.deltaTime)));
            HappyPet.ChangeStat("clean", (HappyPet.ReturnStats("clean") - (DECREASE * Time.deltaTime)));
            HappyPet.health = ((HappyPet.ReturnStats("food") + HappyPet.ReturnStats("happy") + HappyPet.ReturnStats("clean")) / 3.0f) -16;
        }
        //cleanupdates
        if (CleanPetObj.activeInHierarchy)
        {
            CleanPet.ChangeStat("food", (CleanPet.ReturnStats("food") - (DECREASE * Time.deltaTime)));
            CleanPet.ChangeStat("clean", (CleanPet.ReturnStats("clean") - (DECREASE * Time.deltaTime)));
            CleanPet.ChangeStat("happy", (CleanPet.ReturnStats("happy") - (DECREASE * Time.deltaTime)));
            CleanPet.health = ((CleanPet.ReturnStats("food") + CleanPet.ReturnStats("happy") + CleanPet.ReturnStats("clean")) / 3.0f) - 16;
        }

        //UI Updates
        CoinsHud.text = "Coins: " + coins.ToString();
        if (error != "none" && !errorUp)
        {
            ErrorMessage();
        }

        //Slider Updates Fat
        FatHealthSlider.value = FatPet.health;
        FatHappySlider.value = FatPet.ReturnStats("happy");
        FatFoodSlider.value = FatPet.ReturnStats("food");
        FatCleanSlider.value = FatPet.ReturnStats("clean");
        //Slider Updates Happy
        HappyHealthSlider.value = HappyPet.health;
        HappyHappySlider.value = HappyPet.ReturnStats("happy");
        HappyFoodSlider.value = HappyPet.ReturnStats("food");
        HappyCleanSlider.value = HappyPet.ReturnStats("clean");
        //Slider Updates Clean
        CleanHealthSlider.value = CleanPet.health;
        CleanHappySlider.value = CleanPet.ReturnStats("happy");
        CleanFoodSlider.value = CleanPet.ReturnStats("food");
        CleanCleanSlider.value = CleanPet.ReturnStats("clean");


        //Pet selection
        if (Input.GetKey(KeyCode.Alpha2) && FatPetObj.activeInHierarchy) { selectedPet = FatPet; HappyArrow.SetActive(false); FatArrow.SetActive(true); CleanArrow.SetActive(false); }
        else if (Input.GetKey(KeyCode.Alpha1) && HappyPetObj.activeInHierarchy) { selectedPet = HappyPet; HappyArrow.SetActive(true); FatArrow.SetActive(false); CleanArrow.SetActive(false); }
        else if (Input.GetKey(KeyCode.Alpha3) && CleanPetObj.activeInHierarchy) { selectedPet = CleanPet; HappyArrow.SetActive(false); FatArrow.SetActive(false); CleanArrow.SetActive(true); }
        else if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Alpha3)) { error = "Pet not bought yet!"; }

        //Pet Actions
        else if (Input.GetKey(KeyCode.E) && PlayCool <= 0) 
        { 
            selectedPet.Play(); 
            SpawnParticles(HeartParticle, 20); 
            PlayCool = 5;
            Popup.text = "Playing with " + selectedPet.name + "!\n+Max Happiness\n-1 Energy  |  -1 Cleanliness  |  -10 Food";
        }
        else if (Input.GetKey(KeyCode.F) && FeedCool <= 0) 
        {
            selectedPet.Feed(); 
            SpawnParticles(Drumstick, 5); 
            FeedCool = 3;
            Popup.text = "Feeding " + selectedPet.name + "!\n+10 Food  |  +1 Happiness\n-10 Cleanliness";
        }
        else if (Input.GetKey(KeyCode.W) && CleanCool <= 0) 
        { 
            selectedPet.Wash(); 
            CleanCool = 5;
            Popup.text = "Washing " + selectedPet.name + "!\n+Half max Cleanliness  |  +5 Energy";
            SpawnParticles(Bubble, 20);
        }
        else if (Input.GetKey(KeyCode.Space) && ComfortCool <= 0) 
        { 
            selectedPet.Comfort(); 
            SpawnParticles(HeartParticle, 10); 
            ComfortCool = 1;
            Popup.text = "Comforting " + selectedPet.name + "!\n+1 Happiness";
        }

        //Other Keys
        else if (Input.GetKey(KeyCode.Return) && BuyPetCool <= 0) { BuyPet(); BuyPetCool = 0.5f; }
        else if (Input.GetKey(KeyCode.Tab) && ControlMenuCool <= 0) { OpenControls(); ControlMenuCool = 1; }
        else if (Input.GetKey(KeyCode.J)) { Debug.Log(allStats); }

        if (Input.GetKeyDown(KeyCode.Mouse1)) { coins += 1; }
    }

    public class Pet
    {
        public string name;
        public float health;

        protected float food;
        protected float happy;
        protected float clean;
        protected float energy;

        protected float foodMax;
        protected float happyMax;
        protected float cleanMax;
        protected float energyMax;

        protected float foodMod;
        protected float happyMod;
        protected float cleanMod;
        protected float energyMod;

        public Pet()
        {
            food = 50;
            happy = 50;
            clean = 50;
            energy = 0;

            foodMax = 50;
            happyMax = 50;
            cleanMax = 50;
            energyMax = 100;

            foodMod = 0;
            happyMod = 0;
            cleanMod = 0;
        }

        public string ReturnStatsVisual(string stat)
        {
            switch (stat)
            {
                case "food": return this.food.ToString();
                case "happy": return this.happy.ToString();
                case "clean": return this.clean.ToString();
                case "all": return ("food: " + this.food.ToString() + " | happy: " + this.happy.ToString() + " | clean: " + this.clean.ToString());
                default: return "0";
            }
        }
        public float ReturnStats(string stat)
        {
            switch (stat)
            {
                case "food": return this.food;
                case "happy": return this.happy;
                case "clean": return this.clean;
                default: return 0;
            }
        }
        public int StatCheck()
        {
            if (this.food <= 0) { return 1; }
            if (this.happy <= 0) { return 2; }
            if (this.clean <= 0) { return 3; }

            if (this.food > this.foodMax) { this.food = this.foodMax; }
            if (this.happy > this.happyMax) { this.happy = this.happyMax; }
            if (this.clean > this.cleanMax) { this.clean = this.cleanMax; }
            if (this.energy > this.energyMax) { this.energy = this.energyMax; }

            this.health = ((this.food + this.happy + this.clean) / 3);

            return 0;
        }
        public void ChangeStat(string stat, float i)
        {
            switch (stat)
            {
                case "food": this.food = i; break;
                case "happy": this.happy = i; break;
                case "clean": this.clean = i; break;
            }
        }

        public void Play()
        {
            this.energy -= 1;
            this.clean -= 1;
            this.food -= 10;
            this.happy = this.happyMax;
            Debug.Log("You play with " + this.name + ". -1 Energy, -1 Clean, -10 food, Happiness maxed!");
        }
        public void Feed()
        {
            this.clean -= 10;
            this.food += 10;
            this.happy += 1;
            Debug.Log("You feed " + this.name + ". -10 Clean, +10 Food, +1 Happiness");
        }
        public void Wash()
        {
            this.energy += 1;
            this.clean += this.cleanMax / 2;
            this.happy -= 10;
            Debug.Log(this.name + " is clean now. +1 energy, +1/2 Max Cleanliness, -10 Happiness");
        }
        public void Comfort()
        {
            this.happy += 1;
            Debug.Log("YAY! +1 Happiness");
        }
    }
    public class Fatty : Pet
    {
        public Fatty()
        {
            this.food += 50;
            this.foodMax += 50;
            this.foodMod = 2;
            this.name = "Fatty";
        }

        public void FillFood()
        {
            this.food = this.foodMax;
            Debug.Log("NOMNOMNOMNOMNOMNOMNOM... Food to max! | Wait 2 minutes to use again");
        }
    }
    public class Happy : Pet
    {
        public Happy()
        {
            this.happy += 50;
            this.happyMax += 50;
            this.foodMod = 2;
            this.name = "Happy";
        }

        public void EnergyBoost()
        {
            this.energyMod = 5;
            Debug.Log("BOOSTING ENERGY!!!!!!!!!!!!!!!!!!!!!! [Energy * 5]");
            //Make it last for 5 mins, 2 min cooldown.
        }
    }
    public class Clean : Pet
    {
        public Clean()
        {
            this.clean += 50;
            this.cleanMax += 50;
            this.name = "Cleanie";
        }

        public void BecomeTidy()
        {
            this.cleanMod = 0; //lasts 2 minutes, 1 minute cooldown
        }
    }
}
