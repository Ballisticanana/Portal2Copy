using UnityEngine;

public class ButtonScript4 : MonoBehaviour
{
    public bool isEndzone;
    public bool canInteractRed;
    public bool canInteractBlue;

    public bool on;

    public bool isHold;

    public GameObject turretPower0;
    public bool turretPowerInvert0;

    public GameObject turretPower1;
    public bool turretPowerInvert1;

    public GameObject turretPower2;
    public bool turretPowerInvert2;

    public Material colorRed;
    public Material colorRedGlow;
    public Material colorBlue;
    public Material colorBlueGlow;
    public Material colorPurple;
    public Material colorPurpleGlow;
    public Material colorWhite;
    public Material colorWhiteGlow;
    public GameObject button;

    private Vector3 interactStats = new Vector3(3, 3, 3);
    private Vector3 tempVec2 = new Vector3(4, 4, 4);
    void Start()
    {
        
    }
    void Update()
    {
        #region 0
        if (turretPower0 != null)
        {
            if (turretPowerInvert0 == true)
            {
                if (on)
                {
                    turretPower0.GetComponent<TurretController>().powerOff = true;
                }
                else
                {
                    turretPower0.GetComponent<TurretController>().powerOff = false;
                }
            }
            else
            {
                if (on)
                {
                    turretPower0.GetComponent<TurretController>().powerOff = false;
                }
                else
                {
                    turretPower0.GetComponent<TurretController>().powerOff = true;
                }
            }
        }
        #endregion
        #region 1
        if (turretPower0 != null)
        {
            if (turretPowerInvert1 == true)
            {
                if (on)
                {
                    turretPower1.GetComponent<TurretController>().powerOff = true;
                }
                else
                {
                    turretPower1.GetComponent<TurretController>().powerOff = false;
                }
            }
            else
            {
                if (on)
                {
                    turretPower1.GetComponent<TurretController>().powerOff = false;
                }
                else
                {
                    turretPower1.GetComponent<TurretController>().powerOff = true;
                }
            }
        }
        #endregion
        #region 2
        if (turretPower2 != null)
        {
            if (turretPowerInvert2 == true)
            {
                if (on)
                {
                    turretPower2.GetComponent<TurretController>().powerOff = true;
                }
                else
                {
                    turretPower2.GetComponent<TurretController>().powerOff = false;
                }
            }
            else
            {
                if (on)
                {
                    turretPower2.GetComponent<TurretController>().powerOff = false;
                }
                else
                {
                    turretPower2.GetComponent<TurretController>().powerOff = true;
                }
            }
        }
        #endregion
        #region color set
        if (canInteractRed == true)
        {
            interactStats.x = 1;
        }
        else
        {
            interactStats.x = 0;
        }
        if (canInteractBlue == true)
        {
            interactStats.y = 1;
        }
        else
        {
            interactStats.y = 0;
        }
        if (on == true)
        {
            interactStats.z = 1;
        }
        else
        {
            interactStats.z = 0;
        }
        if (tempVec2 != interactStats)
        {
            if (canInteractRed == true && canInteractBlue == false)
            {
                if(on)
                {
                    button.GetComponent<Renderer>().material = colorRedGlow;
                }
                else
                {
                    button.GetComponent<Renderer>().material = colorRed;
                }
            }
            else if (canInteractRed == false && canInteractBlue == true)
            {
                if (on)
                {
                    button.GetComponent<Renderer>().material = colorBlueGlow;
                }
                else
                {
                    button.GetComponent<Renderer>().material = colorBlue;
                }
            }
            else if (canInteractRed == true && canInteractBlue == true)
            {
                if (on)
                {
                    button.GetComponent<Renderer>().material = colorPurpleGlow;
                }
                else
                {
                    button.GetComponent<Renderer>().material = colorPurple;
                }
            }
            else if (canInteractRed == false && canInteractBlue == false)
            {
                if (on)
                {
                    button.GetComponent<Renderer>().material = colorWhiteGlow;
                }
                else
                {
                    button.GetComponent<Renderer>().material = colorWhite;
                }
            }
            tempVec2 = interactStats;
            print("test");
        }
        #endregion
    }
    public void OnTriggerEnter(Collider other)
    {
        if (isEndzone == true)
        {
            if (other.gameObject.name == "Red Player1" && canInteractRed)
            {
                GameObject.Find("Level Manager").GetComponent<LevelManager>().neededForSkip += 1;
            }
        }
        else
        {
            Debug.Log("collided");
            if (other.gameObject.name == "Red Player1" && canInteractRed)
            {
                if (isHold == false)
                {
                    Debug.Log("red");
                    if (!on)
                    {
                        on = true;
                    }
                    else
                    {
                        on = false;
                    }
                }
                else if (isHold == true)
                {
                    on = true;
                }
            }

            if (other.gameObject.name == "Blue Player2" && canInteractBlue)
            {
                Debug.Log("blue");
                if (isHold == false)
                {
                    if (!on)
                    {
                        on = true;
                    }
                    else
                    {
                        on = false;
                    }
                }
                else if (isHold == true)
                {
                    on = true;
                }
            }
        }
        
    }
    public void OnTriggerExit(Collider other)
    {
        if(isEndzone == true)
        {
            if (other.gameObject.name == "Red Player1" && canInteractRed)
            {
                GameObject.Find("Level Manager").GetComponent<LevelManager>().neededForSkip -= 1;
            }
        }
        else
        {
            if (other.gameObject.name == "Red Player1" && canInteractRed)
            {
                if (isHold == true)
                {
                    on = false;
                }
            }
            if (other.gameObject.name == "Blue Player2" && canInteractBlue)
            {
                if (isHold == true)
                {
                    on = false;
                }
            }
        }
    }
}
