using UnityEngine;
using System.Collections;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}


public class Player_Controller : MonoBehaviour
{
    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire;

    public SimpleTouchPad TouchPad;
    public SimpleTouchFireButton FireButton;

    private Quaternion calibrationQuaternion;

    Rigidbody rb;
    AudioSource audio;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audio = GetComponent<AudioSource>();
        //CalibrateAccelerometer();

    }
    
    void Update ()
    {
        if (FireButton.CanFire () && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
            audio.Play();
        }
        
        
    }
        
    void FixedUpdate ()
    {
        // called once per fixed physics step. Executed once per physics step.
        //float moveHorizontal = Input.GetAxis("Horizontal");
        //float moveVertical = Input.GetAxis("Vertical");
        //Vector3 movement = new Vector3 (moveHorizontal, 0.0f, moveVertical);

        //Vector3 accelerationRaw = Input.acceleration;
        //Vector3 acceleration = FixAcceleration(accelerationRaw);
        //Vector3 movement = new Vector3(acceleration.x, 0.0f, acceleration.y);
        Vector2 direction = TouchPad.GetDirection();
        Vector3 movement = new Vector3(direction.x, 0.0f, direction.y);

        rb.velocity = movement * speed;
        rb.position = new Vector3
            (
                Mathf.Clamp (rb.position.x, boundary.xMin, boundary.xMax),
                0.0f, 
                Mathf.Clamp (rb.position.z, boundary.zMin, boundary.zMax)
            );

        rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
            

    }

    //Used to calibrate the acceleration input
    //void CalibrateAccelerometer()
    //{
    //    Vector3 accelerationSnapshot = Input.acceleration;
    //    Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerationSnapshot);
    //    calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    //}

    //Get the 'calibrated' value from the Input
    //Vector3 FixAcceleration(Vector3 acceleration)
    //{
    //    Vector3 fixedAcceleration = calibrationQuaternion * acceleration;
    //    return fixedAcceleration;
    //}
    
}
