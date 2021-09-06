using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneColliderArray : MonoBehaviour
{
    public RuneColliderArea[] runeColliderAreas;


    public void ActivateRune()
    {
        //Call something cool and create a rune object.
    }

    public void DeactivateRune()
    {
        foreach (RuneColliderArea area in runeColliderAreas)
        {
            area.DeactivateArea();
        }
    }

    public bool AllAreasActivated()
    {


        return true;
    }
}
