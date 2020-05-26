using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public IngredientClass ingredientClass;
    public GameObject droppedItem;
    public float knockbackForce;
    private bool isColliding = false;
    private bool takingDamage;
    public void TakeDamage()
    {
        if(takingDamage == false)
        {
            takingDamage = true;
            GameObject itemDrop = Instantiate(droppedItem, transform.position, Quaternion.identity);
            itemDrop.GetComponent<Pickup>().ingredientClass = ingredientClass;
            Destroy(this.gameObject);
        }
       
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(isColliding == false)
        {

            PlayerController player = collision.GetComponent<PlayerController>();
            if (player != null)
            {
                isColliding = true;
                player.TakeDamage();
                player.KnockBack(transform, knockbackForce);
                StartCoroutine(Reset());
            }
        }
    }
  
    public IEnumerator Reset()
    {
        yield return new WaitForSeconds(0.5f);
        isColliding = false;
    }
    public IEnumerator Move()
    {
        return null;
    }
}
