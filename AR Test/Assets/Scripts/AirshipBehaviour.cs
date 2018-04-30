using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Vuforia;

public class AirshipBehaviour : MonoBehaviour {

    public bool takeOff = false;
    public bool shouldSpin = false;
    bool visibleLastFrame;

    public float speed = 4;

    private Rigidbody rb;
    //private Quaternion a;

    public Animator airship;
    public ImageTargetBehaviour ImgBehaviour;

    public GameObject R34;

    public TextBoxManager textBoxManager;

    


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

        foreach (TrackableBehaviour track in TrackerManager.Instance.GetStateManager().GetTrackableBehaviours())
        {
            if (track == ImgBehaviour)
            {
                if (track.CurrentStatus == TrackableBehaviour.Status.TRACKED || track.CurrentStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
                {
                    print("Tracked");
                    if (visibleLastFrame == false)
                    {
                        print("Found image");
                        TakeOff();
                        visibleLastFrame = true;

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

                        textBoxManager.isActive = false;
                        textBoxManager.DisableTextBox();

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


