using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    private Rigidbody2D rb;
    [SerializeField] private float speed = 10;
    [SerializeField] private float jumpSpeed = 10;

    Animator animator;
    [SerializeField] private int groundLayer = 9;
    [SerializeField] private int objectLayer = 7;
    [SerializeField] private int exitLayer = 10;

    private bool isGrounded = false;

    void Awake(){
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }
    
    void Update(){
        if (Input.GetKeyDown (KeyCode.Space) && isGrounded){
                Jumping();
        }
    }

    void FixedUpdate()
    {
        if(rb != null){
            GetMoving();
        } else{
            Debug.LogWarning("Rigidbody ist not attached to " + gameObject.name);
        }
    }

    private void GetMoving(){
        float x = Input.GetAxis("Horizontal") * speed;

        Vector2 force = new Vector2(x,0);
        rb.AddForce(force);

        if(animator != null){
            if(Input.GetAxis("Horizontal") > 0){
                animator.SetBool("IsWalking", true);
            }
            else{
                animator.SetBool("IsWalking", false);
            }
        } else{
            Debug.LogWarning("Animator ist not attached to " + gameObject.name);
        }
    }

    private void Jumping(){
            rb.AddForce(new Vector3(0, jumpSpeed, 0), ForceMode2D.Impulse);
    }

    void OnCollisionEnter2D(Collision2D collision)
     {
         if ((collision.gameObject.layer == groundLayer || collision.gameObject.layer == objectLayer) && !isGrounded){
             isGrounded = true;
         }
     }
     void OnCollisionExit2D(Collision2D collision)
     {
         if ((collision.gameObject.layer == groundLayer || collision.gameObject.layer == objectLayer) && isGrounded){
             isGrounded = false;
         }
     }

     void OnTriggerEnter2D(Collider2D collider){
         if(collider.gameObject.layer == exitLayer)
         {
             Gamemanager.Instance.HasWon();
         }
     }
}
