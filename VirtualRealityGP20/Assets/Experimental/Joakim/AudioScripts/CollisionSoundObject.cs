using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSoundObject : MonoBehaviour
{

    [SerializeField] CollisionSoundMaterial material;

    private void OnCollisionEnter(Collision collision)
    {
        if (material == CollisionSoundMaterial.none)
        {
            return;
        }
        CollisionSoundController.instance.Play(material);
    }
}
