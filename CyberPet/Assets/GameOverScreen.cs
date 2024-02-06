using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverScreen : MonoBehaviour
{
    public CyberPet cyberPet;
    public Text Stats;

    private float[] stats;
    private int timeAlive;
    private int mapsUnlocked;
    private int coins;

    void Start()
    {
        cyberPet.endStatsDump(ref stats);
    }

    void Update()
    {
        Stats.text = stats[0].ToString() + "\n" + (Mathf.Round((stats[1] / 60) * 100) / 100).ToString() + ":00\n" + stats[2].ToString();
    }
}
