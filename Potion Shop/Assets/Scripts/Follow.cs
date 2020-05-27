using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public float speed;
    public float chaseDistance;
    private Transform target;
    void Start()
    {
        target = GameObject.FindObjectOfType<PlayerController>().attackPos;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) < chaseDistance)
        {
            if (target != null)
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

            }
        }
    }
 
}

