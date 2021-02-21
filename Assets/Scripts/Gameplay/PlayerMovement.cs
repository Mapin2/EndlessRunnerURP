using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody2D playerRB = null;
    [SerializeField] Animator animator = null;
    PauseMenu pauseMenu = null;

    // Amount of force added when the player jumps.
    [SerializeField] float jumpForce = 3f;

    // Flag to enable jumping
    bool isOnGround = false;
    // Flag to enable double jumping 
    bool canDoubleJump = true;
    // Flags to jump and double jump
    bool jump = false;
    bool doubleJump = false;

    private void Awake()
    {
        pauseMenu = FindObjectOfType<PauseMenu>();
    }

    void Update()
    {
        if (GameManager.Instance.isGameActive && !pauseMenu.gameIsPaused)
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        if (jump)
        {
            // Using velocity to jump feels better than using addForce
            playerRB.velocity = Vector2.up * jumpForce;
            jump = false;
        }

        if (doubleJump)
        {
            playerRB.velocity = Vector2.up * jumpForce;
            doubleJump = false;
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            jump = true;
            isOnGround = false;
            animator.SetBool("IsJumping", true);
            AudioManager.Instance.Play("Jump");
        }
        else if (Input.GetKeyDown(KeyCode.Space) && !isOnGround && canDoubleJump)
        {
            doubleJump = true;
            canDoubleJump = false;
            animator.SetBool("IsDoubleJumping", true);
            AudioManager.Instance.Play("DoubleJump");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9)
        {
            // Reset flags to enable jump and double jump
            isOnGround = true;
            canDoubleJump = true;
            // Set animation params to false
            animator.SetBool("IsJumping", false);
            animator.SetBool("IsDoubleJumping", false);
        }
        else if (collision.gameObject.layer == 10)
        {
            // Endgame
            animator.SetTrigger("Hitted");
            AudioManager.Instance.Play("Hit");
            // Prevent for multiple collision/falling after dying
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().simulated = false;
            GameManager.Instance.EndGame();
        }
    }

    public void PlayerDeath()
    {
        AudioManager.Instance.Stop("GameTheme");
        AudioManager.Instance.Play("GameOver");
        gameObject.SetActive(false);
        pauseMenu.ShowGameOverMenu();
    }
}
