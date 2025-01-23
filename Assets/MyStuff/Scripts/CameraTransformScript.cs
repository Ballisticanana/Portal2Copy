using UnityEngine;

public class CameraTransformScript : MonoBehaviour
{
    public float nearClipOffset = 0.05f;
    public float nearClipLimit = 0.2f;

    public Transform playerCameraTransform;
    public Camera playerCam;

    public Camera portalCam;
    public Transform portalTransform;
    public Transform otherPortalTransform;

    public PortalScripts portalScripts;
    private void Start()
    {
        portalTransform.gameObject.GetComponent<PortalScripts>();
    }
    void LateUpdate()
    {
        //Runs SetNearClipPlane function applying the the corret clipping Plane  
        portalScripts.SetNearClipPlane();
        //Takes the vector from the other portal to the player
        var vectorRelation = playerCameraTransform.position - otherPortalTransform.position;
        //Lines for visual help
        Debug.DrawLine(otherPortalTransform.position, otherPortalTransform.position + vectorRelation);
        //Multiply the vectorRelation and rotate the vector by the y rotation of both portals
        vectorRelation = Quaternion.AngleAxis(-otherPortalTransform.eulerAngles.y + portalTransform.eulerAngles.y, Vector3.up) * vectorRelation;
        //Lines for visual help
        Debug.DrawLine(portalTransform.position, portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z));
        //moves the cameras transform to the local position of its portal with the added distance of the x & z inverted vectorRelation
        transform.position = portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z);
        //sets the cameras rotaion to the players and then angles the camera with the 2 portals diffrence. afterwards this in inverted around the y axie.
        transform.eulerAngles = playerCameraTransform.eulerAngles + (portalTransform.eulerAngles - otherPortalTransform.eulerAngles) + new Vector3(0, 180, 0);
    }
}
