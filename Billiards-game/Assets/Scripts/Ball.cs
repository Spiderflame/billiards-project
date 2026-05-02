using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isRed;
    private bool isEightBall = false;
    private bool isCueBall = false;

    private bool hasDuplicated = false;

    public BallType ballType;

    private Rigidbody rB;

    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // FIX: Unity uses velocity, not linearVelocity
        if (rB.linearVelocity.y > 0)
        {
            Vector3 newVelocity = rB.linearVelocity;
            newVelocity.y = 0f;
            rB.linearVelocity = newVelocity;
        }
    }

    public bool IsBallRed() => isRed;
    public bool IsEightBall() => isEightBall;
    public bool IsCueBall() => isCueBall;

    public void BallSetup(bool red)
    {
        isRed = red;

        if (isCueBall || isEightBall) return;

        GetComponent<Renderer>().material.color = isRed ? Color.red : Color.blue;
    }

    public void MakeCueBall()
    {
        isCueBall = true;
    }

    public void MakeEightBall()
    {
        isEightBall = true;
        GetComponent<Renderer>().material.color = Color.black;
    }

    // Handles collisions
    private void OnCollisionEnter(Collision collision)
    {
    Ball otherBall = collision.gameObject.GetComponent<Ball>();
    if (otherBall == null) return;

    Debug.Log("Hit detected: " + name + " -> " + otherBall.name);

    // THIS ball decides behavior, not the other one
    HandleBallTypeEffect(otherBall, collision);
    }

    private void HandleBallTypeEffect(Ball otherBall, Collision collision)
    {
    Vector3 force = collision.relativeVelocity;

    /*switch (Types)
    {
        case Types.Normal:
            HandleNormal(otherBall, force);
            break;

        case Types.Duplicate:
            HandleDuplicate(otherBall, force);
            break;
    }*/
    }

    private void HandleNormal(Ball otherBall, Vector3 force)
    {
        Debug.Log("Normal ball hit: " + otherBall.name);
        // normal behavior here
    }

    private void HandleDuplicate(Ball otherBall, Vector3 force)
    {
    if (hasDuplicated) return;

    Debug.Log("Duplicate ball hit: " + otherBall.name);

    hasDuplicated = true;

    Instantiate(otherBall.gameObject,
        otherBall.transform.position + Vector3.up * 0.2f,
        otherBall.transform.rotation);
    }
}

/*
public class Ball : MonoBehaviour
{
    private bool isRed;
    private bool isEightBall = false;
    private bool isCueBall = false;

    public BallType ballType;

    Rigidbody rB;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
    
    private void fixedUpdate()
    {
        if (rB.linearVelocity.y > 0)
        {
            Vector3 newVelocity = rB.linearVelocity;
            newVelocity.y = 0f;
            rB.linearVelocity = newVelocity;
        }
    }

    public bool IsBallRed()
    {
        return isRed;
    }

    public bool IsEightBall()
    {
        return isEightBall;
    }

    public bool IsCueBall()
    {
        return isCueBall;
    }

    public void BallSetup(bool red)
    {
        isRed = red;
        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
        } else if (!isCueBall && !isEightBall)
        {
            GetComponent<Renderer>().material.color = Color.blue;
        }
    }

    public void MakeCueBall()
    {
        isCueBall = true;
    }

    public void MakeEightBall()
    {
        isEightBall = true;
        GetComponent<Renderer>().material.color = Color.black;
    }

    //Handles collisions
    private void OnCollisionEnter(Collision collision)
    {
        Ball otherBall = collision.gameObject.GetComponent<Ball>();
        if (otherBall == null) return;

        Debug.Log("Hit detected: " + gameObject.name + " -> " + otherBall.name);
        Debug.Log(gameObject.name + " ballType = " + ballType);

        if (otherBall.ballType != null)
        {
            Debug.Log("Triggering ability on: " + otherBall.name);

            otherBall.ballType.OnHit(otherBall.gameObject, collision.relativeVelocity);
        }
    }
}
*/
