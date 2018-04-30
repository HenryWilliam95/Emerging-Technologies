using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using Vuforia;
using UnityEngine.UI;

public class ShipController : MonoBehaviour {

    private Rigidbody rb;

    public Animator airship;                    // Set up the animator for the Airship
    public ImageTargetBehaviour ImgBehaviour;   // Allow Vuforia to track this object correctly

    public TextBoxManager textBoxManager;



    public GameObject joystick;

    // Pull the result of the joystick between 1 and -1
    float x = CrossPlatformInputManager.GetAxis("Horizontal");
    float y = CrossPlatformInputManager.GetAxis("Vertical");



    bool visibleLastFrame = false;

    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody>();
        joystick.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {

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

                        joystick.SetActive(true);
                    }
                }
                if (track.CurrentStatus == TrackableBehaviour.Status.NOT_FOUND)
                {
                    if (visibleLastFrame == true)
                    {
                        print("Lost image");
                        visibleLastFrame = false;

                        // Disable the text box once this object has been lost and is no longer being tracked
                        textBoxManager.isActive = false;
                        textBoxManager.DisableTextBox();

                        joystick.SetActive(false);

                        // If Unity3D has reached the end of the file, loop back round again incase user wants to reread anything
                        if (textBoxManager.currentLine > textBoxManager.endLine)
                        {
                            textBoxManager.currentLine = 0;
                        }
                    }
                }
            }
        }

        

        Vector3 movement = new Vector3(x, 0, y);

        // Select how fast the airship will move around the scene
        rb.velocity = movement * 4;

        // Set the rotation of the airship
        transform.eulerAngles = new Vector3(transform.eulerAngles.x, Mathf.Atan2(x, y) * Mathf.Rad2Deg, transform.eulerAngles.z);
	}

    void TakeOff()
    {
        airship.SetTrigger("Take Off");
    }
}
