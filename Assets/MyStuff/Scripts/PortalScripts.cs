using System.Collections;
using StarterAssets;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalScripts : MonoBehaviour
{
    public float nearClipOffset = 0.05f;
    public float nearClipLimit = 0.2f;

    public float addedTpDist;

    //public space _;

    public Camera playerCam;
    public Camera portalCam;
    public Transform otherPortal;
    public Transform player;
    public ThirdPersonController playerController;

    public Transform redPortal;
    public Transform bluePortal;

    public string portalColor;

    private bool player1InRedPortal;
    private bool player1InBluePortal = false;
    private bool player2InRedPortal;
    private bool player2InBluePortal;

    //public space _;

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

        if (Vector3.Dot(forward, toOther) > 0)
        {
            print("Behind Portal");
        }


        if (Vector3.Dot(forward, toOther) > 0 && player1InBluePortal == true && playerController.playerCanTeleport == true)
        {
            playerCam.enabled = false;
            playerController.enabled = false;
            playerController.playerCanTeleport = false;
            player1InBluePortal = false;

            playerController.CameraAngleOverrideY += (redPortal.eulerAngles.y - bluePortal.eulerAngles.y) - 180;
            playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

            Vector3 tempMove = (player.position - bluePortal.position);
            player.position = redPortal.position;
            player.localPosition += tempMove;

            StartCoroutine("PortalTravelCooldown");
        }
        else if (Vector3.Dot(forward, toOther) > 0 && player1InRedPortal == true && playerController.playerCanTeleport == true)
        {
            playerCam.enabled = false;
            playerController.enabled = false;
            playerController.playerCanTeleport = false;
            player1InRedPortal = false;

            playerController.CameraAngleOverrideY += (bluePortal.eulerAngles.y - redPortal.eulerAngles.y) - 180;
            playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

            Vector3 tempMove = (player.position - redPortal.position);
            player.position = bluePortal.position;
            player.position += (Quaternion.Euler(0, (bluePortal.eulerAngles.y - redPortal.eulerAngles.y) - 180, 0) * tempMove);

            StartCoroutine("PortalTravelCooldown");
        }
        else if(Vector3.Dot(forward, toOther) > 0 && player2InBluePortal == true && playerController.playerCanTeleport == true)
        {
            playerCam.enabled = false;
            playerController.enabled = false;
            playerController.playerCanTeleport = false;
            player2InBluePortal = false;

            playerController.CameraAngleOverrideY += (redPortal.eulerAngles.y - bluePortal.eulerAngles.y) - 180;
            playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

            Vector3 tempMove = (player.position - bluePortal.position);
            player.position = redPortal.position;
            player.position += (Quaternion.Euler(0, (redPortal.eulerAngles.y - bluePortal.eulerAngles.y) - 180, 0) * tempMove);

            StartCoroutine("PortalTravelCooldown");
        }
        else if(Vector3.Dot(forward, toOther) > 0 && player2InRedPortal == true && playerController.playerCanTeleport == true)
        {
            playerCam.enabled = false;
            playerController.enabled = false;
            playerController.playerCanTeleport = false;
            player2InRedPortal = false;

            playerController.CameraAngleOverrideY += (redPortal.eulerAngles.y - redPortal.eulerAngles.y) - 180;
            playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

            Vector3 tempMove = (player.position - bluePortal.position);
            player.position = bluePortal.position;
            player.position += (Quaternion.Euler(0, (bluePortal.eulerAngles.y - redPortal.eulerAngles.y) - 180, 0) * tempMove);

            StartCoroutine("PortalTravelCooldown");
        }


        Debug.Log(player1InBluePortal + " " + playerController.playerCanTeleport);
    }
    IEnumerator PortalTravelCooldown()
    {
        

        

        //Debug.Log("This Shit is running");

        //waits for 1 frame
        yield return new WaitForSeconds(Time.deltaTime * 3);
        playerCam.enabled = true;
        playerController.enabled = true;
        yield return new WaitForSeconds(1);
        playerController.playerCanTeleport = true;
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red Player1" && portalColor == "Red" && player1Portal == true)
        {
            Debug.Log("in red portal");
            player1InRedPortal = true;
            Physics.IgnoreLayerCollision(6, 9);
        }
        if (other.gameObject.name == "Red Player1" && portalColor == "Blue" && player1Portal == true)
        {
            Debug.Log("in blue portal");
            player1InBluePortal = true;
            Physics.IgnoreLayerCollision(6, 9);
        }
        if (other.gameObject.name == "Blue Player2" && portalColor == "Red" && player2Portal == true)
        {
            Debug.Log("in red portal");
            player1InRedPortal = true;
            Physics.IgnoreLayerCollision(7, 9);
        }
        if (other.gameObject.name == "Blue Player2" && portalColor == "Blue" && player2Portal == true)
        {
            Debug.Log("in blue portal");
            player1InBluePortal = true;
            Physics.IgnoreLayerCollision(7, 9);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Red Player1")
        {
            player1InBluePortal = false;
            player1InRedPortal = false;
            player2InBluePortal = false;
            player2InRedPortal = false;
            Physics.IgnoreLayerCollision(6, 9, false);
        }

        if (other.gameObject.name == "Blue Player2")
        {
            player1InBluePortal = false;
            player1InRedPortal = false;
            player2InBluePortal = false;
            player2InRedPortal = false;
            Physics.IgnoreLayerCollision(7, 9, false);
        }
    }
}
//if(playerController.playerCanTeleport == true)
//{
//    if (Vector3.Dot(forward, toOther) > 0 && playerController.playerCanTeleport == true && playerController.inPortal == true && portalColor == "Blue")
//    {
//        ////Disables the ability for the player to be able to teleport
//        //playerController.playerCanTeleport = false;
//        //playerController.enabled = false;
//        //playerCam.enabled = false;

