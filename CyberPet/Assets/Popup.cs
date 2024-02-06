using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{

    public Animator ani;

    private float timer;
    private string prevText;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;

        if (GetComponent<Text>().text != prevText)
        {
            ani.SetBool("PopupOn", true);
            timer = 2.5f;
        }
        else if ((timer < 0.0f) && GetComponent<Text>().text == prevText)
        {
            ani.SetBool("PopupOn", false);
        }

        prevText = GetComponent<Text>().text;
    }
}
