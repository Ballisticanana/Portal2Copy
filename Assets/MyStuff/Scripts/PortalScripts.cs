using System.Collections;
using StarterAssets;
using UnityEngine;

public class PortalScripts : MonoBehaviour
{
    public float nearClipOffset = 0.05f;
    public float nearClipLimit = 0.2f;

    public Camera playerCam;
    public Camera portalCam;

    public Transform player1;
    public Transform wholePlayer1Trans;
    public ThirdPersonController player1Controller;

    public Transform player2;
    public Transform wholePlayer2Trans;
    public ThirdPersonController player2Controller;

    public bool canTpPlayer1 = true;

    public Transform otherPortal;
    public void Start()
    {
        player1Controller = player1.GetComponentInParent<ThirdPersonController>();
        player2Controller = player2.GetComponentInParent<ThirdPersonController>();
        /*GameObject.Find("Red Player1").GetComponent<ThirdPersonController>();
        GameObject.Find("Blue Player1").GetComponent<ThirdPersonController>();*/
    }
    public void SetNearClipPlane()
    {
        // Learning resource:
        // http://www.terathon.com/lengyel/Lengyel-Oblique.pdf
        Transform clipPlane = transform;
        int dot = System.Math.Sign(Vector3.Dot(clipPlane.forward, transform.position - portalCam.transform.position));

        Vector3 camSpacePos = portalCam.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = portalCam.worldToCameraMatrix.MultiplyVector(clipPlane.forward) * dot;
        float camSpaceDst = -Vector3.Dot(camSpacePos, camSpaceNormal) + nearClipOffset;

        // Don't use oblique clip plane if very close to portal as it seems this can cause some visual artifacts
        if (Mathf.Abs(camSpaceDst) > nearClipLimit)
        {
            Vector4 clipPlaneCameraSpace = new Vector4(camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);

            // Update projection based on new clip plane
            // Calculate matrix with player cam so that player camera settings (fov, etc) are used
            portalCam.projectionMatrix = playerCam.CalculateObliqueMatrix(clipPlaneCameraSpace);
        }
        else
        {
            portalCam.projectionMatrix = playerCam.projectionMatrix;
        }
    }
    public void Update()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = Vector3.Normalize(player1.position - transform.position);
        //Debug.Log(Vector3.Distance(player1.position, transform.position) < 0.7);
        Debug.Log(otherPortal.eulerAngles - transform.eulerAngles);
        if (Vector3.Dot(forward, toOther) > 0 & Vector3.Distance(player1.position,transform.position) < 0.7 & canTpPlayer1 == true)
        {
            StartCoroutine("PortalTravelCooldown");
            //print("The player transform is behind me!");
        }
    }
    IEnumerator PortalTravelCooldown()
    {
        canTpPlayer1 = false;
        player1Controller.enabled = false;
        player1Controller.currentHorizontalDirection = wholePlayer1Trans.eulerAngles + (otherPortal.eulerAngles - transform.eulerAngles) - new Vector3(0, -180, 0);
        Debug.Log(player1Controller.enabled);
        wholePlayer1Trans.eulerAngles = wholePlayer1Trans.eulerAngles + (otherPortal.eulerAngles - transform.eulerAngles) - new Vector3(0, -180, 0);
        wholePlayer1Trans.transform.position += (otherPortal.transform.position - transform.position);
        player1Controller.enabled = true;
        Debug.Log(player1Controller.enabled);
        yield return new WaitForSeconds(1);
        canTpPlayer1 = true;
    }
}
