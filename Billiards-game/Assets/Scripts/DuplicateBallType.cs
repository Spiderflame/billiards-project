using UnityEngine;

[CreateAssetMenu(menuName = "Ball Types/Duplicate")]
public class DuplicateBallType : BallType
{
    [SerializeField] private float splitForce = .5f;

    [SerializeField] private BallType normalBallType;
    [SerializeField] private GameObject ballPrefab;

    public override void OnHit(
        GameObject self,
        GameObject otherBall,
        Vector3 hitForce)
    {
        Debug.Log("Duplicate triggered");
        
        Ball hitter = otherBall.GetComponent<Ball>();
        Ball target = self.GetComponent<Ball>();

        if (hitter == null || target == null)
            return;

        // ONLY cue ball activates it
        if (!hitter.IsCueBall())
            return;

        // Prevent cue/eight duplication
        if (target.IsCueBall() || target.IsEightBall())
            return;

        Vector3 spawnPos = self.transform.position;
        Quaternion rot = self.transform.rotation;

        // Create two replacement balls
        GameObject ball1 = Instantiate(self, spawnPos, rot);
        GameObject ball2 = Instantiate(self, spawnPos, rot);

        // Remove special ability from new balls
        ball1.GetComponent<Ball>().ballType = normalBallType;
        ball2.GetComponent<Ball>().ballType = normalBallType;

        Rigidbody rb1 = ball1.GetComponent<Rigidbody>();
        Rigidbody rb2 = ball2.GetComponent<Rigidbody>();

        Vector3 hitDirection = hitForce.normalized;
        hitDirection.y = 0f;

        Vector3 leftDir =
            Quaternion.Euler(0, -45, 0) * hitDirection;

        Vector3 rightDir =
            Quaternion.Euler(0, 45, 0) * hitDirection;

        float incomingSpeed = hitForce.magnitude * 0.5f;

        rb1.linearVelocity = leftDir * incomingSpeed * splitForce;
        rb2.linearVelocity = rightDir * incomingSpeed * splitForce;

        // Remove original special ball
        Destroy(self);
    }

    public override void Apply(GameObject self)
    {
        // Nothing!
    }
}