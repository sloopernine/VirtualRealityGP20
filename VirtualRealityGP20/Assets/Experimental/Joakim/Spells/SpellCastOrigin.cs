using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;


public enum Spell
{
    Fireball,
    Teleport,
    CreateRock,

}

public class SpellCastOrigin : MonoBehaviour
{
    public InputAction castAction;
    public InputAction swapCurrentSpellAction;

    public Spell currentSpell;

    public GameObject fireBallPrefab;
    public GameObject RockPrefab;
    public float teleportRange;


    private void OnEnable()
    {
        castAction.Enable();
        swapCurrentSpellAction.Enable();
    }

    private void OnDisable()
    {
        castAction.Disable();
        swapCurrentSpellAction.Disable();
    }

    private void Awake()
    {
        castAction.performed += ctx =>
        {
            CastSpell();
        };

        swapCurrentSpellAction.performed += ctx =>
        {
            SwapSpell();
        };

    }

    public void CastSpell()
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
    private void SwapSpell()
    {
        currentSpell++;
        int amountOfSpells = System.Enum.GetValues(typeof(Spell)).Length;
        int spellIndex = (int)currentSpell;
        spellIndex = spellIndex % amountOfSpells;
        currentSpell = (Spell)spellIndex;
        
        print(System.Enum.GetName(typeof(Spell), currentSpell) + " is ready");
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
            Debug.DrawRay(transform.position, transform.forward * 1000f, Color.red, 3f);

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
