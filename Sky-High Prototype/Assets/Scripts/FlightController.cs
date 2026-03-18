// FlightController.cs 
// CENG 454 – HW1: Sky-High Prototype 
// Author: [Your Full Name] | Student ID: [Your ID] 
 
using UnityEngine; 
 
public class FlightController : MonoBehaviour 
{ 
    [SerializeField] private float pitchSpeed  = 45f;  // degrees/second 
    [SerializeField] private float yawSpeed    = 45f;  // degrees/second 
    [SerializeField] private float rollSpeed   = 45f;  // degrees/second 
    [SerializeField] private float thrustSpeed = 5f;   // units/second 
 
    private Rigidbody rb;
 
    void Start() 
    { 
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;  // Why is freezeRotation needed? Answer in your PDF. 
    } 
 
    void Update()// or FixedUpdate() 
    { 
        HandleRotation(); 
        HandleThrust(); 
    } 
 
    private void HandleRotation() 
    { 
        float pitchInput = Input.GetAxis("Vertical");
        float yawInput = Input.GetAxis("Horizontal");

        float rollInput = 0f;
        if (Input.GetKey(KeyCode.Q))
        {
            rollInput = 1f;
        }
        else if (Input.GetKey(KeyCode.E))
        {
            rollInput = -1f;
        }

        float pitchAmount = pitchInput * pitchSpeed * Time.deltaTime;
        float yawAmount = yawInput * yawSpeed * Time.deltaTime;
        float rollAmount = rollInput * rollSpeed * Time.deltaTime;

        transform.Rotate(Vector3.right, pitchAmount, Space.Self);    // Pitch
        transform.Rotate(Vector3.up, yawAmount, Space.Self);         // Yaw
        transform.Rotate(Vector3.forward, rollAmount, Space.Self);   // Roll 
    
    } 
 
    private void HandleThrust() 
    { 
        if (Input.GetKey(KeyCode.Space))
        {
        transform.Translate(Vector3.forward * thrustSpeed * Time.deltaTime, Space.Self);
        }
    } 
} 