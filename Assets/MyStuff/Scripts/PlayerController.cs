using StarterAssets;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using UnityEngine.Windows;

public class PlayerController : MonoBehaviour
{
    public StarterAssetsInputs input;
    public Rigidbody rb;

    public float walkSpeed;
    public float runSpeed;

    public float targetSpeed;
    public float speed;
    public float SpeedChangeRate = 10.0f;
    public float RotationSmoothTime = 0.12f;

    public Vector3 currentHorizontalDirection;

    private float animationBlend;
    private float rotationVelocity;
    private float verticalVelocity;
    private float terminalVelocity = 53.0f;
    private float targetRotation = 0.0f;

    private GameObject mainCamera;

    public Vector3 currentVelocity;

    public bool inAir;
    void Start()
    {
        input = GetComponent<StarterAssetsInputs>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
    }
    
    public void Move()
    {
        //When theres no input sprint is reset too false
        if (input.sprint == true & input.move == Vector2.zero)
        {
            input.sprint = false;
        }

        //chooses which speed to use
        if (input.sprint == false)
        {
            targetSpeed = walkSpeed;
        }else if (input.sprint == true)
        {
            targetSpeed = runSpeed;
        }

        //Set Target speed to 0 when player is not trying to move
        if (input.move == Vector2.zero)
        {
            targetSpeed = 0.0f;
        }

        float currentHorizontalSpeed = new Vector3(rb.linearVelocity.x, 0.0f, rb.linearVelocity.z).magnitude;

        float speedOffset = 1f;
        float inputMagnitude = input.analogMovement ? input.move.magnitude : 1f;

        // accelerate or decelerate to target speed
        if (currentHorizontalSpeed < targetSpeed - speedOffset ||
            currentHorizontalSpeed > targetSpeed + speedOffset)
        {
            // creates curved result rather than a linear one giving a more organic speed change
            // note T in Lerp is clamped, so we don't need to clamp our speed
            speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                Time.deltaTime * SpeedChangeRate);

            // round speed to 3 decimal places
            speed = Mathf.Round(speed * 1000f) / 1000f;
        }
        else
        {
            speed = targetSpeed;
        }

        animationBlend = Mathf.Lerp(animationBlend, targetSpeed, Time.deltaTime * SpeedChangeRate);
        if (animationBlend < 0.01f) animationBlend = 0f;

        // normalise input direction
        Vector3 inputDirection = new Vector3(input.move.x, 0.0f, input.move.y).normalized;

        Vector3 targetDirection = Quaternion.Euler(0.0f, targetRotation + currentHorizontalDirection.y, 0.0f) * Vector3.forward;
        currentHorizontalDirection = Vector3.zero;

        // move the player
        rb.AddForce(targetDirection.normalized * (speed * Time.deltaTime), ForceMode.VelocityChange);
        Debug.Log("down here");


        /*float currentV = new Vector3(rb.linearVelocity.x, 0, rb.linearVelocity.z).magnitude;
        //If the player isnt sprinting run A else run B
        if (input.sprint == false & inAir == false & currentV < 3)
        {
            rb.AddForce(100 * new Vector3(input.move.x, 0, input.move.y) * walkSpeed * Time.deltaTime, ForceMode.Force);
        }
        else if (input.sprint == true & inAir == false & currentV < )
        {
            rb.AddForce(100 * new Vector3(input.move.x, 0, input.move.y) * runSpeed * Time.deltaTime, ForceMode.Force);
        }*/
        // if there is a move input rotate player when the player is moving
        /*if (input.move != Vector2.zero)
        {
            targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotationVelocity,RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }*/
    }
}
