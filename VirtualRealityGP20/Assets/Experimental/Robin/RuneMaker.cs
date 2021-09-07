using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;
using Valve.VR;

public class RuneMaker : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public SteamVR_Input_Sources handType;
    public SteamVR_Action_Boolean grabAction;

    private bool isMoving;
    
    public GameObject debugPrefab;
    public bool trainingMode;
    public string newGestureName;
    private List<Gesture> trainingSet = new List<Gesture>();
    
    public Transform trackedTransform;
    public float newPositionThresholdDistance = 0.1f;
    public List<Vector3> pointCloudList = new List<Vector3>();
    
    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        
        string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
        foreach (var item in gestureFiles)
        {
            trainingSet.Add(GestureIO.ReadGestureFromFile(item));
        }
    }
    
    private void Update()
    {
        bool isPressed = grabAction.GetState(handType);
        
        Vector3 trackedPosition = trackedTransform.position;
        
        if (!isMoving && isPressed)
        {
            StartMovement(trackedPosition);
        }
        else if(isMoving && !isPressed)
        {
            EndMovement(trackedPosition);
        }
        else if (isMoving && isPressed)
        {
            UpdateMovement(trackedPosition);
        }
    }
    
    private void StartMovement(Vector3 position)
    {
        isMoving = true;
        pointCloudList.Clear();
        lineRenderer.positionCount = 1;
        pointCloudList.Add(trackedTransform.position);
        lineRenderer.SetPosition(0, position);
        CreateDebugCube(position);
    }
    
    private void EndMovement(Vector3 position)
    {
        isMoving = false;
    
        Point[] pointArray = new Point[pointCloudList.Count];
        
        for (int i = 0; i < pointArray.Length; i++)
        {
            Vector2 screenPoint = Camera.main.WorldToScreenPoint(pointCloudList[i]);
            pointArray[i] = new Point(screenPoint.x, screenPoint.y, 0);
        }

        Gesture newGesture = new Gesture(pointArray);
        
        if (trainingMode)
        {
            newGesture.Name = newGestureName;
            trainingSet.Add(newGesture);
            
            string path = Application.persistentDataPath + "/" + newGestureName + ".xml";
            GestureIO.WriteGesture(pointArray, newGestureName, path);
        }
        else
        {
            Result result = PointCloudRecognizer.Classify(newGesture, trainingSet.ToArray());
            Debug.Log(result.GestureClass + " " + result.Score);
        }
    }
    
    private void UpdateMovement(Vector3 position)
    {
        Vector3 lastPoint = pointCloudList[pointCloudList.Count - 1];
        
        //Debug.Log("Updating movement: " + Vector3.Distance(pointSource.position, lastPoint));
        
        if (Vector3.Distance(trackedTransform.position, lastPoint) > newPositionThresholdDistance)
        {
            pointCloudList.Add(trackedTransform.position);
            lineRenderer.positionCount = pointCloudList.Count;
            lineRenderer.SetPosition(pointCloudList.Count - 1, position);
            CreateDebugCube(position);
        }
        else
        {
            lineRenderer.positionCount = pointCloudList.Count;
            lineRenderer.SetPosition(pointCloudList.Count - 1, position);
        }
    }
    
    private void CreateDebugCube(Vector3 position)
    {

    }
}
