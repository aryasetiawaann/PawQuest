using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    Animator anim;
    float speed = 2f;
    bool jumping = false;

    public Transform cameraTransform; // Assign the camera in the Inspector
    public float mouseSensitivity = 100f;
    public float verticalClamp = 85f;

    private float xRotation = 0f;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetBool("isWalk", false);

        Cursor.lockState = CursorLockMode.Locked; // Locks the cursor to the game window
    }

    void Update()
    {
        // Camera rotation with mouse
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

        xRotation = Mathf.Clamp(xRotation, -verticalClamp, verticalClamp);

        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Vertical rotation
        transform.Rotate(Vector3.up * mouseX); // Horizontal rotation

        // Movement
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (v != 0f)
        {
            anim.SetBool("isWalk", true);
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("isRun", true);
                speed = 5f;
            }
            this.transform.Translate(new Vector3(0, 0, v) * Time.deltaTime * speed);
        }
        else
        {
            anim.SetBool("isWalk", false);
        }

        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            anim.SetBool("isRun", false);
            speed = 2f;
        }

        // Jumping
        if (Input.GetKeyDown(KeyCode.Space) && !jumping)
        {
            jumping = true;
            anim.SetBool("isJump", true);
            Invoke("selesai_lompat", 0.1f);
            this.GetComponent<Rigidbody>().AddForce(Vector3.up * 180f);
        }

        // Side movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isLeft", true);
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else
        {
            anim.SetBool("isLeft", false);
        }

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRight", true);
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            anim.SetBool("isRight", false);
        }

        // Attack
        if (Input.GetMouseButtonDown(0))
        {
            anim.SetTrigger("isAttack");
        }
    }

    void selesai_lompat()
    {
        anim.SetBool("isJump", false);
    }

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Plane")
        {
            jumping = false;
        }
    }
}
