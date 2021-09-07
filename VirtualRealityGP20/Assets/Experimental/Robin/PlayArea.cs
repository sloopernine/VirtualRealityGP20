using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class PlayArea : MonoBehaviour
{
    public SteamVR_PlayArea playArea;
    public GameObject player;
    public Camera camera;
    public CVRChaperone chaperone;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        playArea = player.GetComponent<SteamVR_PlayArea>();
        camera = Camera.main;
        chaperone = OpenVR.Chaperone;
    }

    private void Update()
    {
        float x = 0;
        float y = 0;
        
        chaperone.GetPlayAreaSize(ref x, ref y);
        
        Debug.Log("" + camera.transform.position);
        Debug.Log("x: " + x + " y: " + y);
    }
}
