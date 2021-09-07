using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellConjuror : MonoBehaviour
{

    //public InputAction conjureCurrentSpellAction;
    //public InputAction castAction;
    //public InputAction swapCurrentSpellAction;

    public Spell currentSpell;
    public GameObject spellBallPrefab;

    public GameObject fireBallPrefab;
    public GameObject RockPrefab;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            FindSpell();

        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            SwapSpell();

        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            ConjureSpell();

        }
    }



    public void FindSpell()
    {
        //Find spell with ray
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
        {
            Debug.DrawLine(transform.position, transform.forward * 1000f, Color.red, 3f);

            SpellCastOrigin spellCastOrigin = hit.transform.GetComponent<SpellCastOrigin>();

            if (spellCastOrigin != null)
            {
                
                spellCastOrigin.CastSpell(transform.position);
                
            }


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

    private void ConjureSpell()
    {

        SpellCastOrigin spellCastOrigin = Instantiate(spellBallPrefab).GetComponent<SpellCastOrigin>();
        spellCastOrigin.currentSpell = currentSpell;
        spellCastOrigin.transform.position = transform.position + transform.forward;
        spellCastOrigin.transform.rotation = new Quaternion(0, transform.rotation.y, 0f, 0f);


    }

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
