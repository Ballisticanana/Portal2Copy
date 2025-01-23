using System.Collections;
using StarterAssets;
using UnityEngine;

public class PortalScripts : MonoBehaviour
{
    public float nearClipOffset = 0.05f;
    public float nearClipLimit = 0.2f;

    public space _;

    public Camera playerCam;
    public Camera portalCam;
    public Transform otherPortal;
    public Transform player;
    public ThirdPersonController playerController;

    public space _;

    public bool player1Portal;
    public bool player2Portal;
    
    public void Start()
    {
        playerController = player.GetComponentInParent<ThirdPersonController>();
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
        //Gets the forward direction relitive to the portals direction they face.
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        //the Vector from the player too the portal
        Vector3 toOther = Vector3.Normalize(player.position - transform.position);
        //if (the player is behind the portal), (the player tp cooldown is over), and (the player is touching the portal)
        if (Vector3.Dot(-forward, toOther) > 0 && playerController.playerCanTeleport == true && playerController.inPortal == true)
        {
            //Disables the ability for the player to be able to teleport
            playerController.playerCanTeleport = false;
            playerController.enabled = false;
            playerCam.enabled = false;
            StartCoroutine("PortalTravelCooldown");
        }
    }
    IEnumerator PortalTravelCooldown()
    {
        ////NEW CODE MIGHT MESS UP STUFF IF THING BREAK RMOVE "+ transform.TransformDirection(-Vector3.forward)"
        Vector3 tempMove = (player.position - transform.position) + transform.TransformDirection(-Vector3.forward);

        player.position = otherPortal.position + (Quaternion.Euler(0, (otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180, 0) * tempMove);
        playerController.CameraAngleOverrideY += (otherPortal.eulerAngles.y - transform.eulerAngles.y) - 180;
        playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

        //waits for 1 frame
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

//NEW CODE MIGHT MESS UP STUFF IF THING BREAK RMOVE
    
    public void OnTriggerStay.(Collider other)
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
