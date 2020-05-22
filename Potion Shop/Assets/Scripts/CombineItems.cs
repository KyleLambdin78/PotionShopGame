using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;

public class CombineItems : MonoBehaviour
{
    public GameObject[] ingredientSlots;
    public GameObject resultSlot;
    public List<RecipeClass> Recipes;
    public RectTransform rectTransform;
    public List<IngredientClass> currentIngredients;
    private ItemSlot resultSlotScript;

    void Start()
    {
        resultSlotScript = resultSlot.GetComponent<ItemSlot>();
        currentIngredients = new List<IngredientClass>();
        if(resultSlot != null)
        {
            rectTransform = resultSlot.GetComponent<RectTransform>();
        }
         
    }

    public void CreateItem(IngredientClass ingredient)
    {
        currentIngredients.Add(ingredient);
        if(currentIngredients.Count == 2)
        {
            StartCoroutine(CheckResultSlot());
        }
      
    }
    public IEnumerator CheckResultSlot()
    {
        while (currentIngredients.Count == 2)
        {
            if(resultSlotScript.hasItem == false)
            {
                currentIngredients.Sort((x, y) => x.ID.CompareTo(y.ID));
                foreach (RecipeClass recipe in Recipes)
                {
                    if (recipe.Ingredients.SequenceEqual(currentIngredients))
                    {
                        GameObject createdPotion = Instantiate(recipe.Potion, new Vector3(resultSlot.transform.position.x, resultSlot.transform.position.y, resultSlot.transform.position.z), Quaternion.identity, this.transform);
                        createdPotion.GetComponent<DragDrop>().itemSlot = resultSlot;
                        resultSlotScript.hasItem = true;
                        currentIngredients.Clear();
                        CallClearItems();
                        yield break;
                    }
                }
            }
            else
            {
                yield return new WaitForSeconds(1);
            }
        }
        yield break;
    }
    public void CallClearItems()
    {
        foreach(GameObject ingredientSlot in ingredientSlots)
        {
            ingredientSlot.GetComponent<ItemSlot>().ClearItems();
        }
    }
}
