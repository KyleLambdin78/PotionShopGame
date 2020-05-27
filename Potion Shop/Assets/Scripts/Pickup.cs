using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [HideInInspector]public IngredientClass ingredientClass;
    private SpriteRenderer itemSprite;
    private int itemCount;
    void Start()
    {
        itemSprite = GetComponent<SpriteRenderer>();
        itemSprite.sprite = ingredientClass.ingredientSprite;
        itemCount = PlayerPrefs.GetInt(ingredientClass.ingredientName, 20);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();
        if(player != null)
        {
            itemCount += 1;
            PlayerPrefs.SetInt(ingredientClass.ingredientName, itemCount);
            Destroy(this.gameObject);
        }
    }
    
}
