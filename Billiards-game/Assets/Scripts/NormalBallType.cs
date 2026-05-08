using UnityEngine;

[CreateAssetMenu(menuName = "Ball Types/Normal")]
public class NormalBallType : BallType
{
    public override void OnHit(GameObject self, GameObject otherBall, Vector3 hitForce)
    {
        // Does nothing
    }
    public override void Apply(GameObject self)
    {
        // Nothing!
    }
}