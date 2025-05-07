using UnityEngine;

public class ButtonScript : MonoBehaviour
{
    public bool canInteractRed;
    public bool canInteractBlue;

    public bool on;

    public GameObject turretPower;
    public bool turretPowerInvert;

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
        if (turretPower != null)
        {
            if (turretPowerInvert == true)
            {
                if (on)
                {
                    turretPower.GetComponent<TurretController>().powerOff = true;
                }
                else
                {
                    turretPower.GetComponent<TurretController>().powerOff = false;
                }
            }
            else
            {
                if (on)
                {
                    turretPower.GetComponent<TurretController>().powerOff = false;
                }
                else
                {
                    turretPower.GetComponent<TurretController>().powerOff = true;
                }
            }
        }
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
}
