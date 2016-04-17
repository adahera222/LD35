using UnityEngine;
using System.Collections;

public class ShapeBehavior : MonoBehaviour
{
    public float cubeSize = 1f;
    public float cubeSpeed = 1f;
    public float factorDamping = 0.5f;
    public float factorSpeed = 0.5f;
    [Range(0f, 0.499f)]
    public float factorCap = 0.9f;
    public float scaleCap = 10;
    public float squeezeForce = 10f;
    float currentRatioFactor = 0f;
    float currentRatioVelocity = 0f;
    float targetRatioFactor = 0f;
    [Range(0.2f, 10f)]
    public float maxSpeed = 5;
    [Range(0.01f, 1f)]
    public float speedAcceleration = 0.1f;
    [Range(1f, 10f)]
    public float fallSqueeze = 1f;

    Vector3 previousPosition_1;
    Vector3 previousPosition_2;
    Vector3 previousPosition_3;
    Vector3 previousPosition_4;
    bool isColliding = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        var usedFactor = currentRatioFactor * factorCap;
        transform.localScale = new Vector3( ( Mathf.Clamp(1f / (0.5f + usedFactor), -scaleCap, scaleCap) - 1f ) * cubeSize, ( Mathf.Clamp(1f / (0.5f - usedFactor), -scaleCap, scaleCap) - 1f ) * cubeSize, 1);
    }

    void FixedUpdate()
    {
        var rigidBody = GetComponent<Rigidbody>();
        if (rigidBody.IsSleeping())
        {
            rigidBody.WakeUp();
        }

        targetRatioFactor = Input.GetAxis("Vertical");
        var targetDelta = targetRatioFactor - currentRatioFactor;
        currentRatioVelocity += targetDelta * factorSpeed;
        currentRatioVelocity /= 1 + factorDamping;
        currentRatioFactor += currentRatioVelocity;

        rigidBody.AddForce(0, Input.GetAxis("Vertical") * 0.05f, 0);
    }

    void LateUpdate()
    {
        var rigidBody = GetComponent<Rigidbody>();
        if (Mathf.Abs(currentRatioVelocity) > 0.025 && isColliding)
        {
            var vel_1 = transform.position - previousPosition_1;
            var vel_2 = previousPosition_1 - previousPosition_2;
            var vel_3 = previousPosition_2 - previousPosition_3;
            var vel_4 = previousPosition_3 - previousPosition_4;
            var avgVel = new Vector3((vel_1.x + vel_2.x + vel_3.x + vel_4.x) / 4,
                (vel_1.y + vel_2.y + vel_3.y + vel_4.y) / 4,
                0);
            avgVel *= squeezeForce;
            rigidBody.velocity = avgVel;
        }

        var curVel = GetComponent<Rigidbody>().velocity;
        var horizontalForce = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontalForce) > 0.1)
        {
            if (Mathf.Abs(curVel.x) < maxSpeed)
            {
                if (horizontalForce < 0)
                {
                    curVel.x += speedAcceleration * horizontalForce;
                }
                else
                {
                    curVel.x += speedAcceleration * horizontalForce;
                }

            }
        }
        else
        {
            if (curVel.x > 0)
            {
                curVel.x += Mathf.Max(-speedAcceleration, -curVel.x);
            }
            else if (curVel.x < 0)
            {
                curVel.x += Mathf.Max(speedAcceleration, curVel.x);
            }
        }
        rigidBody.velocity = curVel;

        previousPosition_4 = previousPosition_3;
        previousPosition_3 = previousPosition_2;
        previousPosition_2 = previousPosition_1;
        previousPosition_1 = transform.position;
        isColliding = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        var vel_1 = transform.position - previousPosition_1;
        var vel_2 = previousPosition_1 - previousPosition_2;
        var vel_3 = previousPosition_2 - previousPosition_3;
        var vel_4 = previousPosition_3 - previousPosition_4;
        var avgVel = new Vector3((vel_1.x + vel_2.x + vel_3.x + vel_4.x) / 4,
            (vel_1.y + vel_2.y + vel_3.y + vel_4.y) / 4,
            0);
        avgVel *= fallSqueeze;
        currentRatioVelocity = -Mathf.Abs(avgVel.y);
    }

    void OnCollisionStay(Collision collisionInfo)
    {
        isColliding = true;
    }
}
