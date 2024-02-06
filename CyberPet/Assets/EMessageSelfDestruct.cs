using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EMessageSelfDestruct : MonoBehaviour
{
    private float timer;
    public CyberPet CyberPetScript;
    public Text self;

    private void Start()
    {
        timer = 2.5f;
        CyberPetScript = FindFirstObjectByType<CyberPet>();
        self = GetComponentInChildren<Text>();
        self.text = CyberPetScript.error;
        CyberPetScript.error = "none";
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            CyberPetScript.errorUp = false;
            Destroy(gameObject);
        }
    }
}
