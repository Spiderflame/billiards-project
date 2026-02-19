using UnityEngine;

public class GameSetup : MonoBehaviour
{
    int redBallsRemanining = 7;
    int blueBallsRemanining = 7;
    float ballRadius;
    float ballDiameter;

    [SerializeField] GameObject ballPrefab;
    [SerializeField] Transform cueBallPosition;
    [SerializeField] Transform headBallPosition;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ballRadius = ballPrefab.GetComponent<SphereCollider>().radius * 1f;
        ballDiameter = ballRadius * 2f;
        //Debug.Break();
        PlaceAllBalls();
    }

    void PlaceAllBalls()
    {
        PlaceCueBall();
        PlaceRandomBalls();
    }

    void PlaceCueBall()
    {
        GameObject ball = Instantiate(ballPrefab, cueBallPosition.position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeCueBall();
    }

    void PlaceEightBall(Vector3 position)
    {
        GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
        ball.GetComponent<Ball>().MakeEightBall();
    }

    void PlaceRandomBalls()
    {
        //1 ball, 2 balls, 3 balls (2nd is 8 ball), 4 balls, 5 balls
        int NumInThisRow = 1;
        int rand;
        Vector3 firstInRowPosition = headBallPosition.position;
        Vector3 currentPosition = firstInRowPosition;

        void PlaceRedBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(true);
            redBallsRemanining--;
        }

        void PlaceBlueBall(Vector3 position)
        {
            GameObject ball = Instantiate(ballPrefab, position, Quaternion.identity);
            ball.GetComponent<Ball>().BallSetup(false);
            blueBallsRemanining--;
        }

        //Outter loop are the 5 rows
        for (int row = 0; row < 5; row++)
        {
            //Switches row (the 5 rows)
            for (int j = 0; j < NumInThisRow; j++)
            {
                //Places balls in each row
                //Checks if this is middle spot for 8 ball
                if ((row == 2) && (j == 1))
                {
                    PlaceEightBall(currentPosition);
                } 
                //If there are red and blue balls still, chose one and randomly place them
                else if ((redBallsRemanining > 0) && (blueBallsRemanining > 0))
                {
                    rand = Random.Range(0,2);
                    if (rand == 0)
                    {
                       PlaceRedBall(currentPosition); 
                    }
                    else
                    {
                        PlaceBlueBall(currentPosition);
                    }
                } 
                //If only red balls places one
                else if (redBallsRemanining > 0)
                {
                    PlaceRedBall(currentPosition); 
                }
                //Otherwise place blue ball
                else
                {
                    PlaceBlueBall(currentPosition);
                }

                //Move current position of ball to the right
                currentPosition += new Vector3(1, 0, 0).normalized * ballDiameter;
            }

            //Once all balls in row are placed move to the next row
            firstInRowPosition += Vector3.back * (Mathf.Sqrt(3) * ballRadius) + Vector3.left * ballRadius;
            currentPosition = firstInRowPosition;
            NumInThisRow++;
        }
    }
}
