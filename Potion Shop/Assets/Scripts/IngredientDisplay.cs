using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    public IngredientClass ingredient;
    public Text ingredientText;
    public Image image;
    public int defaultAmount;
    public Text itemAmountText;
    public CanvasGroup canvasG;
    [SerializeField]private int itemAmount;

    void Start()
    {
        itemAmount = PlayerPrefs.GetInt(ingredient.ingredientName, defaultAmount);
        ingredientText.text = ingredient.ingredientName;
        image.sprite = ingredient.ingredientSprite;
    }

    // Update is called once per frame
    void Update()
    {
        if(itemAmount <= 0)
        {
            image.enabled = false;
            canvasG.blocksRaycasts = false;
        }
    }
    public void DecrementItemAmount()
    {
        itemAmount -= 1;
        PlayerPrefs.SetInt(ingredient.ingredientName, itemAmount);
    }
}
