using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Customer : MonoBehaviour
{
    public GameObject thoughtBubble;
    public float speed;
    public float distance;
    public Transform detection;
    public Image potion;
    public PotionClass[] potionClass;
    private bool atCounter;
    void Start()
    {
        PickPotion();
        //StartCoroutine(MoveRight());
    }
    public void Update()
    {
        
        RaycastHit2D rightHit = Physics2D.Raycast(detection.position, Vector2.right, distance);
        if (rightHit.collider == false)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
        else
        {

        }
        
    }
    public void PickPotion()
    {
        PotionClass wantedPotion;
        wantedPotion = potionClass[Random.Range(0, potionClass.Length)];
        potion.sprite = wantedPotion.potionSprite;
        thoughtBubble.SetActive(true);
    }
    public void CustomerServed()
    {
        thoughtBubble.SetActive(false);
        StartCoroutine(MoveDown());
    }
    IEnumerator MoveDown()
    {
        yield return true;
    }
    IEnumerator MoveRight()
    {
        RaycastHit2D rightHit = Physics2D.Raycast(detection.position, Vector2.right, distance);
        while (atCounter == false)
        {
            
            if(rightHit.collider == true)
            {
                transform.Translate(Vector2.right * speed * Time.deltaTime);
                atCounter = true;
                yield return null;
            }
            
        }
        

    }
}
