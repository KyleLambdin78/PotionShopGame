using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class StoreManager : MonoBehaviour
{
    public GameObject spawnSpot;
    public GameObject customer;
    public Canvas canvas;

    public Text goldText;
    public Text customerCountText;
    public Text timerText;
    public Text customersServed;
    public Text gameOverText;
    public Text goldEarned;
    public float goldAmount = 0;
    public float startTime = 60;
    private float positiveCustomers;
    private float totalCustomers;
    public List<GameObject> stars;
    private List<GameObject> currentCustomers = new List<GameObject>();

    public int customerCount = 3;
    public GameObject resultScreen;
    public GameObject gameOverScreen;

    void Start()
    {
        Debug.Log("Customer");
        positiveCustomers = 0;
        StartCoroutine(LevelTimer());
        goldAmount = PlayerPrefs.GetFloat("Gold", 0);
        goldText.text = "Gold:" + goldAmount;
        CreateCustomers();
    }
    public void Update()
    {
        if (Input.GetKeyDown("q"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown("a"))
        {
            DemoteStars();
        }
        if (Input.GetKeyDown("s"))
        {
            PromoteStars();
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
    public void DemoteStars()
    {
        positiveCustomers = 0;
        totalCustomers += 1;
        customerCountText.text = positiveCustomers.ToString();
        List<GameObject> activeStars = new List<GameObject>();
       for(int i = 0; i < stars.Count; i++)
        {
            if (stars[i].activeSelf == true)
            {
                activeStars.Add(stars[i]);
            }
        }
        activeStars[activeStars.Count - 1].SetActive(false);
        if (activeStars.Count - 1 == 0)
        {
            GameOver();
        }
    }
    public void PromoteStars()
    {
        positiveCustomers += 1;
        totalCustomers += 1;
        customerCountText.text = positiveCustomers.ToString();
        if(positiveCustomers % 10 == 0)
        {
            List<GameObject> inactiveStars = new List<GameObject>();
            for (int i = 0; i < stars.Count; i++)
            {
                if (stars[i].activeSelf == false)
                {
                    inactiveStars.Add(stars[i]);
                }
            }
            if (inactiveStars.Count != 0)
            {
                inactiveStars[inactiveStars.Count - 1].SetActive(true);
            }
                
        }
       
    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
        goldEarned.text = "Gold Earned:" + goldAmount.ToString();
        StopAllCoroutines();
        foreach (GameObject customer in currentCustomers)
        {
            Destroy(customer);
        }
    }
    public IEnumerator LevelTimer()
    {
        float t = startTime;
        while (t > 0)
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
        timerText.text = "0";
        TimesUp();
    }
    public void TimesUp()
    {
        resultScreen.SetActive(true);
        customersServed.text = totalCustomers.ToString();
        PlayerPrefs.SetFloat("Gold", goldAmount);
        foreach (GameObject customer in currentCustomers)
        {
            Destroy(customer);
        }
    }
    public void CombatScene()
    {
        SceneManager.LoadScene(1);
    }
 
}
