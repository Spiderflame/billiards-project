using UnityEngine;

public class GameManager : MonoBehaviour
{
    
    enum CurrentPlayer
    {
        Player1,
        Player2
    }
    
    CurrentPlayer currentPlayer;
    bool isWinningShotForPlayer1 = false;
    bool isWinningShotForPlayer2 = false;
    int player1BallsRemaining = 7;
    int player2BallsRemaining = 7;

    [SerializedField] TextMeshProlGUI player1BallText;
    [SerializedField] TextMeshProlGUI player2BallText;
    [SerializedField] TextMeshProlGUI currentTurnText;
    [SerializedField] TextMeshProlGUI messageText;
    
    [SerializedField] GameObject restartButton;

    [SerializedField] Transform headPosition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentPlayer = CurrentPlayer.Player1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    bool Scratch()
    {
        if(currentPlayer == currentPlayer.Player1)
        {
            if (isWinningShotForPlayer1)
            {
                scratchOnWinningShot("Player 1");
                return true;
            }
        } else
        {
            if (isWinningShotForPlayer2)
            {
                scratchOnWinningShot("Player 2");
                return true;
            }
        }
        NextPlayerTurn();
        return false;
    }

    void EarlyEightBall()
    {
        if(currentPlayer == currentPlayer.Player1)
        {
            Lose("Player 1 Hit in the Eight Ball Too Early and has Lost!");
        } else
        {
            Lose("Player 2 Hit in the Eight Ball Too Early and has Lost!");
        }
    }

    void ScratchOnWinningShot(string player)
    {
        Lose(player + "Scratched on Their Final Shot and Has Lost");
    }

    void NoMoreBalls()
    {
        if(player1BallsRemaining == CurrentPlayer.Player1)
        {
            isWinningShotForPlayer1 = true;
        } else
        {
            isWinningShotForPlayer2 = true;
        }
    }

    bool CheckBall(Ball ball)
    {
        if (ball.IsCueBall())
        {
            //Could probably return Scratch() instead
            if (Scratch())
            {
                return true;
            }
            else
            {
                return false;
            }
        }else if (ball.IsEightBall())
        {
            if(currentPlayer == CurrentPlayer.Player1)
            {
                if (isWinningShotForPlayer1)
                {
                    Win("Player 1");
                    return true;
                }
            }
            else
            {
                if (isWinningShotForPlayer2)
                {
                    Win("Player 2");
                    return true;
                }
            }
            //Didn't win aka auto lose
            EarlyEightBall();
        }
        else
        {
            //All other logic when not eight ball or cue ball
            if (ball.IsBallRed())
            {
                player1BallsRemaining--;
                if(player1BallsRemaining <= 0)
                {
                    isWinningShotForPlayer1 = true;
                }
                if(currentPlayer != CurrentPlayer.Player1)
                {
                    //Means player 2 knocked in ball
                    NextPlayerTurn();
                }
            }
            else
            {
                player2BallsRemaining--;
                if(player1BallsRemaining <= 0)
                {
                    isWinningShotForPlayer2 = true;
                }
                if(currentPlayer != CurrentPlayer.Player2)
                {
                    //Means player 1 knocked in ball
                    NextPlayerTurn();
                }
            }
        }
        return true;
    }

    void Lose(string message)
    {
        messageText.gameObject.SetAction(true);
        messageText.test = message;
        restartButton.SetActive(true);
    }

    void Win(string player)
    {
        messageText.gameObject.SetAction(true);
        messageText.test = player + " Has Won!";
        restartButton.SetActive(true);
    }

    void NextPlayerTurn()
    {
        if (currentPlayer == CurrentPlayer.Player1)
        {
            currentPlayer = CurrentPlayer.Player2;
            currentTurnText.text = "Current Turn: Player 2";
        }
        else
        {
            currentPlayer = CurrentPlayer.Player1;
            currentTurnText.text = "Current Turn: Player 1";
        }
    }

    private void OnTriggerEnter(collider other)
    {
        if (other.gameObject.tag == "Ball")
        {
            if (CheckBall(other.gameObject.GetComponent<CheckBall>()))
            {
                Destroy(other.gameObject);
            }
            else
            {
                other.gameObject.transform = headPosition.positrion;
                other.gameObject.GetComponent<rigidbody>().velocity = vector3.zero;
                other.gameObject.GetComponent<rigidbody>().angularVelocity = vector3.zero;
            }
        }
    }
}
