using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAmo : MonoBehaviour
{
    private float _time = 0;

    private void FixedUpdate()
    {
        _time += Time.fixedDeltaTime;

        if(_time > 5)
        {
            Destroy(gameObject); 
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (gameObject.CompareTag("AmoAttack") && !collision.collider.gameObject.CompareTag("Attack")
            || gameObject.CompareTag("AmoDefense") && !collision.collider.gameObject.CompareTag("Defense")
            )
        {
            Destroy(gameObject);
        }
    }
}
