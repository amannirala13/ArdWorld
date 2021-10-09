using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotController : MonoBehaviour
{

    public Animator botAnimator;
    public CharacterController controller;
    
    public GameObject cameraGroup;
    
    public float gravity = 2;
    public float jumpPower = 5;
    public float speed = 0.8f;
    public float speedMultiplier = 1;
    public float mouseSensitivity = 1;
    
    public float health = 100;
    
    private Vector3 _motionDir = Vector3.zero;
    private Quaternion _bodyRotation;
    private Quaternion _cameraRotation;

    // Start is called before the first frame update
    void Start()
    {
        if (botAnimator == null)
            botAnimator = GetComponent<Animator>();
        if(controller == null)
            controller = GetComponent<CharacterController>();
        
        _bodyRotation = transform.localRotation;
        _cameraRotation = cameraGroup.transform.localRotation;
    }

    // Update is called once per frame
    void Update()
    {
        
        /*transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")) * (Time.deltaTime * mouseSensitivity));
        cameraGroup.transform.Rotate(new Vector3(-1 * Input.GetAxis("Mouse Y"), 0) * (Time.deltaTime * mouseSensitivity));*/

        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.W))
            {
                float speedFactor = 0;
                if (Input.GetKey(KeyCode.LeftShift))
                    speedFactor = speedMultiplier;
                        
                _motionDir = new Vector3(0, 0, 1 * Time.deltaTime ) * (speed + speedFactor) ;
                botAnimator.SetBool("Walk_Anim", true);
            }
            /*if (Input.GetKey(KeyCode.S))
            {
                _motionDir = new Vector3(0, 0, -1 * Time.deltaTime ) * speed;
                botAnimator.SetBool("Walk_Anim", true);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                _motionDir = Vector3.zero;
                botAnimator.SetBool("Walk_Anim", false);
            }*/
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _motionDir = new Vector3(0, jumpPower * Time.deltaTime, 0);
            }
            
        }
        _motionDir.y -= gravity * Time.deltaTime;

        if (Input.GetKeyUp(KeyCode.W))
        {
            _motionDir = Vector3.zero;
            botAnimator.SetBool("Walk_Anim", false);
        }

        _cameraRotation.x += (-1 * Input.GetAxis("Mouse Y")) * mouseSensitivity;
        _bodyRotation.y += Input.GetAxis("Mouse X") * mouseSensitivity;

        _cameraRotation.x = Mathf.Clamp(_cameraRotation.x, -60f, 60f);

        transform.localRotation = Quaternion.Euler(_bodyRotation.x,_bodyRotation.y, _bodyRotation.z);
        cameraGroup.transform.localRotation = Quaternion.Euler(_cameraRotation.x, _cameraRotation.y, _cameraRotation.z);

        _motionDir = transform.TransformDirection(_motionDir);
        controller.Move(_motionDir);
    }
}
