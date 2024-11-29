using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScHero : MonoBehaviour
{
    Animator anim;
    float speed = 2f;
    bool jumping = false;

    void Start()
    {
        anim = this.GetComponent<Animator>();
        anim.SetBool("isWalk", false);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // Forward/backward movement
        if (v != 0f)
        {
            anim.SetBool("isWalk", true);
            anim.SetFloat("speed", v);

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

        // Left movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            anim.SetBool("isLeft", true);
            this.transform.Translate(Vector3.left * Time.deltaTime * speed);
        }
        else
        {
            anim.SetBool("isLeft", false);
        }

        // Right movement
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            anim.SetBool("isRight", true);
            this.transform.Translate(Vector3.right * Time.deltaTime * speed);
        }
        else
        {
            anim.SetBool("isRight", false);
        }

        // Attack on Left Mouse Button Click
        if (Input.GetMouseButtonDown(0)) // Left mouse button
        {
            anim.SetTrigger("isAttack"); // Trigger attack animation
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
