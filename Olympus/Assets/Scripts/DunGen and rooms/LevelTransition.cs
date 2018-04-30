using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{
    public int floor = 0;
    public float delay = 3.0f;
    private GameObject transitionImage;
    private bool setup;

    private void Start()
    {
        Setup();
        
    }    

    public void Setup()
    {
        floor++;
        setup = true;
        transitionImage = GameObject.Find("Transition");

        // Works on first load but not for second floor
        transitionImage.SetActive(true);
        Invoke("TurnOffTransition", delay);

    }

    private void TurnOffTransition()
    {
        transitionImage.SetActive(false);
        setup = false;
    }

    private void Update()
    {
        if(setup == true)
        {
            // freeze controls
        }
    }

    private void GameOver()
    {
        // freeze controls and display game over menu
        // Can restart or exit to main menu
    }
}
