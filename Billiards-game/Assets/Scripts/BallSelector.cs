using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class BallSelector : MonoBehaviour
{

    //Pause Ball selected Text
    [SerializeField] TextMeshProUGUI ballSelectedText;
    
    //Pause Ball UI Image for ball type
    [SerializeField] Image ballImage;

    //List that contains pictures for each ball type
    //0- normal, 1- Duplicate, 2- Bouncy
    [SerializeField] Sprite[] ballSprites;
    
    //List containg every ball's type
    //0- normal, 1- Duplicate, 2- Bouncy
    //Player 1: Ball 1-7 <-> Index 1-7.Player 2: Ball 9-15 <-> Index 8-14
    //Index 0 = Error
    //Index 0 also can indicate duplicate ball.
    int[] ballTypeList = new int[15];

    public static int shownIndex = 1;

    public int realIndex = 1;

    //These global values update whenever a new ball is red or blue ball is made in start up
    public static int globalRedValue = 1;
    public static int globalBlueValue = 8;

    void Start()
    {
        for (int i = 0; i < ballTypeList.Length; i++)
        {
            ballTypeList[i] = 0;
        }

        UpdateBallSelected();
    }

    public void UpBallArrow()
    {
        shownIndex++;
        realIndex++;
        if(shownIndex > 15)
        {
            shownIndex = 1;
            realIndex = 1;
        } else if (shownIndex == 8)
        {
            shownIndex = 9;
        }
        UpdateBallSelected();
    }

    public void DownBallArrow()
    {
        shownIndex--;
        realIndex--;
        if(shownIndex < 1)
        {
            shownIndex = 15;
            realIndex = 14;
        }   else if (shownIndex == 8)
        {
            shownIndex = 7;
        }
        UpdateBallSelected();
    }

    public void UpTypeArrow()
    {
        ballTypeList[realIndex]++;

        if(ballTypeList[realIndex] > (ballSprites.Length - 1))
        {
            ballTypeList[realIndex] = 0;
        }

        UpdateBallSelected();
    }

    public void DownTypeArrow()
    {
        ballTypeList[realIndex]--;

        if(ballTypeList[realIndex] < 0)
        {
            ballTypeList[realIndex] = (ballSprites.Length - 1);
        }

        UpdateBallSelected();
    }

    public void Randomize()
    {   
        for (int i = 0; i < ballTypeList.Length; i++)
        {
            ballTypeList[i] = Random.Range(0, ballSprites.Length);;
        }

        UpdateBallSelected();
    }

    void UpdateBallSelected()
    {
        ballSelectedText.text = shownIndex.ToString();

        int type = ballTypeList[realIndex];

        if (type >= 0 && type < ballSprites.Length)
        {
            ballImage.sprite = ballSprites[type];
        }
        else
        {
            Debug.LogError("Invalid ball type: " + type);
        }

        Ball[] balls = FindObjectsByType<Ball>(FindObjectsSortMode.None);

        foreach (Ball ball in balls)
        {
            ball.UpdateBall();
        }
    }

    public int GetBallType(int index)
    {
        return ballTypeList[index];
    }
}
