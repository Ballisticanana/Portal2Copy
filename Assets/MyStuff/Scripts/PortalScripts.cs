using System.Collections;
using StarterAssets;
using UnityEngine;

public class PortalScripts : MonoBehaviour
{
    public float nearClipOffset = 0.05f;
    public float nearClipLimit = 0.2f;

    public Camera playerCam;
    public Camera portalCam;
    public Transform otherPortal;

    public Transform player;
    public ThirdPersonController playerController;

    public bool canTpPlayer1 = true;

    
    public void Start()
    {
        playerController = player.GetComponentInParent<ThirdPersonController>();
        /*GameObject.Find("Red Player1").GetComponent<ThirdPersonController>();
        GameObject.Find("Blue Player1").GetComponent<ThirdPersonController>();*/
    }
    public void SetNearClipPlane()
    {
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
        Vector3 toOther = Vector3.Normalize(player.position - transform.position);
        //Debug.Log(otherPortal.eulerAngles - transform.eulerAngles);
        if (Vector3.Dot(forward, toOther) > 0 && Vector3.Distance(player.position,transform.position) < 3 && playerController.playerCanTeleport == true)
        {
            playerController.playerCanTeleport = false;
            playerController.enabled = false;
            StartCoroutine("PortalTravelCooldown");
            print("The player transform is behind me!");
        }
    }
    IEnumerator PortalTravelCooldown()
    {
        player.transform.position += (otherPortal.transform.position - transform.position);
        playerController.CameraAngleOverrideY += (otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180;
        Debug.Log((otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180);
        yield return new WaitForSeconds(0.05f);
        playerController.enabled = true;
        yield return new WaitForSeconds(1);
        playerController.playerCanTeleport = true;
    }
}
