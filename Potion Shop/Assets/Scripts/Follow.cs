using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float speed;

    private Transform target;
    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerController>().attackPos;
        StartCoroutine(FollowPlayer());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    IEnumerator FollowPlayer()
    {
        while (true)
        {
            if(target != null)
            {
                float oldTransform = transform.position.x;
                transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
                if (oldTransform > transform.position.x)
                {
                    transform.localScale = new Vector2(1, transform.localScale.y);
                }
                else if (oldTransform < transform.position.x)
                {
                    transform.localScale = new Vector2(-1, transform.localScale.y);
                }
                yield return null;
            }
           
        }
        
    }
}

