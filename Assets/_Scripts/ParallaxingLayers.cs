using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxingLayers : MonoBehaviour {

    private float ParallaxRatio = 0.05f;

    BackGroundElement[] backgroundElements;
    Transform cam;

	// Use this for initialization
	void Start () {

        GameObject[] backgroundElementsGameObjects;
        backgroundElementsGameObjects = GameObject.FindGameObjectsWithTag("Background");

        backgroundElements = new BackGroundElement[backgroundElementsGameObjects.Length];

        for (int i = 0; i < backgroundElements.Length; i++)
        {
            backgroundElements[i].gameObject = backgroundElementsGameObjects[i];
            backgroundElements[i].defaultXPosition = backgroundElementsGameObjects[i].transform.position.x;
        }

        cam = FindObjectOfType<Camera>().transform;

    }
	
	// Update is called once per frame
	void Update () {

        float cameraXPos = cam.position.x;

        for (int i = 0; i < backgroundElements.Length; i++)
        {
            Transform thisElementTransform = backgroundElements[i].gameObject.transform;
            float newXPos = backgroundElements[i].defaultXPosition + cameraXPos * ParallaxRatio * thisElementTransform.position.z;   
            
            //looks a bit jittery. Maybe should be using 'move towards' instead         
            thisElementTransform.position = new Vector3(newXPos, thisElementTransform.position.y, thisElementTransform.position.z);
        }

    }
}

public struct BackGroundElement
{
    public GameObject gameObject;
    public float defaultXPosition;
    
}

