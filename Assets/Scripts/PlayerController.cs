using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController PlayerCC;
    private GameObject MainCamera;
    private float gravity = 9.87f;
    private float verticalSpeed = 0;

    private Animator readAnim;
    private Animator textBGAnim;
    private bool nearMessage = false;
    private string message;

    private bool nearAction = false;

    public GameObject ReadIcon;
    public GameObject TextBG;
    public GameObject TextObject;
    public float walkSpeed;

    private void Start()
    {
        PlayerCC = GetComponent<CharacterController>();
        MainCamera = GameObject.Find("Main Camera");
        readAnim = ReadIcon.GetComponent<Animator>();
        textBGAnim = TextBG.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        //Camera / Eyes
        //Should add sensitivity variable eventually?
        float mouseX = (Input.mousePosition.x / Screen.width) - 0.5f;
        float mouseY = (Input.mousePosition.y / Screen.height) - 0.5f;
        transform.localRotation = Quaternion.Euler(new Vector4(transform.localRotation.y, mouseX * 360f, transform.localRotation.z));
        MainCamera.transform.localRotation = Quaternion.Euler(new Vector4(-1f * (mouseY * 180f), MainCamera.transform.localRotation.x, MainCamera.transform.localRotation.z));


        //Walk
        float horizontalMove = Input.GetAxis("Horizontal");
        float verticalMove = Input.GetAxis("Vertical");

        if (PlayerCC.isGrounded)
        {
            verticalSpeed = 0;
        }
        else
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

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (nearMessage)
            {
                readAnim.Play("ReadIconReverse");
                textBGAnim.Play("TextBGAppear");
                nearMessage = false;

                //Set Message & Toggle UI Text to restart TextFade
                TextObject.GetComponent<Text>().text = message;
                TextObject.GetComponent <TextFade>().Restart();
            }
            else if (nearAction)
            {

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Read")
        {
            readAnim.Play("ReadIcon", 0, 0.15f);
            nearMessage = true;
            message = other.gameObject.GetComponent<Message>().message;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Read" && nearMessage)
        {
            readAnim.Play("ReadIconReverse");
            nearMessage = false;
        }
    }
}
