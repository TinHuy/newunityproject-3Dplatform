using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    public float maxSpeed;
    float rotation = 0.0f;
    float camRotation = 0.0f;
    GameObject cam;
    public float rotationSpeed = 2.0f;
    public float camRotationSpeed = 1.5f;
    Rigidbody myRigidbody;

    bool isOnGround;
    public GameObject GroundChecker;
    public LayerMask GroundLayer;
    public float jumpForce = 300.0f;

    public float normalSpeed = 10.0f;
    public float SprintSpeed = 20.0f;

    public float masSprint = 5.0f;
    float sprintTimer;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        sprintTimer = maxSpeed;

        cam = GameObject.Find("Main Camera");
        myRigidbody = GetComponent<Rigidbody>(); 
    }

    // Update is called once per frame
    void Update()
    {

        isOnGround = Physics.CheckSphere(GroundChecker.transform.position, 0.1f, GroundLayer);

        if (isOnGround == true && Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.AddForce(transform.up * jumpForce);
        }

        if (Input.GetKey(KeyCode.LeftShift) && sprintTimer > 0.0f)
        {
            maxSpeed = SprintSpeed;
            sprintTimer = sprintTimer - Time.deltaTime;
        } else
        {
            maxSpeed = normalSpeed;
            if (Input.GetKey(KeyCode.LeftShift) == false)  {
                sprintTimer = sprintTimer + Time.deltaTime;
            }
        }

        sprintTimer = Mathf.Clamp(sprintTimer, 0.0f, masSprint);

        transform.position = transform.position + (transform.forward * Input.GetAxis("Vertical"));
        transform.position = transform.position + (transform.right * Input.GetAxis("Horizontal"));

        Vector3 newVelocity = transform.forward * Input.GetAxis("Vertical") * maxSpeed + (transform.right * Input.GetAxis("Horizontal") * maxSpeed);
        myRigidbody.velocity = new Vector3(newVelocity.x, myRigidbody.velocity.y, newVelocity.z);

        rotation = rotation + Input.GetAxis("Mouse X") * rotationSpeed;
        transform.rotation = Quaternion.Euler(new Vector3(0.0f, rotation, 0.0f));

        camRotation = camRotation + -Input.GetAxis("Mouse Y") * rotationSpeed;
        cam.transform.localRotation = Quaternion.Euler(new Vector3(camRotation, 0.0f, 0.0f));
    }
}
