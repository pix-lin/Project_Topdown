using System;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float walkMaxSpeed;

    float h;
    float v;
    //bool hDown, hUp;
    //bool vDown, vUp;
    //bool hEnter, vEnter;
    //bool isWalking;

    Rigidbody2D rigid;
    Animator anime;

    Vector2 lastDirec = Vector2.zero;

    //AnimatorStateInfo animeInfo;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anime = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        /*
        Don't use AddForce() Func. Because it makes to move continue. (add acceleration)
        rigid.AddForce(new Vector2(h, v), ForceMode2D.Impulse);
        if (Input.GetButtonUp("Horizontal") || Input.GetButtonUp("Vertical"))
            rigid.linearVelocity = Vector2.zero;
        */

        //Move to horizontal / vertical by button
        rigid.linearVelocity = new Vector2(h, v) * walkMaxSpeed;

        //Scan Raycast
        Debug.DrawRay(rigid.position, lastDirec * 0.65f, new Color(1, 0, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(rigid.position, lastDirec, 0.65f, LayerMask.GetMask("Door"));

        if (rayHit.collider != null)
        {
            switch(rayHit.collider.tag)
            {
                case "Up":
                    {
                        //Scene 로딩 함수
                        Debug.Log("Up Door");
                        break;
                    }
                case "Down":
                    {
                        //Scene 로딩 함수
                        Debug.Log("Down Door");
                        break;
                    }
                case "Left":
                    {
                        //Scene 로딩 함수
                        Debug.Log("Left Door");
                        break;
                    }
                case "Right": //Default
                    {
                        //Scene 로딩 함수
                        Debug.Log("Right Door");
                        break;
                    }
            }
            
        }

    }

    private void Update()
    {
        //Get Move Button Sign
        h = Input.GetAxisRaw("Horizontal");
        v = Input.GetAxisRaw("Vertical");

        //Check Button Up&Down
        //hDown = Input.GetButtonDown("Horizontal");
        //vDown = Input.GetButtonDown("Vertical");
        //hUp = Input.GetButtonUp("Horizontal");
        //vUp = Input.GetButtonUp("Vertical");
        //hEnter = Input.GetButton("Horizontal");
        //vEnter = Input.GetButton("Vertical");

        //Idle, Walk Animation
        //Send a signal to the transition only Once
        if (anime.GetInteger("vAxisRaw") != (int)v)
        {
            anime.SetBool("IsWalk", true);
            anime.SetInteger("vAxisRaw", (int)v);
            anime.SetInteger("hAxisRaw", 0);
        }
        else if (anime.GetInteger("hAxisRaw") != (int)h)
        {
            anime.SetBool("IsWalk", true);
            if (v == 0)
                anime.SetInteger("hAxisRaw", (int)h);
        }
        else
            anime.SetBool("IsWalk", false);


        //Player Direction
        if (Mathf.Abs(h) > Mathf.Abs(v))
            lastDirec = new Vector2(h, 0).normalized;

        else if (Mathf.Abs(v) > Mathf.Abs(h))
            lastDirec = new Vector2(0, v).normalized;
        else if ((Mathf.Abs(h) > 0 && Mathf.Abs(v) > 0) && Mathf.Abs(h) == Mathf.Abs(v))
            lastDirec = new Vector2(0, v).normalized;

        //animeInfo = anime.GetCurrentAnimatorStateInfo(0);
        //if (animeInfo.IsName("Player_Idle_Up") || animeInfo.IsName("Player_Idle_Down"))
    }

}
