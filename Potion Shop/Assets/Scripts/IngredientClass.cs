using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewIngredient", menuName = "Ingredient")]
public class IngredientClass : ScriptableObject
{
    public string ingredientName;
    public Sprite ingredientSprite;
    public int ID;

}
