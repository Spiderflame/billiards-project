using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private bool isRed;
    private bool isEightBall = false;
    private bool isCueBall = false;    

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
