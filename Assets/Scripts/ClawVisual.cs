using UnityEngine;

public class ClawVisual : MonoBehaviour
{
    public Transform leftClaw;
    public Transform rightClaw;

    public Vector3 leftClosedRotation;
    public Vector3 rightClosedRotation;

    public float moveSpeed = 6f;

    private Quaternion leftOpenRotation;
    private Quaternion rightOpenRotation;

    private Quaternion leftTargetRotation;
    private Quaternion rightTargetRotation;

    void Start()
    {
        leftOpenRotation = leftClaw.localRotation;
        rightOpenRotation = rightClaw.localRotation;

        leftTargetRotation = leftOpenRotation;
        rightTargetRotation = rightOpenRotation;
    }

    void Update()
    {
        leftClaw.localRotation = Quaternion.Slerp(
            leftClaw.localRotation,
            leftTargetRotation,
            moveSpeed * Time.deltaTime
        );

        rightClaw.localRotation = Quaternion.Slerp(
            rightClaw.localRotation,
            rightTargetRotation,
            moveSpeed * Time.deltaTime
        );
    }

    public void CloseClaw()
    {
        leftTargetRotation = Quaternion.Euler(leftClosedRotation);
        rightTargetRotation = Quaternion.Euler(rightClosedRotation);
    }

    public void OpenClaw()
    {
        leftTargetRotation = leftOpenRotation;
        rightTargetRotation = rightOpenRotation;
    }
}