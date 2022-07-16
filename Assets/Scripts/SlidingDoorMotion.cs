using UnityEngine;

public class SlidingDoorMotion : BaseDoorController
{
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Attack") || IsCollisionWithPlayer(other) && CanOpen)
        {
            animator.SetBool("SlideIsOpen", true);
            sound.PlayDelayed(0.5f);
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
        if (IsCollisionWithPlayer(other))
        {
            animator.SetBool("SlideIsOpen", false);
            sound.PlayDelayed(0.5f);
        }
    }
}
