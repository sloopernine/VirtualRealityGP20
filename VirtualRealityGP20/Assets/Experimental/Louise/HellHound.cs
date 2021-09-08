using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HellHound : Enemy
{
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Enemy")
        {
            TakeDamage(50);
        }
    }
}
