using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellObjectHolder : MonoBehaviour
{


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ToggleHoldSpellObject();

        }
    }

    private void ToggleHoldSpellObject()
    {
        SpellObject spellCastOrigin = TryToFindObject();
        if (spellCastOrigin == null)
        {
            return;
        }

        if (spellCastOrigin.transform.IsChildOf(transform))
        {
            spellCastOrigin.transform.parent = null;
            spellCastOrigin.GetComponent<Rigidbody>().isKinematic = false;
            return;
        }
        spellCastOrigin.GetComponent<Rigidbody>().isKinematic = true;
        spellCastOrigin.GetComponent<Rigidbody>().velocity = Vector3.zero;

        spellCastOrigin.transform.SetParent(transform);

    }

    private SpellObject TryToFindObject()
    {
        RaycastHit hit;
        SpellObject spellCastOrigin = null;

        if (Physics.Raycast(transform.position, transform.forward, out hit, 1000f))
        {
            Debug.DrawLine(transform.position, transform.forward * 1000f, Color.red, 3f);

            spellCastOrigin = hit.transform.GetComponent<SpellObject>();

        }


        return spellCastOrigin;
    }
}
