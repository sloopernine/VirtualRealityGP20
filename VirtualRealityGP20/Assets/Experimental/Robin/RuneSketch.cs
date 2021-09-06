using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneSketch : MonoBehaviour
{
    public void AddPointData(List<Vector3> pointData)
    {
        
        ProcessPointData();
    }

    public SpellType ProcessPointData()
    {
        
        
        return SpellType.Fireball;
    }
}
