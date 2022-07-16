using UnityEngine;

public class RotateDoor : BaseDoorController
{
    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other.gameObject.tag + " Enter");

        if ((other.gameObject.CompareTag("Attack") || IsCollisionWithPlayer(other)) && CanOpen)
        {
            animator.SetBool("DoorIsOpen", true);
            sound.PlayDelayed(1f);
            _isOpened = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Amo"))
        {
            Debug.Log("inside");
            _doorStreangth -= 1;
        }

        if (_doorStreangth <= 0)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(IsCollisionWithPlayer(other))
        {
            animator.SetBool("DoorIsOpen", false);
            sound.PlayDelayed(1f);
            _isOpened = false;
        }
    }
}