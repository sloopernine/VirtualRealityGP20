using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Interactions;

public class SpellConjuror : MonoBehaviour
{

    public InputAction conjureCurrentSpellAction;
    public InputAction castAction;
    public InputAction swapCurrentSpellAction;

    public Spell currentSpell;
    public GameObject spellBallPrefab;


    private void Awake()
    {

        conjureCurrentSpellAction.performed += ctx =>
        {
            ConjureSpell();
        };
        castAction.performed += ctx =>
        {
            CastSpell();
        };

        swapCurrentSpellAction.performed += ctx =>
        {
            SwapSpell();
        };


    }

    private void OnEnable()
    {
        castAction.Enable();
        swapCurrentSpellAction.Enable();
        conjureCurrentSpellAction.Enable();

    }

    private void OnDisable()
    {
        castAction.Disable();
        swapCurrentSpellAction.Disable();
        conjureCurrentSpellAction.Disable();

    }


    public void CastSpell()
    {
        //Find spell with ray
        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
        {
            Debug.DrawLine(transform.position, transform.forward * 1000f, Color.red, 3f);

            SpellCastOrigin spellCastOrigin = hit.transform.GetComponent<SpellCastOrigin>();

            if (spellCastOrigin != null)
            {

            }


        }


        //Cast its spell
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
        spellCastOrigin.transform.rotation = transform.rotation;


    }
}
