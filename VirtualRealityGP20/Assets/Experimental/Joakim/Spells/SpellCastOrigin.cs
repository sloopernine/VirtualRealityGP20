using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum Spell
{
    Fireball,
    Teleport,
    CreateRock,

}

public class SpellCastOrigin : MonoBehaviour
{

    public GameObject fireBallPrefab;
    public GameObject RockPrefab;

    public Spell currentSpell;


    public void CastSpell(Spell currentSpell)
    {
        switch (currentSpell)
        {
            case Spell.Fireball:
                Fireball();
                break;
            case Spell.Teleport:
                Teleport();

                break;
            case Spell.CreateRock:
                CreateRock();

                break;
            default:
                break;
        }
    }

    private void Fireball()
    {
        GameObject ball = Instantiate(fireBallPrefab);
        ball.transform.position = transform.position + transform.forward * 0.6f;
        ball.transform.rotation = transform.rotation;
    }

    private void Teleport()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
        {
            Debug.DrawLine(transform.position, transform.forward * 1000f, Color.red, 3f);

            Vector3 newPosition = new Vector3(hit.point.x, transform.position.y, hit.point.z);
            transform.position = newPosition;

        }
        
    }

    private void CreateRock()
    {
        GameObject rock = Instantiate(RockPrefab);
        rock.transform.position = transform.position + transform.forward * 1.5f + transform.up;
    }
}
