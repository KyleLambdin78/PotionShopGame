using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public GameObject tutorialScreen;
    public string whichTutorial;
    private int timesWithTutorial;
    void Start()
    {
        timesWithTutorial = PlayerPrefs.GetInt(whichTutorial, 0);
        if (timesWithTutorial < 1)
        {
            Time.timeScale = 0;
        }
        else
        {
            Destroy(this.gameObject);
        }
        
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            Time.timeScale = 1;
            timesWithTutorial += 1;
            PlayerPrefs.SetInt(whichTutorial, timesWithTutorial);
            tutorialScreen.SetActive(false);
        }
    }
}
