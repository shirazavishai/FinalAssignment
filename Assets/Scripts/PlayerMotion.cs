using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMotion : MonoBehaviour
{
    private CharacterController cController;
    [SerializeField] private float Speed = 10;
    private float angularSpeed = 200;
    private float rotationAboutY=0;
    private float rotationAboutX = 0;
    public GameObject aCamera;// must be connected in UNITY to camera
    
    // Start is called before the first frame update
    void Start()
    {
        //connect characterController of player to cController
        cController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        float dx, dz;

        //applies on camera
        rotationAboutX -= Input.GetAxis("Mouse Y") * angularSpeed * Time.deltaTime;
        aCamera.transform.localEulerAngles = new Vector3(rotationAboutX, 0, 0);


        //applies on player(and camera)
        rotationAboutY +=  Input.GetAxis("Mouse X")* angularSpeed*Time.deltaTime;
        
        transform.localEulerAngles=(new Vector3(0, rotationAboutY,0));


        //Input.GetAxis("Horizontel") can be: -1(left) or 0 or 1(right)
        dx = Input.GetAxis("Horizontal") * Speed * Time.deltaTime;
        //Input.GetAxis("Vertical") can be: -1(back) or 0(we dont press nothing) or 1(front)
        dz = Input.GetAxis("Vertical") * Speed * Time.deltaTime;
        // this.transform.Translate(new Vector3(0, 0, 0.03f));
    
        Vector3 motion = new Vector3(dx, -1, dz);//we consider dx and dz as LOCAL coordinates
       motion= transform.TransformDirection(motion);//changes motion from local to global coordinations
        //move is based on GLOBAL coordinates
        cController.Move(motion);
    }
}
