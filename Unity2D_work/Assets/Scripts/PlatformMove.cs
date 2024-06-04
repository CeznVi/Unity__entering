using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMove : MonoBehaviour
{
   [SerializeField] private Rigidbody2D rb;

    public float speed;
    public float delayTime;
    public Transform[] points;
   
    private int lastPointIndex;
    private Transform target => points[lastPointIndex];
    private float move=1;

    private void Awake()
    {
        lastPointIndex = 0;
    }

    private void FixedUpdate()
    {
        Vector2 direction = target.position - transform.position;
        rb.velocity= direction.normalized * speed*move;

        if (direction.magnitude < 0.1f)
        { 
            lastPointIndex=lastPointIndex+1>=points.Length? 0 : lastPointIndex + 1;
            StartCoroutine(delay());
        }
    }

    IEnumerator delay()
    { 
        move = 0;
        yield return new WaitForSeconds(delayTime);
        move = 1;
    }
}
