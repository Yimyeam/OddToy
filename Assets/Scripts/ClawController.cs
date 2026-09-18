using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class ClawController : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed = 5f;

    [SerializeField]
    private float leftLimit = -5f;

    [SerializeField]
    private float rightLimit = 5f;

    [SerializeField]
    private Transform clawVertical;

    [SerializeField]
    private GrabPoint grabPoint;

    [SerializeField]
    private float verticalSpeed = 3f;

    [SerializeField]
    private float downPositionY = -2.5f;

    private bool isBusy;
    void Start()
    {

    }

    void Update()
    {
        if (isBusy)
            return;

        if (Keyboard.current.dKey.isPressed)
            MoveRight();

        if (Keyboard.current.aKey.isPressed)
            MoveLeft();

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            StartCoroutine(GrabSequence());
    }

    private void MoveRight()
    {
        transform.position += Vector3.right * moveSpeed * Time.deltaTime;

        if (transform.position.x > rightLimit)
            transform.position = new Vector3(rightLimit, transform.position.y, transform.position.z);
    }

    private void MoveLeft()
    {
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < leftLimit)
            transform.position = new Vector3(leftLimit, transform.position.y, transform.position.z);
    }

    private IEnumerator GrabSequence()
    {
        isBusy = true;

        Vector3 startPosition = clawVertical.localPosition;

        Vector3 downPosition = new Vector3(
            startPosition.x,
            downPositionY,
            startPosition.z
        );

        while (Vector3.Distance(clawVertical.localPosition, downPosition) > 0.01f)
        {
            clawVertical.localPosition = Vector3.MoveTowards(
                clawVertical.localPosition,
                downPosition,
                verticalSpeed * Time.deltaTime
            );

            yield return null;
        }

        ConveyorItem item = grabPoint.GetCurrentItem();

        if (item != null)
        {
            item.enabled = false;

            item.transform.SetParent(grabPoint.transform);
            item.transform.localPosition = Vector3.zero;

            grabPoint.ClearCurrentItem();
        }

        yield return new WaitForSeconds(0.3f);

        while (Vector3.Distance(clawVertical.localPosition, startPosition) > 0.01f)
        {
            clawVertical.localPosition = Vector3.MoveTowards(
                clawVertical.localPosition,
                startPosition,
                verticalSpeed * Time.deltaTime
            );

            yield return null;
        }

        clawVertical.localPosition = startPosition;

        if (item != null)
        {
            Destroy(item.gameObject);
        }

        isBusy = false;
    }
}