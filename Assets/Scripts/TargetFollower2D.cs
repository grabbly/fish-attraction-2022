using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetFollower2D : MonoBehaviour
{
    public Transform target;
    [SerializeField] private float speed = 5f;
    private Rigidbody2D body;
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (target != null && target.gameObject.activeSelf)
        {
            var rayToTarget = (target.position - transform.position);
            if(Vector2.Distance(target.position , transform.position) < 1)
            {
                body.velocity = (target.position - transform.position) * speed;
            }else
            {
                body.velocity = (target.position - transform.position).normalized * speed;
            }
        }
        
    }
}
