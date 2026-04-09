using UnityEngine;

public class BallType
{
    /*
    This Script will be used to assign different types to balls.
    */
    string type;
    //TODO: variable that is used for mass and friction?

    public string getBallType()
    {
        return this.type;
    }

    public bool normal()
    {
        //This ball is used as the base ball type that everything else is changed from
        return true;
    }

    public bool duplicate()
    {
        //This ball will split into 2 (each giving .5 points) and they will move 45deg and -45deg
        // in the direction they were hit in by the cue ball.
        return true;
    }
}
