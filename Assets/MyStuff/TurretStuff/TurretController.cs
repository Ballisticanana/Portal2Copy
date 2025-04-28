using System.Collections;
using StarterAssets;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Windows;
using VolumetricLightsDemo;

public class TurretController : MonoBehaviour
{
    public bool canTargetRed;
    public bool canTargetBlue;
    public bool freezeMovement = false;
    public bool lockedAngle = false;
    public bool powerOff = false;
    public float rotateAngle;
    public float rotateSpeed;
    public float upDownAngle;
    public float leftrightAngle;

    public Transform TurretHeadBone;
    public Light volumetricSpotLight;
    public GameObject zapPartical;
    public GameObject hitParticle;
    public MeshCollider triggerZone;

    public GameObject glowMaterial;
    public GameObject colorRing;

    public Material cameraGlowRed;
    public Material colorRed;

    public Material cameraGlowBlue;
    public Material colorBlue;

    public Material cameraGlowPurple;
    public Material colorPurple;

    public Material cameraGlowWhite;
    public Material colorWhite;

    //private bool hasAAngle = false;
    private float relitiveTime = 0;
    private bool powerCheck = false;

    public GameObject redPlayerPos;
    public GameObject bluePlayerPos;
    public GameObject rayPoint;
    public LayerMask redTurretMask;
    public LayerMask blueTurretMask;
    public bool redInZone;
    public bool blueInZone;
    public bool killing;
    private bool killCheck;
    private Transform Spawn_Point_Red;
    private Transform Spawn_Point_Blue;
    private Vector2 targetStats = new Vector2(3,3);
    private Vector2 tempVec2 = new Vector2(4, 4);

