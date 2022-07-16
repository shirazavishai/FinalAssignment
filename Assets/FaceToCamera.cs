using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FaceToCamera : MonoBehaviour
{
    public Transform _camera;

    private void Start()
    {
        _camera = Camera.main.transform;
    }

    private void Update()
    {
        transform.LookAt(_camera); 
    }
}
