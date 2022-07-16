using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinZone : MonoBehaviour
{
    [SerializeField] private GameCanvasController GCC;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Attack") || (other.gameObject.CompareTag("Player") && SceneManager.IsAttack))
        {
            GCC.AttackWin();
        } 
    }
}
