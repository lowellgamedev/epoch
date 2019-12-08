using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class player : MonoBehaviour {


    //Config
    public float CurrentHealth;
    public float MaxHealth;
    public float CurrentMana;
    public float MaxMana;
    [SerializeField] float runSpeed = 5;
    [SerializeField] float jumpSpeed = 5;
    //[SerializeField] float climbSpeed = 5;
    float scaleSize = 1;
    float StartingGravitySacle;
    public float dashSpeed;
    private float dashTime;
    public float startDashTime;
    private int direction;


    //Components
    Rigidbody2D myRidgidBody;
    Animator myAnimator;
    Collider2D myCollider2D;
    public Slider HealthBar;
    public Slider ManaBar;



    // Start is called before the first frame update
    void Start() {
        myRidgidBody = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        myCollider2D = GetComponent<Collider2D>();
        StartingGravitySacle = myRidgidBody.gravityScale;
        dashTime = startDashTime;
        MaxHealth = 100;
        CurrentHealth = MaxHealth;
    }

    // Update is called once per frame
    void Update() {
        Run();
        flipSprite();
        jump();
        sit();
        dash();
        takeDamage((float)0.5);
        UpdateUI();
    }


    //Set controlThrow to -1 or 1 depending on the horizontal input key (A and D), 
    //then set the player's velocity to a new Vector2D with the adjusted key input
    //If there's a horizontal key input, then set the "Running" animation to true.
    private void Run() {
        float controlThrow = CrossPlatformInputManager.GetAxis("Horizontal");
        Vector2 playerVelocity = new Vector2(controlThrow * runSpeed, myRidgidBody.velocity.y);
        myRidgidBody.velocity = playerVelocity;
        bool hasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;
        myAnimator.SetBool("Running", hasHorizontalSpeed);
    }

    //Flips the sprite if depending on which way it's facing
    private void flipSprite() {
        bool hasHorizontalSpeed = Mathf.Abs(myRidgidBody.velocity.x) > Mathf.Epsilon;

        if (hasHorizontalSpeed) {
            transform.localScale = new Vector2(Mathf.Sign(myRidgidBody.velocity.x) * scaleSize, scaleSize);
        }
    }

    private void jump() {
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

    private void sit() {
        if (myCollider2D.IsTouchingLayers(LayerMask.GetMask("Ground"))) {
            if (Input.GetKeyDown("c")) {
                myAnimator.SetBool("Sit", true);
            }
            if (Input.GetKeyUp("c")) {
                myAnimator.SetBool("Sit", false);
            }
        }
    }
    /*private void climb() {
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

    private void dash() {
        if (Input.GetKeyDown(KeyCode.LeftShift)) {
            myRidgidBody.velocity = Vector2.zero;
            if (dashTime <= 0) {
                myRidgidBody.velocity = Vector2.zero;
            } else {
                if (transform.localScale.x > 0) {
                    myRidgidBody.velocity = Vector2.right * dashSpeed;
                } else if (transform.localScale.x < 0){
                    myRidgidBody.velocity = Vector2.left * dashSpeed;
                }
            }
        }
    }

    public void takeDamage(float damage) {
        CurrentHealth -= damage;
        if (CurrentHealth <= 0) {
            //Die();
            SceneManager.LoadScene("Death");
        }
    }

    public void drainMana(float manaDrain) {
        CurrentMana -= manaDrain;
    }

    public void UpdateUI() {
        float Health = CurrentHealth / MaxHealth;
        float Mana = CurrentMana / MaxMana;
        HealthBar.value = Health;
        ManaBar.value = Mana;
    }

/*    public void Die() {
        myAnimator.SetBool("Dead", true);
    }*/
}
