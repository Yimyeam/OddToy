using System.Collections;
using UnityEngine;

public class ControlsHint : MonoBehaviour
{
    [SerializeField]
    private float showDuration = 5f;

    private void Start()
    {
        StartCoroutine(HideAfterDelay());
    }

    private IEnumerator HideAfterDelay()
    {
        yield return new WaitForSecondsRealtime(showDuration);

        gameObject.SetActive(false);
    }
}