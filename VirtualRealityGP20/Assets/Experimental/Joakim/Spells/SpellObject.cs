using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObject : MonoBehaviour
{
    public float initialLifeTime;
    public float initialVelocity;
    private Rigidbody rb;
    private float aliveTime;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * initialVelocity);
        aliveTime = 0f;

    }

    void Update()
    {
        aliveTime += Time.deltaTime;
        if (aliveTime > initialLifeTime)
        {
            Destroy(gameObject);
        }
    }
}
