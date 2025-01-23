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

    public bool player1Portal;
    public bool player2Portal;
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

    public void FixedUpdate()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 toOther = Vector3.Normalize(player.position - transform.position);
        //Debug.Log(otherPortal.eulerAngles - transform.eulerAngles);
        if (Vector3.Dot(-forward, toOther) > 0 && playerController.playerCanTeleport == true && playerController.inPortal == true)
        {
            playerController.playerCanTeleport = false;
            playerController.enabled = false;
            playerCam.enabled = false;
            StartCoroutine("PortalTravelCooldown");
        }
    }
    IEnumerator PortalTravelCooldown()
    {
        Vector3 tempMove = player.position - transform.position;

        player.position = otherPortal.position + (Quaternion.Euler(0, (otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180, 0) * tempMove);
        // + (Quaternion.Euler(0, (otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180, 0) * Vector3.forward * 2)
        playerController.CameraAngleOverrideY += (otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180;
        playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

        Debug.Log((otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180);
        
        yield return new WaitForSeconds(Time.deltaTime * 1);
        playerCam.enabled = true;
        playerController.enabled = true;
        yield return new WaitForSeconds(1);
        playerController.playerCanTeleport = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red Player1" && player1Portal == true)
        {
            Debug.Log("in portal");
            playerController.inPortal = true;
        }

        if (other.gameObject.name == "Blue Player1" && player2Portal == true)
        {
            Debug.Log("in portal");
            playerController.inPortal = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Red Player1" && player1Portal == true)
        {
            Debug.Log("in portal");
            playerController.inPortal = false;
        }

        if (other.gameObject.name == "Blue Player1" && player2Portal == true)
        {
            Debug.Log("in portal");
            playerController.inPortal = false;
        }
    }
}
