using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ResultScreen : MonoBehaviour
{
    public Text oozeText;
    public Text wingText;
    public Text eyeText;
    public Text enemiesText;
    private int startOozeAmount;
    private int startWingAmount;
    private int startEyeAmount;

    void Start()
    {
        startOozeAmount = PlayerPrefs.GetInt("Ooze",20);
        startWingAmount = PlayerPrefs.GetInt("Wing",20);
        startEyeAmount = PlayerPrefs.GetInt("Eye Ball",20);
    }
    public void ShowResults()
    {
        int endOozeAmount = PlayerPrefs.GetInt("Ooze", 20);
        int endWingAmount = PlayerPrefs.GetInt("Wing", 20);
        int endEyeAmount = PlayerPrefs.GetInt("Eye Ball", 20);
        int OozeDifference = endOozeAmount - startOozeAmount;
        int WingDifference = endWingAmount - startWingAmount;
        int EyeDifference = endEyeAmount - startEyeAmount;
        oozeText.text = "Ooze: " + OozeDifference.ToString();
        wingText.text = "Wing: " + WingDifference.ToString();
        eyeText.text = "Eyes: "  + EyeDifference.ToString();
        enemiesText.text = "Total Enemies Killed = " + (OozeDifference + WingDifference + EyeDifference).ToString();
    }
    public void ReturnToStore()
    {
        SceneManager.LoadScene(0);
    }
    void Update()
    {
        
    }
}
