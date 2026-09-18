using UnityEngine;

public class GrabPoint : MonoBehaviour
{
    private ConveyorItem currentItem;

    private void OnTriggerEnter(Collider other)
    {
        ConveyorItem item = other.GetComponent<ConveyorItem>();

        if (item == null)
            return;

        currentItem = item;
    }

    private void OnTriggerExit(Collider other)
    {
        ConveyorItem item = other.GetComponent<ConveyorItem>();

        if (item == null)
            return;

        if (currentItem == item)
            currentItem = null;
    }

    public ConveyorItem GetCurrentItem()
    {
        return currentItem;
    }

    public void ClearCurrentItem()
    {
        currentItem = null;
    }
}