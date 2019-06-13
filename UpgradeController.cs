using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeController : MonoBehaviour {

    static GameObject building;
    public GameObject Object;
    public Camera cam;
    static bool boolean = false;

    // Use this for initialization
    void Start ()
    {
        cam = Camera.main;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (boolean)
        {
            Vector3 screenPos = cam.WorldToScreenPoint(building.transform.position);
            Object.transform.position = screenPos;
        }
    }

    public void SetBuilding(GameObject build)
    {
        building = build;
        boolean = true;
    }
}