using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColor : MonoBehaviour {

    public Color planeColour = Color.green;
    public Renderer rend;

	// Use this for initialization
	void Start ()
    {
        rend = GetComponent<Renderer>();
        rend.material.color = planeColour;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetMouseButtonDown(0))
        {
            planeColour = Color.blue;
            rend.material.color = planeColour;
        }
	}
}
