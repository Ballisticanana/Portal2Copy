using Unity.VisualScripting;
using UnityEngine;

public class DoorScript9 : MonoBehaviour
{
    public bool redDoor;
    public bool blueDoor;

    public bool on;
    private int maxPower;
    private int currentPower;
    public float step;
    public float doorWidth;

    public GameObject buttonInput0;
    public GameObject buttonInput1;
    public GameObject buttonInput2;

    private GameObject doorL;
    private GameObject doorR;

    public Material colorRed;
    public Material colorRedGlow;
    public Material colorBlue;
    public Material colorBlueGlow;
    public Material colorPurple;
    public Material colorPurpleGlow;
    public Material colorWhite;
    public Material colorWhiteGlow;
    public GameObject doorLight;
    private Vector3 interactStats = new Vector3(3, 3, 3);
    private Vector3 tempVec2 = new Vector3(4, 4, 4);
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        #region Find doors
        doorL = transform.Find("Door L").gameObject;
        doorR = transform.Find("Door R").gameObject;
        #endregion
        #region Find max power
        if (buttonInput0 != null)
        {
            maxPower = maxPower + 1;
        }
        if (buttonInput1 != null)
        {
            maxPower = maxPower + 1;
        }
        if (buttonInput2 != null)
        {
            maxPower = maxPower + 1;
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        #region Find current power
        currentPower = 0;
        if (buttonInput0 != null)
        {
            if(buttonInput0.GetComponent<ButtonScript>().on == true)
            {
                currentPower = currentPower + 1;
            }
        }
        if (buttonInput1 != null)
        {
            if(buttonInput1.GetComponent<ButtonScript>().on == true)
            {
                currentPower = currentPower + 1;
            }
        }
        if (buttonInput2 != null)
        {
            if(buttonInput2.GetComponent<ButtonScript>().on == true)
            {
                currentPower = currentPower + 1;
            }
        }
        #endregion
        #region Max power?
        if(currentPower == maxPower)
        {
            on = false;
        }
        else
        {
            on = true;
        }
        #endregion
        #region open & close
        if (on == false)
        {
            doorL.transform.localPosition = new Vector3(0, 2, 1 + 1.99f);
            doorR.transform.localPosition = new Vector3(0, 2, -1 - 1.99f);  //Mathf.Lerp(doorR.transform.position.z, transform.localPosition.z - 2.99f, step * Time.deltaTime));
        }
        if (on == true)
        {
            doorL.transform.localPosition = new Vector3(0, 2, 1);
            doorR.transform.localPosition = new Vector3(0, 2, -1);  //Mathf.Lerp(doorR.transform.position.z, transform.localPosition.z - 1, step * Time.deltaTime));
        }
        #endregion
        #region color set
        if (redDoor == true)
        {
            interactStats.x = 1;
        }
        else
        {
            interactStats.x = 0;
        }
        if (blueDoor == true)
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
            if (redDoor == true && blueDoor == false)
            {
                if (on)
                {
                    doorLight.GetComponent<Renderer>().material = colorRedGlow;
                }
                else
                {
                    doorLight.GetComponent<Renderer>().material = colorRed;
                }
            }
            else if (redDoor == false && blueDoor == true)
            {
                if (on)
                {
                    doorLight.GetComponent<Renderer>().material = colorBlueGlow;
                }
                else
                {
                    doorLight.GetComponent<Renderer>().material = colorBlue;
                }
            }
            else if (redDoor == true && blueDoor == true)
            {
                if (on)
                {
                    doorLight.GetComponent<Renderer>().material = colorPurpleGlow;
                }
                else
                {
                    doorLight.GetComponent<Renderer>().material = colorPurple;
                }
            }
            else if (redDoor == false && blueDoor == false)
            {
                if (on)
                {
                    doorLight.GetComponent<Renderer>().material = colorWhiteGlow;
                }
                else
                {
                    doorLight.GetComponent<Renderer>().material = colorWhite;
                }
            }
            tempVec2 = interactStats;
        }
        #endregion
    }
}
