using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using PDollarGestureRecognizer;

public class RuneMaker : MonoBehaviour
{
    private LineRenderer lineRenderer;
    
    public XRNode inputSource;
    public InputHelpers.Button inputButton;
    public float inputThreshold = 0.1f;

    private bool isMoving;

    public GameObject debugPrefab;
    
    public Transform pointSource;
    public float newPositionThresholdDistance = 0.1f;
    public List<Vector3> pointCloudList = new List<Vector3>();

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        InputHelpers.IsPressed(InputDevices.GetDeviceAtXRNode(inputSource), inputButton, out bool isPressed, inputThreshold);

        Vector3 newPosition = pointSource.position;
        
        if (!isMoving && isPressed)
        {
            StartMovement(newPosition);
        }
        else if(isMoving && !isPressed)
        {
            EndMovement(newPosition);
        }
        else if (isMoving && isPressed)
        {
            UpdateMovement(newPosition);
        }
    }

    private void StartMovement(Vector3 position)
    {
        isMoving = true;
        pointCloudList.Clear();
        lineRenderer.positionCount = 1;
        pointCloudList.Add(pointSource.position);
        lineRenderer.SetPosition(0, position);
        CreateDebugCube(position);
    }

    private void EndMovement(Vector3 position)
    {
        isMoving = false;
    }

    private void UpdateMovement(Vector3 position)
    {
        Vector3 lastPoint = pointCloudList[pointCloudList.Count - 1];

        //Debug.Log("Updating movement: " + Vector3.Distance(pointSource.position, lastPoint));
        
        if (Vector3.Distance(pointSource.position, lastPoint) > newPositionThresholdDistance)
        {
            pointCloudList.Add(pointSource.position);
            lineRenderer.positionCount = pointCloudList.Count;
            lineRenderer.SetPosition(pointCloudList.Count - 1, position);
            CreateDebugCube(position);
        }
        else
        {
            lineRenderer.SetPosition(pointCloudList.Count - 1, position);
        }
    }

    private void CreateDebugCube(Vector3 position)
    {
        return;
        
        if (debugPrefab)
        {
            Destroy(Instantiate(debugPrefab, pointSource.position, quaternion.identity), 3);
        }
    }
}
