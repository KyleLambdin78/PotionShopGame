using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour
{
    public GameObject spawnSpot;
    public Canvas canvas;
    public Text goldText;
    public float goldAmount = 0;
    public GameObject customer;
    public int customerCount = 3;
    private List<GameObject> currentCustomers = new List<GameObject>();
    
    void Start()
    {
        goldText.text = "Gold:" + goldAmount;
        CreateCustomers();
    }

    public void CreateCustomers()
    {
        if(currentCustomers.Count < customerCount)
        {
            GameObject spawnedCustomer = Instantiate(customer, new Vector3(spawnSpot.transform.position.x, spawnSpot.transform.position.y, spawnSpot.transform.position.z), Quaternion.identity, canvas.transform);
            currentCustomers.Add(spawnedCustomer);
        }
        else
        {
            CallRestartCustomer();
        }
    }
    public void AddGold(float gold)
    {
        goldAmount += gold;
        goldText.text = "Gold:" + goldAmount;
    }
    public void CallRestartCustomer()
    {
       
    }
    public void MoveLine()
    {
        foreach (GameObject customer in currentCustomers)
        {
            customer.GetComponent<Customer>().StartCoroutine("MoveRight()");
        }
    }
}
