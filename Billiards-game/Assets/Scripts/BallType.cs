using UnityEngine;

public abstract class BallType : ScriptableObject
{
    public abstract void OnHit(GameObject self, GameObject otherBall, Vector3 hitForce);

    public abstract void Apply(GameObject self);
}