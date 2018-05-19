using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransition : MonoBehaviour
{    
    public float delay = 3.0f;
    public GameObject transitionImage;

    private void Start()
    {
        Setup();        
    }    

    public void Setup()
    {
        if(FindObjectOfType<PlayerCharacter>() != null)
        {
            FindObjectOfType<PlayerCharacter>().freeze = true;
        }

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

        Color backColourFinal = transitionImage.GetComponent<Image>().color;
        Color finalColor = new Color(backColourFinal.r, backColourFinal.g, backColourFinal.b, alphaVal);
        transitionImage.GetComponent<Image>().color = backColourFinal;

        Color textColourFinal = transitionImage.GetComponentInChildren<Text>().color;
        finalColor = new Color(textColourFinal.r, textColourFinal.g, textColourFinal.b, alphaVal);
        transitionImage.GetComponentInChildren<Text>().color = finalColor;

        if(alphaVal == 0)
        {
            StartCoroutine(DisplayName());
        }
    }

    IEnumerator DisplayName()
    {
        GameObject displayText = GameObject.Find("Name Display");
        int floorNum = FindObjectOfType<DungeonGeneration>().floorNum;

        if(floorNum == 1)
        {
            displayText.GetComponent<Text>().text = "Hades";
        }
        else if (floorNum == 1)
        {
            displayText.GetComponent<Text>().text = "Earth";
        }
        else if (floorNum == 1)
        {
            displayText.GetComponent<Text>().text = "Olympus";
        }

        yield return new WaitForSeconds(2);

        displayText.GetComponent<Text>().text = "";
    }
}
