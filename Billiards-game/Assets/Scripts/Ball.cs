using UnityEngine;

public class Ball : MonoBehaviour
{
    private bool isRed;
    private bool isEightBall = false;
    private bool isCueBall = false;

    public BallType ballType;

    //Index stores in tha ball what type it is
    public int index;

    private Rigidbody rB;

    void Start()
    {
        rB = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
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

        if (isRed)
        {
            GetComponent<Renderer>().material.color = Color.red;
            index = BallSelector.globalRedValue;
            BallSelector.globalRedValue++;
            Debug.Log("Global Red Value: " + BallSelector.globalRedValue);

        } else
        {
            GetComponent<Renderer>().material.color = Color.blue;
            index = BallSelector.globalBlueValue;
            BallSelector.globalBlueValue++;
            Debug.Log("Global Blue Value: " + BallSelector.globalBlueValue);
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

    // Handles collisions
    private void OnCollisionEnter(Collision collision)
    {
    Ball otherBall = collision.gameObject.GetComponent<Ball>();
    if (otherBall == null) return;

    Vector3 force = collision.relativeVelocity;

    if (ballType != null)
    {
        Debug.Log("Hit detected: " + name + " -> " + otherBall.name);
        ballType.OnHit(gameObject, otherBall.gameObject, force);
    }
    }

    public void UpdateBall()
    {
        BallSelector selector = FindFirstObjectByType<BallSelector>();
        
        //0- normal, 1- Duplicate, 2- Bouncy
        int currentType = selector.GetBallType(index);

        switch (currentType)
        {
            case 0:
                ballType = ScriptableObject.CreateInstance<NormalBallType>();
                break;

            case 1:
                ballType = ScriptableObject.CreateInstance<DuplicateBallType>();
                break;

            case 2:
                ballType = ScriptableObject.CreateInstance<BouncyBallType>();
                break;

            default:
                Debug.Log("Ball is not updating");
                break;
        }
    }
}