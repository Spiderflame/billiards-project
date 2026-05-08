using UnityEngine;

[CreateAssetMenu(menuName = "Ball Types/Bounce")]
public class BouncyBallType : BallType
{
    [SerializeField] private BallType normalBallType;
    [SerializeField] private GameObject ballPrefab;

  public override void OnHit(
        GameObject self,
        GameObject otherBall,
        Vector3 hitForce)
    {
       
    }
    public override void Apply(GameObject self)
    {
        Rigidbody rb = self.GetComponent<Rigidbody>();
        rb.mass = .1f;
    }
}