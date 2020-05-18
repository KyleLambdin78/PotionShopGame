using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    public IngredientClass ingredient;
    public Text ingredientText;
    public Image image;

    void Start()
    {
        ingredientText.text = ingredient.ingredientName;
        image.sprite = ingredient.ingredientSprite;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
