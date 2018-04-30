using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class LoadNewTextFile : MonoBehaviour {

    public TextAsset text;

    public int startLine, endLine;

    public TextBoxManager textBoxManager;
    public DefaultTrackableEventHandler eventHandler;

    // Use this for initialization
    void Start () {
        textBoxManager = FindObjectOfType<TextBoxManager>();
        eventHandler = FindObjectOfType<DefaultTrackableEventHandler>();
	}
	
	// Update is called once per frame
	void Update () {
        TrackableFinder();

    }

    void TrackableFinder()
    {

    }
}
