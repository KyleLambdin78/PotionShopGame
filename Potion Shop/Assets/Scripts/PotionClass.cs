using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewPotion", menuName = "Potion")]
public class PotionClass : ScriptableObject
{
    public string potionName;
    public int price;
    public Sprite potionSprite;
}
