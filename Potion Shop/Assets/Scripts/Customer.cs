using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class Customer : MonoBehaviour, IDropHandler
{
    public GameObject thoughtBubble;
    public float speed;
    public float distance;
    public Transform detection;
    public Vector3 defPosition;
    public Image potion;
    public Text timerText;
    public PotionClass[] potionClass;
    private float startTime;
    private PotionClass wantedPotion;
    private GameManager GameManager;
    private enum State {ApproachingCounter, AtCounter, OrderPlaced, OrderReceived}
    State customerState = State.ApproachingCounter;
    void Start()
    {
        timerText.enabled = false;
        startTime = 0;
        GameManager = FindObjectOfType<GameManager>();
        defPosition = this.transform.position;
        ResetCustomer();
        StartCoroutine(MoveRight());
    }

    public void PickPotion()
    {
        wantedPotion = potionClass[Random.Range(0, potionClass.Length)];
        potion.sprite = wantedPotion.potionSprite;
        thoughtBubble.SetActive(true);
    }
    public void CustomerServed()
    {
        GameManager.CreateCustomers();
        if(customerState == State.AtCounter)
        {
            thoughtBubble.SetActive(false);
            customerState = State.OrderPlaced;
            StartCoroutine(MoveDown());
        }
        else if(customerState == State.OrderPlaced)
        {
            customerState = State.OrderReceived;
            StartCoroutine(MoveDown());
        }
        
    }
    IEnumerator MoveDown()
    {
        while (customerState == State.OrderPlaced || customerState == State.OrderReceived)
        {
            RaycastHit2D rightHit = Physics2D.Raycast(detection.position, Vector2.down, distance);
            if (rightHit.collider == false)
            {
                transform.Translate(Vector2.down * speed * Time.deltaTime);
            }
            else if (rightHit.collider == true)
            {
                if(customerState == State.OrderPlaced)
                {
                    StartCoroutine(MoveRight());
                    yield break;
                }
                else if (customerState == State.OrderReceived)
                {
                    this.transform.position= defPosition;
                    ResetCustomer();
                    RestartCustomer();
                    yield break;
                }
               
            }

            yield return null;
        }
    }
    IEnumerator MoveRight()
    {
      while(customerState == State.ApproachingCounter || customerState == State.OrderPlaced)
        {
            RaycastHit2D rightHit = Physics2D.Raycast(detection.position, this.transform.right, distance);
            if (rightHit.collider == false)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
            }
            else if (rightHit.collider == true)
            {
                if(customerState == State.ApproachingCounter)
                {
                    customerState = State.AtCounter;
                    thoughtBubble.SetActive(true);
                    StartCoroutine(Timer());
                    yield break;
                }
                else if (customerState == State.OrderPlaced)
                {
                    thoughtBubble.SetActive(true);
                    yield break;
                }
                yield return true;
            }

            yield return null;
        }
        

    }
    public void ResetCustomer()
    {
        PickPotion();
        thoughtBubble.SetActive(false);
    }
    public void RestartCustomer()
    {
        customerState = State.ApproachingCounter;
        StartCoroutine(MoveRight());
    }
    public void OnDrop(PointerEventData eventData)
    {
        GameObject givenPotion = eventData.pointerDrag;
        if(givenPotion.GetComponent<PotionDisplay>().potion == wantedPotion && customerState == State.OrderPlaced)
        {
            customerState = State.OrderReceived;
            GameManager.AddGold(100);
            Destroy(givenPotion);
            StartCoroutine(MoveDown());
        }
        else if(givenPotion.GetComponent<PotionDisplay>(). potion != wantedPotion && customerState == State.OrderPlaced)
        {
            Destroy(givenPotion);
            Debug.Log("Wrong Potion");
            StartCoroutine(MoveDown());
        }
       
      
    }
    public IEnumerator Timer()
    {
        timerText.enabled = true;
        startTime = Time.time;
        while(customerState != State.OrderReceived)
        {
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f1");
            if(t >= 60)
            {
                timerText.text = minutes + ":" + seconds;
            }
            else
            {
                timerText.text = seconds;
            }
            
            if(t < 10)
            {
                timerText.color = Color.green;
            }
            else if(t > 10 && t < 20)
            {
                timerText.color = Color.yellow;
            }
            else
            {
                timerText.color = Color.red;
            }
            yield return null;
        }
        yield return new WaitForSeconds(2);
        startTime = Time.time;
        timerText.enabled = false;
        yield break;
    }
}
