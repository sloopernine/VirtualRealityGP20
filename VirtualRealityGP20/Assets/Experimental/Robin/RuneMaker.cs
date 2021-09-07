using System.Collections.Generic;
using UnityEngine;
using PDollarGestureRecognizer;
using System.IO;
using Valve.VR;

public class RuneMaker : MonoBehaviour
{
	private LineRenderer leftLineRenderer;
	private LineRenderer rightLineRenderer;

	//So, setting  the  system so that it *can* take input from two hands...
	//Then possibly have this see if you only have one controller?
	//This will also be interesting for if you have one of the two buttons pressed. How would that work?
	//As long as you have either button pressed it will not check but instead continue drawing?

	public SteamVR_Input_Sources leftHandInput;
	public SteamVR_Input_Sources rightHandInput;
	public SteamVR_Action_Boolean leftGrabAction;
	public SteamVR_Action_Boolean rightGrabAction;
	public SteamVR_Action_Vector3 leftHandPosition;
	public SteamVR_Action_Vector3 rightHandPosition;

	//will this bool also need to be seperated into two?
	private bool isMoving;
	
	public GameObject debugPrefab;
	public bool trainingMode;
	public string newGestureName;
	private List<Gesture> trainingSet = new List<Gesture>();
	
	public Transform trackedTransform;
	public float newPositionThresholdDistance;
	public List<Vector3> pointCloudList = new List<Vector3>();
	
	private void Start()
	{
		rightLineRenderer = GetComponent<LineRenderer>();
		
		string[] gestureFiles = Directory.GetFiles(Application.persistentDataPath, "*.xml");
		foreach (var item in gestureFiles)
		{
			trainingSet.Add(GestureIO.ReadGestureFromFile(item));
		}
	}
	
	private void Update()
	{
		bool isPressed = leftGrabAction.GetState(leftHandInput);
		
		//if isPressed is false for left hand, checks it for the rigt hand, so only gets false if both are false.
		if(isPressed == false)
		{
			isPressed = rightGrabAction.GetState(rightHandInput);
		}

		//see here if you can remove trackedTransform and just set a reference to the position of each of the controllers.
		//Would I like to actually make a list of the two handcontrollers here? It is a list of only two, but still, might make the code easier to read.

		Vector3 leftPosition = leftHandPosition.GetAxis(leftHandInput);
		Vector3 rightPosition = rightHandPosition.GetAxis(rightHandInput);

		//Vector3 trackedPosition = trackedTransform.position;

		if (!isMoving && isPressed)
		{
			StartMovement(leftPosition);
			StartMovement(rightPosition);
		}
		else if(isMoving && !isPressed)
		{
			EndMovement(leftPosition);
			EndMovement(rightPosition);

		} else if (isMoving && isPressed)
		{
			UpdateMovement(leftPosition);
			UpdateMovement(rightPosition);
		}
	}
	
	private void StartMovement(Vector3 position)
	{
		isMoving = true;
		pointCloudList.Clear();
		//I will need to add something here that will ensure that it will check both hands, or rather, check the hand that is still active?
		//So, it will need to look at both hands, and send in the one that is still active?
		//Noo, wait, it checks that in the next step, the one that has some sort of threshold... So just send both in?
		rightLineRenderer.positionCount = 1;
		pointCloudList.Add(trackedTransform.position);
		rightLineRenderer.SetPosition(0, position);
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
				
		if (Vector3.Distance(trackedTransform.position, lastPoint) > newPositionThresholdDistance)
		{
			pointCloudList.Add(trackedTransform.position);
			rightLineRenderer.positionCount = pointCloudList.Count;
			rightLineRenderer.SetPosition(pointCloudList.Count - 1, position);

			rightLineRenderer.positionCount = pointCloudList.Count;
			rightLineRenderer.SetPosition(pointCloudList.Count - 1, position);
		}
		else
		{
			rightLineRenderer.positionCount = pointCloudList.Count;
			rightLineRenderer.SetPosition(pointCloudList.Count - 1, position);
		}
	}
}