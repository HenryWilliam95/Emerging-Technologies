using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AirshipBehaviour : MonoBehaviour {

    public bool takeOff = false;
    public bool shouldSpin = false;
    bool visibleLastFrame;  // Set a bool to tell Unity3D if this object has been tracked before

    public float speed = 4;

    private Rigidbody rb;
    //private Quaternion a;

    public Animator airship;                    // Set up the animator for the Airship
    public ImageTargetBehaviour ImgBehaviour;   // Allow Vuforia to track this object correctly

    public GameObject R34;

    public TextBoxManager textBoxManager;       // Reference to the textbox to update if it should be visible or not 

    


	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        //a = GetComponent<Transform>().rotation;
    }
	
	// Update is called once per frame
	void Update ()
    {

        transform.Rotate(0, 1, 0);

        //Vector3 move = new Vector3(1, 0, 0);

        //rb.AddForce(move * speed);

        // Loop through all the currently trackable objects
        foreach (TrackableBehaviour track in TrackerManager.Instance.GetStateManager().GetTrackableBehaviours())
        {
            // If this image is currently being tracked
            if (track == ImgBehaviour)
            {
                if (track.CurrentStatus == TrackableBehaviour.Status.TRACKED || track.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
                {
                    // Tell the console this object was found by the AR camera
                    print("Tracked");
                    if (visibleLastFrame == false)
                    {
                        // Start the take off animation and set the visible bool to true
                        print("Found image");
                        TakeOff();
                        visibleLastFrame = true;

                        // Activate the text box to show information about this object
                        textBoxManager.isActive = true;
                        textBoxManager.EnableTextBox();
                    }
                }
                if(track.CurrentStatus == TrackableBehaviour.Status.NOT_FOUND)
                {
                    if (visibleLastFrame == true)
                    {
                        print("Lost image");
                        visibleLastFrame = false;

                        // Disable the text box once this object has been lost and is no longer being tracked
                        textBoxManager.isActive = false;
                        textBoxManager.DisableTextBox();

                        // If Unity3D has reached the end of the file, loop back round again incase user wants to reread anything
                        if (textBoxManager.currentLine > textBoxManager.endLine)
                        {
                            textBoxManager.currentLine = 0;
                        }
                    }
                }
            }           
        }
	}

    void TakeOff()
    {
        airship.SetTrigger("Take Off");
    }
}


