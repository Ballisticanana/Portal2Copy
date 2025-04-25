


// Code by Logan Seifert
using UnityEngine;

public class CameraTransformScript : MonoBehaviour
{
    //Players POV position
    public Transform playerCameraTransform;

    //The center of the portal connected to this camera
    public Transform portalTransform;

    //The center of the other portal connected to this camera
    public Transform otherPortalTransform;

    //Refrence to the portals scripts
    public PortalScripts portalScripts;

    private void Start()
    {
        portalTransform.gameObject.GetComponent<PortalScripts>();//Finds correct Portal script
    }

    void LateUpdate()
    {
        //Runs SetNearClipPlane function applying the the corret clipping Plane to the camera 
        portalScripts.SetNearClipPlane();

        //Takes the vector from the other portal to the player
        var vectorRelation = playerCameraTransform.position - otherPortalTransform.position;

        //Draws vectorRelation for visual help
        Debug.DrawLine(otherPortalTransform.position, otherPortalTransform.position + vectorRelation);

        //Multiply vectorRelation by the rotation of 2 portals y rotation
        vectorRelation = Quaternion.AngleAxis(-otherPortalTransform.eulerAngles.y + portalTransform.eulerAngles.y, Vector3.up) * vectorRelation;

        //Lines for visual help
        Debug.DrawLine(portalTransform.position, portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z));

        //moves the cameras transform to the local position of its portal with the added distance of the x & z inverted vectorRelation
        transform.position = portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z);

        //sets the cameras rotaion to the players and then angles the camera with the 2 portals diffrence. afterwards this in inverted around the y axie.
        transform.eulerAngles = playerCameraTransform.eulerAngles + (portalTransform.eulerAngles - otherPortalTransform.eulerAngles) + new Vector3(0, 180, 0);
    }
}








