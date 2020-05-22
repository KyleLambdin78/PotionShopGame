using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    public GameObject spawnSpot;
    public Canvas canvas;
    public Text goldText;
    public float goldAmount = 0;
    public Text timerText;
    public float startTime = 60; 
    public GameObject customer;
    public int customerCount = 3;
    private List<GameObject> currentCustomers = new List<GameObject>();
    
    void Start()
    {
        StartCoroutine(LevelTimer());
        goldText.text = "Gold:" + goldAmount;
        CreateCustomers();
    }
    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("escape"))
        {
            Application.Quit();
        }
    }
    public void CreateCustomers()
    {
        if(currentCustomers.Count < customerCount)
        {
            GameObject spawnedCustomer = Instantiate(customer, new Vector3(spawnSpot.transform.position.x, spawnSpot.transform.position.y, spawnSpot.transform.position.z), Quaternion.identity, canvas.transform);
            currentCustomers.Add(spawnedCustomer);
        }
      
    }
    public void AddGold(float gold)
    {
        goldAmount += gold;
        goldText.text = "Gold:" + goldAmount;
    }
    public void GameOver()
    {

    }
    public IEnumerator LevelTimer()
    {
        float t = startTime;
        while (t >= 0)
        {
            t = startTime - Time.timeSinceLevelLoad;
            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");
            if (t >= 60)
            {
                timerText.text = minutes + ":" + seconds;
            }
            else
            {
                timerText.text = seconds;
            }
            yield return null;
        }
       yield return new WaitForSeconds(1);
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);   
    }
    
 
}
