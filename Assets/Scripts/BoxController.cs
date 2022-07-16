using UnityEngine;

public class BoxController : NearByCollectiable
{
    public Collider Key;

    protected override void OnAway()
    {
        if (_isOpened)
        {
            _animator.SetBool("IsOpened", false);
        }
    }

    protected override void OnNearBy()
    {
        _animator.SetBool("IsOpened", true);
    }

    public void EnableKeyCollider()
    {
        Key.enabled = true;
    }

    public void DisableKeyCollider()
    {
        Key.enabled = false;
    }
}

[RequireComponent(typeof(Animator))]
public abstract class NearByCollectiable : MonoBehaviour
{
    protected bool _playerNearBy;
    protected bool _isOpened;
    protected Animator _animator;

    void Start()
    {
        _animator = GetComponent<Animator>();
        _playerNearBy = false;
        _isOpened = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && _playerNearBy)
        {
            _isOpened = true;
        }
    }

    protected abstract void OnNearBy();

    protected abstract void OnAway();

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            _playerNearBy = true;

            OnNearBy();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Attack"))
        {
            _playerNearBy = false;

            OnAway();
        }
    }
}
