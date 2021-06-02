using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController PlayerCC;
    private float gravity = 9.87f;
    private float verticalSpeed = 0;

    public float walkSpeed;

    private void Start()
    {
        PlayerCC = GetComponent<CharacterController>();
    }

    void FixedUpdate()
    {
        //Camera / Eyes
        //Should add sensitivity variable eventually?
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f), mouseX * 360f, transform.localRotation.z));
        
        //Walk
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (PlayerCC.isGrounded)
        {
            verticalSpeed = 0;
        } else
        {
            verticalSpeed -= gravity * Time.deltaTime;
        }

        Vector3 fixedForward = new Vector3(transform.forward.x, 0, transform.forward.z);    //Prevent upwards movement issue

        Vector3 gravityMove = new Vector3(0, verticalSpeed, 0);
        Vector3 move = fixedForward * verticalMove + transform.right * horizontalMove;
        PlayerCC.Move(walkSpeed * Time.deltaTime * move + gravityMove * Time.deltaTime);

        //anim.SetFloat("walk", Mathf.Abs(Input.GetAxis("Vertical")));
        //https://itnext.io/how-to-write-a-simple-3d-character-controller-in-unity-1a07b954a4ca?gi=6ca306c360ca

        //Toggle Flashlight?
    }
}
