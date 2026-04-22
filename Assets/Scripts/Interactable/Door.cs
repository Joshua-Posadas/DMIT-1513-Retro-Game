using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    public float openHeight = 3f;
    public float openSpeed = 3f;
    public float autoCloseDelay = 5f;

    private Vector3 closedPos;
    private Vector3 openPos;
    private bool isOpen = false;
    private bool isMoving = false;

    private DoorAudio doorAudio;

    private void Start()
    {
        closedPos = transform.position;
        openPos = closedPos + Vector3.up * openHeight;

        doorAudio = GetComponent<DoorAudio>();
    }

    public void Interact()
    {
        if (!isOpen && !isMoving)
            StartCoroutine(OpenDoor());
    }
    public bool IsOpen()
    {
        return isOpen;
    }

    private System.Collections.IEnumerator OpenDoor()
    {
        isMoving = true;

        if (doorAudio != null)
            doorAudio.PlayOpen();

        while (Vector3.Distance(transform.position, openPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, openPos, openSpeed * Time.deltaTime);
            yield return null;
        }

        isOpen = true;
        isMoving = false;

        yield return new WaitForSeconds(autoCloseDelay);

        StartCoroutine(CloseDoor());
    }

    private System.Collections.IEnumerator CloseDoor()
    {
        isMoving = true;

        if (doorAudio != null)
            doorAudio.PlayClose();

        while (Vector3.Distance(transform.position, closedPos) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, closedPos, openSpeed * Time.deltaTime);
            yield return null;
        }

        isOpen = false;
        isMoving = false;
    }
}
