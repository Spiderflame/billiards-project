using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isRed;
    private bool isEightBall = false;
    private bool isCueBall = false;

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
    
    private void fixedUpdat()
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
        } else if (!isCueBall || !isEightBall)
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
}
