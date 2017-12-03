using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    // Movement
    [Header("Movement")]
    public float horizontalSpeed = 5;
    public float verticalForce = 5;

    [Header("Jump")]
    public float jumpForce = 1;
    public float downwardsGravityMultiplier = 1;
    public float airMovementMultiplier = 0.6f;
    public LayerMask groundDetection;

    [Header("Fire")]
    public Transform shootingGameObject;
    public Transform bulletsParent;
    public GameObject fireGameObject;

    // Internal variables
    private bool lookingRight = true;
    private bool OnGround = true;
    // Object references
    private Animator animator;
    private Rigidbody rb;

    // Use this for initialization
    void Start () {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
        // Check ground
        IsOnGround();

        // Get Input keys
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        bool jump = Input.GetButtonDown("Jump");
        bool fire = Input.GetButtonDown("Fire1");

        bool poison = Input.GetButtonDown("Poison");
        bool heal = Input.GetButtonDown("Heal");

        float localAirMovementMultiplier = 1f;
        if (!OnGround)
        {
            localAirMovementMultiplier = airMovementMultiplier;
            rb.AddForce(Vector3.down * 9.8f * downwardsGravityMultiplier);
        }

        // Character movement - Horizontal
        if (horizontal > 0.1)
        {
            transform.Translate(Vector3.right * horizontalSpeed * localAirMovementMultiplier * Time.deltaTime);

        }
        else if (horizontal < -0.1)
        {
            transform.Translate(-Vector3.right * horizontalSpeed * localAirMovementMultiplier * Time.deltaTime);
        }
        if (animator)
        {
            animator.SetFloat("speed", Mathf.Abs(horizontal));
        }

        // Flip logic
        if (!lookingRight && horizontal > 0)
        {
            Flip();
        }
        else if (lookingRight && horizontal < 0)
        {
            Flip();
        }

        // JUMP - POWER SKILL 1
        if (GameController.Instance.IsJumpEnabled())
        {
            // Jump logic
            if (OnGround && jump)
            {
                Vector3 jumpVector = Vector3.up + 0.3f * horizontal * Vector3.right;
                rb.AddForce(jumpVector.normalized * jumpForce, ForceMode.Impulse);
            }
        }

        // FLY - POWER SKILL 2
        if (GameController.Instance.IsFlyEnabled())
        {
            if (vertical > 0.1)
            {
                rb.AddForce(Vector3.up * verticalForce, ForceMode.Force);
            }
        }

        // SHOOT - POWER SKILL 3
        if (GameController.Instance.IsFireEnabled())
        {
            if (fire)
            {
                Fire();
            }
        }

        // Drink a poison
        if (poison)
        {
            GameController.Instance.Poison();
        }

        // Heal the player
        if (heal)
        {
            GameController.Instance.Heal();
        }

    }

    void Flip()
    {
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        lookingRight = !lookingRight;
    }

    void Fire()
    {
        GameObject shot = Instantiate(fireGameObject, shootingGameObject.position, transform.rotation, bulletsParent) as GameObject;
        shot.GetComponent<Shot>().SetDirection(lookingRight ? 1.0f : -1.0f);
    }

    void IsOnGround()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position - Vector3.up, 0.1f, groundDetection);
        if (colliders.Length > 0)
        {
            OnGround = true;
        } else
        {
            OnGround = false;
        }

    }

}