//        //playerController.CameraAngleOverrideY += (redPortal.eulerAngles.y - bluePortal.eulerAngles.y) - 180;
//        //playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

//        //Vector3 blueTempMove = (player.position - bluePortal.position);
//        //player.position = redPortal.position;
//        //player.position += (Quaternion.Euler(0, (redPortal.eulerAngles.y - bluePortal.eulerAngles.y) - 180, 0) * blueTempMove);

//        //StartCoroutine("PortalTravelCooldown");
//        //Debug.Log("teleporting player to Red");
//    }
//    else if (Vector3.Dot(forward, toOther) > 0 && playerController.playerCanTeleport == true && playerController.inPortal == true && portalColor == "Red")
//    {
//        ////Disables the ability for the player to be able to teleport
//        //playerController.playerCanTeleport = false;
//        //playerController.enabled = false;
//        //playerCam.enabled = false;

//        //playerController.CameraAngleOverrideY += (bluePortal.eulerAngles.y - redPortal.eulerAngles.y) - 180;
//        //playerController.CinemachineCameraTarget.transform.rotation = Quaternion.Euler(playerController._cinemachineTargetPitch + playerController.CameraAngleOverride, playerController._cinemachineTargetYaw + playerController.CameraAngleOverrideY, 0.0f);

//        //Vector3 redTempMove = (player.position - redPortal.position);
//        //player.position = bluePortal.position;
//        //player.position -= bluePortal.transform.forward * 2;
//        ////player.position += (Quaternion.Euler(0, (bluePortal.eulerAngles.y - redPortal.eulerAngles.y) - 180, 0) * redTempMove);

//        //StartCoroutine("PortalTravelCooldown");
//        //Debug.Log("teleporting player to Blue");
//    }
//}

//public void OnTriggerEnter(Collider other)
//{
//    if (other.gameObject.name == "Red Player1" && player1Portal == true)
//    {
//        Debug.Log("in portal");
//        playerController.inPortal = true;
//    }

//    if (other.gameObject.name == "Blue Player2" && player2Portal == true)
//    {
//        Debug.Log("in portal");
//        playerController.inPortal = true;
//    }
//}

////NEW CODE MIGHT MESS UP STUFF IF THING BREAK RMOVE
//public void OnTriggerStay(Collider other)
//{
//    if (other.gameObject.name == "Red Player1" && player1Portal == true)
//    {
//        Debug.Log("in portal");
//        playerController.inPortal = true;
//    }

//    if (other.gameObject.name == "Blue Player2" && player2Portal == true)
//    {
//        Debug.Log("in portal");
//        playerController.inPortal = true;
//    }
//}
//public void OnTriggerExit(Collider other)
//{
//    if (other.gameObject.name == "Red Player1" && player1Portal == true)
//    {
//        Debug.Log("in portal");
//        playerController.inPortal = false;
//    }

//    if (other.gameObject.name == "Blue Player2" && player2Portal == true)
//    {
//        Debug.Log("in portal");
//        playerController.inPortal = false;
//    }
//}
