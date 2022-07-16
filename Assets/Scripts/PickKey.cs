using UnityEngine;

public class PickKey : MonoBehaviour
{
    public static bool playerHoldsKey { set; get; }

    void Start()
    {
        playerHoldsKey = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            playerHoldsKey = true;
            Destroy(gameObject);
        }
    }
}
