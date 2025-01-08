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
        portalScripts.SetNearClipPlane();
        var vectorRelation = playerCameraTransform.position - otherPortalTransform.position;
        Debug.DrawLine(otherPortalTransform.position, otherPortalTransform.position + vectorRelation);
        vectorRelation = Quaternion.AngleAxis(-otherPortalTransform.eulerAngles.y + portalTransform.eulerAngles.y, Vector3.up) * vectorRelation;
        Debug.DrawLine(portalTransform.position, portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z));
        transform.position = portalTransform.localPosition + new Vector3(-vectorRelation.x, vectorRelation.y, -vectorRelation.z);
        transform.eulerAngles = playerCameraTransform.eulerAngles + (portalTransform.eulerAngles - otherPortalTransform.eulerAngles) + new Vector3(0, 180, 0);
    }
}
