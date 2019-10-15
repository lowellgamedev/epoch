using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class player : MonoBehaviour {


    //Config
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed = 5;
    //[SerializeField] float climbSpeed = 5;
    float scaleSize = 1;
    float StartingGravitySacle;

    //Components
    Rigidbody2D myRidgidBody;
    Animator myAnimator;
    Collider2D myCollider2D;


    // Start is called before the first frame update
    void Start() {
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        StartingGravitySacle = myRidgidBody.gravityScale;
    }

    // Update is called once per frame
    void Update() {
        Run();
        flipSprite();
        //Punch();
        fall();
        jump();

        //climb();
        //Debug.Log(myRidgidBody.velocity.x + "," + myRidgidBody.velocity.y);
        //Debug.Log(myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder")));

    }

    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRidgidBody.velocity.y);
        myRidgidBody.velocity = playerVelocity;
        bool hasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", hasHorizontalSpeed);
    }

    private void flipSprite() {
        bool hasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(myRidgidBody.velocity.x) * scaleSize, scaleSize);
        }
    }

    /*    private void Punch() {
            myAnimator.SetBool("Punch", Input.GetKeyDown(KeyCode.J));
        }*/

    private void jump() {
       // bool hasPosVelY = false;
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if (CrossPlatformInputManager.GetButtonDown("Jump")) {
                Vector2 jumpVelToAdd = new Vector2(0f, jumpSpeed);
                myRidgidBody.velocity += jumpVelToAdd;
                myAnimator.SetBool("Jumping", true);
            }
        }
        if (myRidgidBody.velocity.y <= 0) {
            myAnimator.SetBool("Jumping", false);
        }
        return;
    }

    private void fall() {
        if (myRidgidBody.velocity.y < 0f) {
            myAnimator.SetBool("Falling", true);
        } else if (myRidgidBody.velocity.y >= 0f) {
            myAnimator.SetBool("Falling", false);
        }
        return;
    }

    /*    private void climb() {
            if (!myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ladder"))) {
                myAnimator.SetBool("Climb", false);
                myRidgidBody.gravityScale = StartingGravitySacle;
                return;
            }

            float controlThrow = CrossPlatformInputManager.GetAxis("Vertical");
            Vector2 climbVelocity = new Vector2(myRidgidBody.velocity.x, controlThrow * climbSpeed);
            myRidgidBody.velocity = climbVelocity;
            myRidgidBody.gravityScale = 0f;

            myAnimator.SetBool("Climb", true);

        }*/

    /*    private void animatorControl() {


        }*/

    /*private void fall() {
        myAnimator.SetBool("Falling", myRidgidBody.velocity.y < Mathf.Epsilon);
        if (myRidgidBody.velocity.y == Mathf.Epsilon) {
            myAnimator.SetBool("Falling", false);
        }
    }*/
}
