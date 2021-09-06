using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuneColliderArea : MonoBehaviour
{
    [Header("Renderer")]
    [SerializeField] Material colliderIdle;
    [SerializeField] Material colliderActivated;
    [SerializeField] float activeTime = 3;
    private MeshRenderer meshRenderer;

    private bool isActive;
    public bool getActiveStatus { get { return isActive; } }

    private void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    public void ActiveArea()
    {
        isActive = true;
        SetMaterial(colliderActivated);
        StopAllCoroutines();
    }

    public void DeactivateArea()
    {
        isActive = false;
        SetMaterial(colliderIdle);
    }

    private void SetMaterial(Material material)
    {
        meshRenderer.material = material;     
    }

    public void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name + "Entered the trigger area");
        ActiveArea();
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log("Exit collision");
        StartCoroutine(ActivatedTimer(activeTime));
    }

    private IEnumerator ActivatedTimer(float activeTime)
    {
        Debug.Log("Started Coroutine Activated Timer");
        yield return new WaitForSeconds(activeTime);
        DeactivateArea();
    }
}
