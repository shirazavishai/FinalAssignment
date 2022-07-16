using UnityEngine;

public class LifeManager : MonoBehaviour
{
    [SerializeField] private int _startlife = 3;
    [SerializeField] private int _currentLife = 3;

    private void Start()
    {
        _currentLife = _startlife;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_currentLife == 0) return;

        if (other.CompareTag("Amo"))
        {
            _currentLife--; 
        }   
    }
}
