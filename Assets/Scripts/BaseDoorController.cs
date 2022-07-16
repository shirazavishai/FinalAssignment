using System;
using UnityEngine;

public class BaseDoorController : MonoBehaviour
{
    protected Animator animator;
    protected AudioSource sound;
    protected bool _isOpened;
    [SerializeField] protected int _doorStreangth = 20000;

    public event Action DetroyedEvent;

    protected bool HoldsKey
    {
        get
        {
            return PickKey.playerHoldsKey;
        }
    }

    protected bool IsCollisionWithPlayer(Collider other)
    {
        return other.gameObject.CompareTag("Player");
    }

    protected bool CanOpen
    {
        get
        {
            return !_isOpened && HoldsKey;
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        sound = GetComponent<AudioSource>();

        _isOpened = false;
    }

    private void OnDisable()
    {
        if (DetroyedEvent != null)
        {
            DetroyedEvent.Invoke();
        }
    }
}
