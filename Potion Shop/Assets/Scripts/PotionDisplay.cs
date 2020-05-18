using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PotionDisplay : MonoBehaviour
{
    public PotionClass potion;
    public Text potionText;
    public Image image;
    
    void Start()
    {
        potionText.text = potion.potionName;
        image.sprite = potion.potionSprite;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