    void Start()
    {
        Spawn_Point_Red = GameObject.Find("Spawn_Point_Red").transform;
        Spawn_Point_Blue = GameObject.Find("Spawn_Point_Blue").transform;

        redPlayerPos = GameObject.Find("Red Player1");
        bluePlayerPos = GameObject.Find("Blue Player2");

        volumetricSpotLight = GetComponentInChildren<Light>();
    }
    void Update()
    {
        #region Killing logic
        if (killCheck == true)
        {
            killCheck = false;
            redInZone = false;
            blueInZone = false;
        }
        if (killing == false && powerOff == false && killCheck == false)
        {
            if (redInZone == true && killing == false)
            {
                Ray redRay = new Ray(rayPoint.transform.position, ((redPlayerPos.transform.position + new Vector3(0, 1.6f, 0)) - rayPoint.transform.position));
                RaycastHit redRayHit;
                if (Physics.Raycast(redRay, out redRayHit, 1000, redTurretMask))
                {
                    if (redRayHit.collider.gameObject.name == "Red Player1")
                    {
                        killing = true;
                        hitParticle.transform.position = redRayHit.point;
                        redRayHit.collider.gameObject.GetComponent<ThirdPersonController>().enabled = false;
                        redRayHit.collider.gameObject.transform.position = Spawn_Point_Red.position;
                        redRayHit.collider.gameObject.GetComponent<ThirdPersonController>().enabled = true;
                        StartCoroutine(Killing());
                        redInZone = false;
                    }
                }
                Debug.DrawRay(rayPoint.transform.position, ((redPlayerPos.transform.position + new Vector3(0, 1.6f, 0)) - rayPoint.transform.position).normalized * redRayHit.distance);
            }
            //killing = false;
            if (blueInZone == true && killing == false)
            {
                Ray blueRay = new Ray(rayPoint.transform.position, ((bluePlayerPos.transform.position + new Vector3(0, 1.6f, 0)) - rayPoint.transform.position));
                RaycastHit blueRayHit;
                if (Physics.Raycast(blueRay, out blueRayHit, 1000, blueTurretMask))
                {
                    if (blueRayHit.collider.gameObject.name == "Blue Player2")
                    {
                        killing = true;
                        hitParticle.transform.position = blueRayHit.point;
                        blueRayHit.collider.gameObject.GetComponent<ThirdPersonController>().enabled = false;
                        blueRayHit.collider.gameObject.transform.position = Spawn_Point_Red.position;
                        blueRayHit.collider.gameObject.GetComponent<ThirdPersonController>().enabled = true;
                        StartCoroutine(Killing());
                        blueInZone = false;
                    }
                }
                Debug.DrawRay(rayPoint.transform.position, ((bluePlayerPos.transform.position + new Vector3(0, 1.6f, 0)) - rayPoint.transform.position).normalized * blueRayHit.distance);
            }
        }
        #endregion
        #region movement logic
        if (powerOff == true)
        {
            TurretHeadBone.eulerAngles = Vector3.Lerp((TurretHeadBone.eulerAngles), new Vector3(0, 0, 80), 0.75f * Time.deltaTime);
            if (powerCheck == false)
            {
                StartCoroutine(PowerOffEffect());
            }
            powerCheck = true;
            volumetricSpotLight.enabled = false;
            killCheck = true;
        }
        else
        {
            if (lockedAngle == true)
            {
                freezeMovement = true;
            }
            if (freezeMovement == false)
            {
                relitiveTime += Time.deltaTime;
                TurretHeadBone.eulerAngles = new Vector3(0, Mathf.Sin(relitiveTime * rotateSpeed) * (rotateAngle / 2) + leftrightAngle, -upDownAngle);
            }
            else if (lockedAngle == true)
            {
                freezeMovement = true;
                TurretHeadBone.eulerAngles = new Vector3(0, leftrightAngle, -upDownAngle);
            }
            powerCheck = false;
            volumetricSpotLight.enabled = true;
        }
        
        #endregion
        #region color set
        if (canTargetRed == true)
        {
            targetStats.x = 1;
        }
        else
        {
            targetStats.x = 0;
        }
        if(canTargetBlue == true)
        {
            targetStats.y = 1;
        }
        else
        {
            targetStats.y = 0;
        }
        if (tempVec2 != targetStats)
        {
            if (canTargetRed == true && canTargetBlue == false)
            {
                volumetricSpotLight.color = new Color(1, 0, 0, 1);
                glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowRed;
                colorRing.GetComponent<MeshRenderer>().material = colorRed;
                glowMaterial.GetComponent<MeshRenderer>().material = colorRed;
            }
            else if (canTargetRed == false && canTargetBlue == true)
            {
                volumetricSpotLight.color = new Color(0, 0, 1, 1);
                glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowBlue;
                colorRing.GetComponent<MeshRenderer>().material = colorBlue;
                glowMaterial.GetComponent<MeshRenderer>().material = colorBlue;
            }
            else if (canTargetRed == true && canTargetBlue == true)
            {
                volumetricSpotLight.color = new Color(0.8333335f, 0, 1, 1);
                glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowPurple;
                colorRing.GetComponent<MeshRenderer>().material = colorPurple;
                glowMaterial.GetComponent<MeshRenderer>().material = colorPurple;
            }
            else if (canTargetRed == false && canTargetBlue == false)
            {
                volumetricSpotLight.color = new Color(1, 1, 1, 1);
                glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowWhite;
                colorRing.GetComponent<MeshRenderer>().material = colorWhite;
                glowMaterial.GetComponent<MeshRenderer>().material = colorWhite;
            }
            tempVec2 = targetStats;
            print("test");
        }
        #endregion
    }
    IEnumerator PowerOffEffect()
    {
        zapPartical.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        zapPartical.SetActive(false);
    }
    IEnumerator Killing()
    {
        hitParticle.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        killing = false;
        hitParticle.SetActive(false);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Red Player1" && canTargetRed)
        {
            Debug.Log("helllo red");
            redInZone = true;
        }
        if (other.gameObject.name == "Blue Player2" && canTargetBlue)
        {
            Debug.Log("helllo blue");
            blueInZone = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Red Player1" && canTargetRed && powerOff == false)
        {
            Debug.Log("byee red");
            redInZone = false;
        }
        if (other.gameObject.name == "Blue Player2" && canTargetBlue && powerOff == false)
        {
            Debug.Log("byee blue");
            blueInZone = false;
        }
    }
}

/*
 if(killing == false)
        {
            if (redInZone == true && killing == false)
            {
                Ray redRay = new Ray(rayPoint.transform.position, ((redPlayerPos.transform.position + new Vector3(0, 1.6f, 0)) - rayPoint.transform.position));
                RaycastHit redRayHit;
                if (Physics.Raycast(redRay, out redRayHit, 1000, redTurretMask))
                {
                    Debug.Log(redRayHit.collider);
                    if (redRayHit.collider.gameObject.name == "Red Player1")
                    {
                        Debug.Log("you died");
                        killing = true;
                        hitParticle.transform.position = redRayHit.point;
                        redRayHit.collider.gameObject.GetComponent<ThirdPersonController>().enabled = false;
                        redRayHit.collider.gameObject.transform.position = Spawn_Point_Red.position;
                        redRayHit.collider.gameObject.GetComponent<ThirdPersonController>().enabled = true;
                        StartCoroutine(Killing());
                    }
                }
                Debug.DrawRay(rayPoint.transform.position, ((redPlayerPos.transform.position + new Vector3(0, 1.6f, 0)) - rayPoint.transform.position).normalized * redRayHit.distance);
            }


            if (powerOff == false)
            {
                volumetricSpotLight.enabled = false;
            }
            else
            {
                volumetricSpotLight.enabled = true;
            }
            if (powerOff == false)
            {
                #region Set Color
                if (powerCheck == true)
                {
                    if (canTargetRed == true && canTargetBlue == false)
                    {
                        glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowRed;
                    }
                    else if (canTargetRed == false && canTargetBlue == true)
                    {
                        glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowBlue;
                    }
                    else if (canTargetRed == true && canTargetBlue == true)
                    {
                        glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowPurple;
                    }
                    else if (canTargetRed == false && canTargetBlue == false)
                    {
                        glowMaterial.GetComponent<MeshRenderer>().material = cameraGlowWhite;
                    }
                }
                #endregion
                powerCheck = false;
                volumetricSpotLight.enabled = true;
                if (freezeMovement == false)
                {
                    relitiveTime += Time.deltaTime;
                    TurretHeadBone.eulerAngles = new Vector3(0, Mathf.Sin(relitiveTime * rotateSpeed) * (rotateAngle / 2) + leftrightAngle, -upDownAngle);
                }
                hasAAngle = true;
            }
            else if (freezeMovement == true & powerOff == false & hasAAngle == false)
            {
                TurretHeadBone.eulerAngles = new Vector3(0, leftrightAngle, -upDownAngle);
                hasAAngle = true;
            }
            else if (freezeMovement == false & powerOff == true)
            {
                TurretHeadBone.eulerAngles = Vector3.Lerp((TurretHeadBone.eulerAngles), new Vector3(0, 0, 80), 0.75f * Time.deltaTime);
                volumetricSpotLight.enabled = false;
                #region Set Color
                if (canTargetRed == true && canTargetBlue == false)
                {
                    glowMaterial.GetComponent<MeshRenderer>().material = colorRed;
                }
                else if (canTargetRed == false && canTargetBlue == true)
                {
                    glowMaterial.GetComponent<MeshRenderer>().material = colorBlue;
                }
                else if (canTargetRed == true && canTargetBlue == true)
                {
                    glowMaterial.GetComponent<MeshRenderer>().material = colorPurple;
                }
                else if (canTargetRed == false && canTargetBlue == false)
                {
                    glowMaterial.GetComponent<MeshRenderer>().material = colorWhite;
                }
                if (powerCheck == false)
                {
                    StartCoroutine(PowerOffEffect());
                }
                powerCheck = true;
                #endregion
            }
        }
 */
