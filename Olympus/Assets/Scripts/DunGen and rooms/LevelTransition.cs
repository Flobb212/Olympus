using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{    
    public float delay = 3.0f;
    public GameObject transitionImage;

    private void Start()
    {
        transitionImage.SetActive(true);
        Setup();        
    }    

    public void Setup()
    {
        FindObjectOfType<PlayerCharacter>().freeze = true;

        if(transitionImage == null)
        {
            transitionImage = GameObject.Find("Transition");
        }
        
        StartCoroutine(Fade(1.0f));
        Invoke("TurnOffTransition", delay);

    }

    private void TurnOffTransition()
    {
        StartCoroutine(Fade(0.0f));
        FindObjectOfType<PlayerCharacter>().freeze = false;
    }

    IEnumerator Fade(float alphaVal)
    {
        float alpha = transitionImage.GetComponent<Image>().color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / 1.0f)
        {
            Color backColour = transitionImage.GetComponent<Image>().color;
            Color textColour = transitionImage.GetComponentInChildren<Text>().color;
            backColour.a = Mathf.Lerp(alpha, alphaVal, t);
            textColour.a = Mathf.Lerp(alpha, alphaVal, t);
            transitionImage.GetComponent<Image>().color = backColour;
            transitionImage.GetComponentInChildren<Text>().color = textColour;
            yield return null;
        }
    }

    private void GameOver()
    {
        // freeze controls and display game over menu
        // Can restart or exit to main menu
    }
}
