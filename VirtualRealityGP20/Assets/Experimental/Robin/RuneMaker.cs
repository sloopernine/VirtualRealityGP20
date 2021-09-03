using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;

public class RuneMaker : MonoBehaviour
{
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;

    private bool isMoving;

    public GameObject debugPrefab;
    
    public Transform pointSource;
    public float newPositionThresholdDistance = 0.1f;
    public List<Vector3> pointCloudList = new List<Vector3>();
    
    private void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);
        
        if (!isMoving && isPressed)
        {
            StartMovement();
        }
        else if(isMoving && !isPressed)
        {
            EndMovement();
        }
        else if (isMoving && isPressed)
        {
            UpdateMovement();
        }
    }

    private void StartMovement()
    {
        isMoving = true;
        pointCloudList.Clear();
        pointCloudList.Add(pointSource.position);
        CreateDebugCube(pointSource.position);
        //Debug.Log("Start movement");
    }

    private void EndMovement()
    {
        isMoving = false;
        
        //Debug.Log("End movement");
    }

    private void UpdateMovement()
    {

        Vector3 lastPoint = pointCloudList[pointCloudList.Count - 1];

        Debug.Log("Updating movement: " + Vector3.Distance(pointSource.position, lastPoint));
        
        if (Vector3.Distance(pointSource.position, lastPoint) > newPositionThresholdDistance)
        {
            pointCloudList.Add(pointSource.position);
            CreateDebugCube(pointSource.position);
            Debug.Log("Create Cube");
        }
    }

    private void CreateDebugCube(Vector3 position)
    {
        if (debugPrefab)
        {
            Destroy(Instantiate(debugPrefab, pointSource.position, quaternion.identity), 3);
        }
    }
}
