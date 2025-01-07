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
    void LateUpdate()
    {
        SetNearClipPlane();
        var vectorRelation = playerCameraTransform.position - otherPortalTransform.position;
        Debug.DrawLine(otherPortalTransform.position, otherPortalTransform.position + vectorRelation);
        vectorRelation = Quaternion.AngleAxis(-otherPortalTransform.eulerAngles.y + portalTransform.eulerAngles.y, Vector3.up) * vectorRelation;
        Debug.DrawLine(portalTransform.position, portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z));
        transform.position = portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z);
        transform.eulerAngles = playerCameraTransform.eulerAngles + (portalTransform.eulerAngles - otherPortalTransform.eulerAngles) + new Vector3(0, 180, 0);
    }

    void SetNearClipPlane()
    {
        // Learning resource:
        // http://www.terathon.com/lengyel/Lengyel-Oblique.pdf
        Transform clipPlane = otherPortalTransform;
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

    /*void SetNearClipPlane()
    {
        Transform clipPlane = transform;
        int dot = System.Math.Sign(Vector3.Dot (clipPlane.forward, portalTransform.position - transform.position));

        Vector3 camSpacePos = portalCam.worldToCameraMatrix.MultiplyPoint(clipPlane.position);
        Vector3 camSpaceNormal = portalCam.worldToCameraMatrix.MultiplyVector (clipPlane.forward) * dot;
        float camSpaceDst = -Vector3.Dot (camSpacePos,camSpaceNormal);

        Vector4 clipPlaneCameraSpace = new Vector4 (camSpaceNormal.x, camSpaceNormal.y, camSpaceNormal.z, camSpaceDst);

        portalCam.projectionMatrix = playerCam.CalculateObliqueMatrix (clipPlaneCameraSpace);
    }*/
}
