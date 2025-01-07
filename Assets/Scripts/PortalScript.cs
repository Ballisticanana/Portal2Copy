using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PortalScript : MonoBehaviour
{
    public bool isRed;

    //player cameras & transform
    public Camera player1RedCamera;
    public Transform player1RedTransform;
    public Vector3 player1RedPosition;
    public Quaternion player1RedRotation;

    public Camera player2BlueCamera;
    public Transform player2BlueTransform;
    public Vector3 player2BluePosition;
    public Quaternion player2BlueRotation;


    //player portal cameras & transform
    public Camera player1RedPortalCamera;
    public Transform player1RedPortalCameraTransform;
    public Vector3 player1RedPortalCameraPosition;
    public Quaternion player1RedPortalCameraRotation;
    public Vector3 player1RedPortalCameraNewPos;

    public Camera player2BluePortalCamera;
    public Transform player2BluePortalCameraTransform;
    public Vector3 player2BluePortalCameraPosition;
    public Quaternion player2BluePortalCameraRotation;
    public Vector3 player2BluePortalCameraNewPos;

    //Portal transform
    public Transform redPortalTransform;
    public Vector3 redPortalPosition;
    public Quaternion redPortalRotation;

    public Transform bluePortalTransform;
    public Vector3 bluePortalPosition;
    public Quaternion bluePortalRotation;



    public float testNumberX = 0;
    public float testNumberY = 0;
    public float testNumberZ = -1;
    public float testNumberW = 0;


    void Start()
    {
        //player cameras & transform
        player1RedCamera = GameObject.Find("CameraRed").GetComponentInChildren<Camera>();
        player1RedTransform = GameObject.Find("CameraRed").GetComponent<Transform>();

        player2BlueCamera = GameObject.Find("CameraBlue").GetComponentInChildren<Camera>();
        player2BlueTransform = GameObject.Find("CameraBlue").GetComponent<Transform>();

        if (gameObject.name == "Red Portal")
        {
            isRed = true;
            //player portal cameras & transform
            player1RedPortalCamera = GameObject.Find("PlayerRedViewOutRed").GetComponent<Camera>();
            player1RedPortalCameraTransform = GameObject.Find("PlayerRedViewOutRed").GetComponent<Transform>();

            player2BluePortalCamera = GameObject.Find("PlayerBlueViewOutRed").GetComponent<Camera>();
            player2BluePortalCameraTransform = GameObject.Find("PlayerBlueViewOutRed").GetComponent<Transform>();

            //portal transforms needs updates
            redPortalTransform = GameObject.Find("Red Portal").GetComponent<Transform>();

            bluePortalTransform = GameObject.Find("Blue Portal").GetComponent<Transform>();

        }
        else
        {
            isRed = false;
            //player portal cameras & transform
            player1RedPortalCamera = GameObject.Find("PlayerRedViewOutBlue").GetComponent<Camera>();
            player1RedPortalCameraTransform = GameObject.Find("PlayerRedViewOutBlue").GetComponent<Transform>();

            player2BluePortalCamera = GameObject.Find("PlayerBlueViewOutBlue").GetComponent<Camera>();
            player2BluePortalCameraTransform = GameObject.Find("PlayerBlueViewOutBlue").GetComponent<Transform>();

            //portal transforms needs updates
            redPortalTransform = GameObject.Find("Red Portal").GetComponent<Transform>();

            bluePortalTransform = GameObject.Find("Blue Portal").GetComponent<Transform>();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //player cameras & transform
        player1RedPosition = player1RedTransform.transform.position;
        player1RedRotation = player1RedTransform.transform.rotation;

        player2BluePosition = player2BlueTransform.transform.position;
        player2BlueRotation = player2BlueTransform.transform.rotation;

        //player portal cameras & transform
        player1RedPortalCameraPosition = player1RedPortalCameraTransform.position;
        player1RedPortalCameraRotation = player1RedPortalCameraTransform.rotation;

        player2BluePortalCameraPosition = player2BluePortalCameraTransform.position;
        player2BluePortalCameraRotation = player2BluePortalCameraTransform.rotation;

        //portal transforms needs updates
        redPortalPosition = redPortalTransform.position;
        redPortalRotation = redPortalTransform.rotation;

        bluePortalPosition = bluePortalTransform.position;
        bluePortalRotation = bluePortalTransform.rotation;

        if (isRed == true)
        {
            //portal high thing
            player1RedPortalCameraNewPos = bluePortalPosition - bluePortalTransform.TransformDirection(player1RedTransform.position - redPortalTransform.position);
            player1RedPortalCameraTransform.transform.position = new Vector3 (player1RedPortalCameraNewPos.x, player1RedTransform.position.y, player1RedPortalCameraNewPos.z);
            player1RedPortalCameraTransform.eulerAngles = player1RedTransform.eulerAngles + bluePortalTransform.eulerAngles + new Vector3(0,180,0);
            //portal hight + player hight = camera position y
            
            //Debug.Log(player1RedTransform.position - redPortalTransform.position);
            //Debug.Log(bluePortalTransform.TransformDirection(player1RedTransform.position - redPortalTransform.position));
            //Debug.Log(new Vector3 (player1RedPortalCameraNewPos.x, player1RedTransform.position.y, player1RedPortalCameraNewPos.z));

            player2BluePortalCameraNewPos = bluePortalPosition - bluePortalTransform.TransformDirection(player2BlueTransform.position - redPortalTransform.position);
            player2BluePortalCameraTransform.transform.position = new Vector3 (player2BluePortalCameraNewPos.x, player2BlueTransform.position.y, player2BluePortalCameraNewPos.z);
            player2BluePortalCameraTransform.eulerAngles = player2BlueTransform.eulerAngles + bluePortalTransform.eulerAngles + new Vector3(0,180,0);
            
            //portal.eular + player.eular should = correct roataion
            //in theroy this should work for floor and celling portlas

            //test this layout
            /*
            //player1RedPortalCameraNewPos = bluePortalPosition - bluePortalTransform.TransformDirection(player1RedPosition - redPortalPosition);
            //player1RedPortalCameraPosition = new Vector3 (player1RedPortalCameraNewPos.x, player1RedPosition.y + bluePortalPosition.y, player1RedPortalCameraNewPos.z);
            //player1RedPortalCameraTransform.eulerAngles = player1RedTransform.eulerAngles + bluePortalTransform.eulerAngles + new Vector3(0,180,0);
            */
        }
        else if (isRed == false)
        {
            player1RedPortalCameraNewPos =redPortalPosition - redPortalTransform.TransformDirection(player1RedTransform.position - bluePortalTransform.position);
            player1RedPortalCameraTransform.transform.position = new Vector3 (player1RedPortalCameraNewPos.x, player1RedTransform.position.y, player1RedPortalCameraNewPos.z);
            player1RedPortalCameraTransform.eulerAngles = player1RedTransform.eulerAngles + redPortalTransform.eulerAngles + new Vector3(0, 180, 0);
            // + new Vector3(0, 180, 0)

            //Debug.Log(player1RedTransform.position - bluePortalTransform.position);
            //Debug.Log(redPortalTransform.TransformDirection(player1RedTransform.position - bluePortalTransform.position));
            //Debug.Log(new Vector3(player1RedPortalCameraNewPos.x, player1RedTransform.position.y, player1RedPortalCameraNewPos.z));
        }
    }
}
