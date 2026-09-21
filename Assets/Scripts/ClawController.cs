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
    private ClawVisual clawVisual;

    [SerializeField]
    private Transform destroyPoint;

    [SerializeField]
    private float verticalSpeed = 3f;

    [SerializeField]
    private float autoMoveSpeed = 5f;

    [SerializeField]
    private float downPositionY = -2.5f;

    [SerializeField]
    private AudioSource moveAudioSource;

    [SerializeField]
    private AudioSource sfxAudioSource;

    [SerializeField]
    private AudioClip clawClangClip;

    [SerializeField]
    private AudioClip grabClip;

    [SerializeField]
    private AudioClip dropClip;


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

        if (Keyboard.current.dKey.wasPressedThisFrame ||
            Keyboard.current.aKey.wasPressedThisFrame)
        {
            moveAudioSource.Stop();
            moveAudioSource.Play();
        }

        if (Keyboard.current.spaceKey.wasPressedThisFrame)
            StartCoroutine(GrabSequence());
    }


    private void StopMoveSound()
    {
        if (moveAudioSource.isPlaying)
            moveAudioSource.Stop();
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

        float startX = transform.position.x;

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

        clawVisual.CloseClaw();
        sfxAudioSource.PlayOneShot(clawClangClip, 0.15f);

        yield return new WaitForSeconds(0.25f);

        ConveyorItem item = grabPoint.GetCurrentItem();

        if (item != null)
        {
            sfxAudioSource.PlayOneShot(grabClip, 0.25f);

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

        if (item == null)
        {
            clawVisual.OpenClaw();

            yield return new WaitForSeconds(0.25f);
        }

        if (item != null)
        {
            while (Mathf.Abs(transform.position.x - destroyPoint.position.x) > 0.01f)
            {
                float newX = Mathf.MoveTowards(
                    transform.position.x,
                    destroyPoint.position.x,
                    autoMoveSpeed * Time.deltaTime
                );

                transform.position = new Vector3(
                    newX,
                    transform.position.y,
                    transform.position.z
                );

                yield return null;
            }

            if (item.GetToyType() == ToyType.Anomaly)
            {
                GameManager.instance.AddScore(3);
            }
            else
            {
                GameManager.instance.AddScore(-2);
            }

            clawVisual.OpenClaw();

            sfxAudioSource.PlayOneShot(dropClip, 0.05f);

            Destroy(item.gameObject);

            yield return new WaitForSeconds(0.3f);

            while (Mathf.Abs(transform.position.x - startX) > 0.01f)
            {
                float newX = Mathf.MoveTowards(
                    transform.position.x,
                    startX,
                    autoMoveSpeed * Time.deltaTime
                );

                transform.position = new Vector3(
                    newX,
                    transform.position.y,
                    transform.position.z
                );

                yield return null;
            }
        }

        isBusy = false;
    }
}