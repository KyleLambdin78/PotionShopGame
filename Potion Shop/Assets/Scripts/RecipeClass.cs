using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewRecipe", menuName = "Recipe")]
public class RecipeClass : ScriptableObject
{
  public List<IngredientClass> Ingredients;
  public GameObject Potion;
    
  public void Sort()
    {

    }
 
}
