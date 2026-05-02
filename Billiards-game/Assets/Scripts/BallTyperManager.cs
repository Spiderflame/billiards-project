using UnityEngine;

public class BallTypeManager : MonoBehaviour
{
    public static BallTypeManager Instance;

    public GameObject ballPrefab;

    void Awake()
    {
        Instance = this;
    }

    public void OnBallHit(GameObject ball, Vector3 force)
    {
        Ball b = ball.GetComponent<Ball>();
        if (b == null) return;

        /*switch (b.Type)
        {
            case Types.Normal:
                break;

            case Types.Duplicate:
                Duplicate(ball, force);
                break;
        }*/
    }

    void Duplicate(GameObject ball, Vector3 force)
    {
        Debug.Log("Duplicate triggered");

        Vector3 pos = ball.transform.position;

        GameObject b1 = Instantiate(ballPrefab, pos, Quaternion.identity);
        GameObject b2 = Instantiate(ballPrefab, pos, Quaternion.identity);

        Rigidbody rb1 = b1.GetComponent<Rigidbody>();
        Rigidbody rb2 = b2.GetComponent<Rigidbody>();

        rb1.linearVelocity = Quaternion.Euler(0, 45, 0) * force;
        rb2.linearVelocity = Quaternion.Euler(0, -45, 0) * force;
    }
}

//TODO: Make the HIT BALL duplicate if it is a duplicate ball
/*
public class BallTypeManager : MonoBehaviour
{   
    public enum Types
    {
        Normal,
        Duplicate
    }
    
    public BallType currentType;

    public GameObject ballPrefab;

    public void OnBallHit(GameObject ball, Vector3 force)
    {
        switch (currentType)
        {
            case BallType.Normal:
                // do nothing
                break;

            case BallType.Duplicate:
                Duplicate(ball, force);
                break;
        }
    }

    void Duplicate(GameObject ball, Vector3 force)
    {
        Debug.Log("Duplicate ability triggered!");

        Vector3 spawnPos = ball.transform.position;

        Vector3 dir1 = Quaternion.Euler(0, 45, 0) * force;
        Vector3 dir2 = Quaternion.Euler(0, -45, 0) * force;

        Destroy(ball);

        GameObject b1 = Instantiate(ballPrefab, spawnPos, Quaternion.identity);
        GameObject b2 = Instantiate(ballPrefab, spawnPos, Quaternion.identity);

        b1.GetComponent<Rigidbody>().linearVelocity = dir1;
        b2.GetComponent<Rigidbody>().linearVelocity = dir2;
    }
}
*/