using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraController : MonoBehaviour
{
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 offset;
    [SerializeField] float downAngle;
    [SerializeField] float power;

    [SerializeField] GameObject cueStick;
    private float horizontalInput;

    Transform cueBall;
    GameManager gameManager;
    private bool isTakingShot;
    [SerializeField] float maxDrawDistance;
    private float savedMousePosition;
    [SerializeField] TextMeshProUGUI powerText;

    //private Vector3 currentOffset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();

        foreach(GameObject ball in GameObject.FindGameObjectsWithTag("Ball"))
        {
            if (ball.GetComponent<Ball>().IsCueBall())
            {
                cueBall = ball.transform;
                break;
            }

               
        }

        Debug.Log(cueBall);
        //currentOffset = offset;
        ResetCamera();
    }

    // Update is called once per frame
    void Update()
    {
        if(cueBall != null && !isTakingShot)
        {
            //Moving mouse left-right moves the camera in that direction over degrees per seconds
            horizontalInput = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;

            //moves around cueball, Moves on Y axis(0,1,0), what determines how much it moves
            transform.RotateAround(cueBall.position, Vector3.up, horizontalInput);
        }

        /*//Temp
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ResetCamera();
        }
        //End Temp*/

        /*Moved Down to shoot function
        if (Input.GetButtonDown("Fire1") && gameObject.GetComponent<Camera>().enabled)
        {
            Vector3 hitDirection = transform.forward;
            hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

            cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power, ForceMode.Impulse);
            cueStick.SetActive(false);
            gameManager.SwitchCameras();
        }
        */
        Shoot();
    }

    public void ResetCamera()
    {
        cueStick.SetActive(true);
        transform.position = cueBall.position + offset;
        transform.LookAt(cueBall.position);
        transform.localEulerAngles = new Vector3(downAngle, transform.localEulerAngles.y, 0);
    }

    void Shoot()
    {
        if (gameObject.GetComponent<CameraController>().enabled)
        {
            if(Input.GetButtonDown("Fire1") && !isTakingShot)
            {
                isTakingShot = true;
                savedMousePosition = 0f;
            }
            else if (isTakingShot)
            {
                if(savedMousePosition + Input.GetAxis("Mouse Y") <= 0)
                {
                    savedMousePosition += Input.GetAxis("Mouse Y");
                    if(savedMousePosition <= maxDrawDistance)
                    {
                        savedMousePosition = maxDrawDistance;
                    }
                    float powerValueNumer = ((savedMousePosition - 0) / (maxDrawDistance - 0)) * (100 - 0) + 0;
                    int powerValueInt = Mathf.RoundToInt(powerValueNumer);
                    powerText.text = "Power: " + powerValueInt + "%";
                }
                if(Input.GetButtonDown("Fire1"))
                {
                    Vector3 hitDirection = transform.forward;
                    hitDirection = new Vector3(hitDirection.x, 0, hitDirection.z).normalized;

                    cueBall.gameObject.GetComponent<Rigidbody>().AddForce(hitDirection * power * Mathf.Abs(savedMousePosition), ForceMode.Impulse);
                    cueStick.SetActive(false);
                    gameManager.SwitchCameras();
                    isTakingShot = false;
                }
            }
        }
    }
}
