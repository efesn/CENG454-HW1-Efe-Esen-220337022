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
    [SerializeField] private Transform thrustReference;
    [SerializeField] private AudioSource engineAudioSource;
    [SerializeField] private AudioClip engineClip;
 
    // TODO (Task 3-A): Declare a private Rigidbody field named 'rb' 

    private Rigidbody rb;
 
    void Start() 
    { 
        // TODO (Task 3-B): Cache GetComponent<Rigidbody>() into 'rb'. 
        //                  Then set rb.freezeRotation = true. 
        //                  Why is freezeRotation needed? Answer in your PDF.
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("FlightController requires a Rigidbody on the same GameObject.");
            enabled = false;
            return;
        }

        if (thrustReference == null)
        {
            var autoRef = new GameObject("ThrustForwardRefRuntime");
            autoRef.transform.SetParent(transform, false);
            autoRef.transform.localPosition = Vector3.zero;
            autoRef.transform.localRotation = Quaternion.Euler(90f, 0f, 0f);
            thrustReference = autoRef.transform;
        }

        if (engineAudioSource == null)
        {
            engineAudioSource = GetComponent<AudioSource>();
            if (engineAudioSource == null)
            {
                engineAudioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        if (engineClip == null)
        {
            engineClip = Resources.Load<AudioClip>("Audio/airplane_engine");
        }

        engineAudioSource.playOnAwake = false;
        engineAudioSource.loop = true;
        if (engineClip != null)
        {
            engineAudioSource.clip = engineClip;
        }

        rb.freezeRotation = true;  // Why is freezeRotation needed? Answer in your PDF. 
    } 
 
    void Update()// or FixedUpdate() 
    { 
        HandleRotation(); 
        HandleThrust(); 
    } 
 
    private void HandleRotation() 
    { 
         // TODO (Task 3-C): 
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
         // TODO (Task 3-D): 
        if (Input.GetKey(KeyCode.Space))
        {
        Vector3 worldForward = thrustReference.TransformDirection(Vector3.forward);
        transform.Translate(worldForward * thrustSpeed * Time.deltaTime, Space.World);

        if (engineAudioSource != null && engineAudioSource.clip != null && !engineAudioSource.isPlaying)
        {
            engineAudioSource.Play();
        }
        }
        else
        {
            if (engineAudioSource != null && engineAudioSource.isPlaying)
            {
                engineAudioSource.Stop();
            }
        }
    } 
} 