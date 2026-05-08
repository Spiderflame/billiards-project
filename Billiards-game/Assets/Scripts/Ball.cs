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

    Vector3 force = collision.relativeVelocity;

    if (ballType != null)
    {
        ballType.OnHit(gameObject, otherBall.gameObject, force);
    }
    }
}