using UnityEngine;

public class GameSetup : MonoBehaviour
{
    int redBallsRemanining = 7;
    int blueBallsRemanining = 7;
    float ballRadius;
    float ballDiamater;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 1f;
        ballDiameter = ballRadius * 2f;
        PlaceAllBalls();
    }

    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();
    }

    void PlaceCueBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
    }

    void PlaceEightBalls()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
    }

    void PlaceRandomBalls()
    {
        //1 ball, 2 balls, 3 balls (2nd is 8 ball), 4 balls, 5 balls
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;
    }
}
