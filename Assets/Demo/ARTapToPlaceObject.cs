using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.XR.ARFoundation;
using UnityEngine.Experimental.XR;

public class NewBehaviourScript : MonoBehaviour
{
    private GameOject placementIndicator;

    //creating a reference to that session origin object so I'll create a
    // variable of a our session origin and call it a our origin
    private ARSessionOrigin arOrigin;
    // we can place a virtual object in order to represent that position in space
    // call it placement pose a pose object is a simple data structure 
    // to describe the position and rotation of a 3D point
    private Pose placementPose;

    private bool placementPoseIsValid = false;

    // private GameOject PlacementIndicator { get => placementIndicator; set => placementIndicator = value; }

    // Start is called before the first frame update
    void Start()
    {
        arOrigin = FindObjectOfType<ARSessionOrigin>();
        
    }

    // Update is called once per frame
    void Update()
    {
        //call a new method
        UpdatePlacementPose();
        UpdatePlacementIndicator();
        
    }

    private void UpdatePlacementIndicator()
    {
        if (placementPoseIsValid)
        {
            PlacementIndicator.setActive(true);
            PlacementIndicator.transform.SetPositionAndRotation(placementPose.position, placementPose.rotation);
        }
        else {
            PlacementIndicator.setActive(false);
        }
    }
    private void UpdatePlacementPose()
    {
        // throw new NotImplementedException();
        var screenCenter = Camera.current.ViewporToScreenPoint(new Vector3(0.5f, 0.5f));

        var hits = new List<ARRaycastHit>();
        arOrigin.Raycast(screenCenter, hits, TrackableType.Planes);

        placementPoseIsValid = hits.Count > 0; 
        if (placementPoseIsValid)
        {
            placementPose = hits[0].pose;
        }
    }


}
