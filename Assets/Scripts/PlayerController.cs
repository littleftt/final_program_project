using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 10f;
    public float gravityMultiplier = 1f;

    public bool isGameOver = false;

    private Rigidbody rb;

    private InputAction jumpAction;

    private bool isOnGround = true;

    private Animator playerAnim;

    public AudioClip jumpSfx;
    public AudioClip deathSfx;
    public AudioClip healSfx;
    public AudioClip hitSfx;
    public AudioClip coinSfx;

    public AudioSource playerAudio;

    public ParticleSystem dirt;
    public ParticleSystem explosion;

    public MenuManager menuUI;

    public float startingHealth;
    public float currentHealth;
=======
    private bool isSmallMode = false;
    private Vector3 normalScale;
    private Vector3 smallScale;
    private BoxCollider normalCollider;
    private CapsuleCollider smallCollider;
>>>>>>> e0ddeaf0a0b215cf2d9d5cdacc4e904818b4ab5d

    public CoinsManager coinsManager;

    void Awake()
    {
        currentHealth = startingHealth;

        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
<<<<<<< HEAD

        if (CompareTag("Player1"))
        {
            jumpAction = InputSystem.actions.FindAction("Jump1");
        }

        if (CompareTag("Player2"))
        {
            jumpAction = InputSystem.actions.FindAction("Jump2");
        }
=======
        jumpAction = InputSystem.actions.FindAction("Jump");

        normalCollider = GetComponent<BoxCollider>();
        smallCollider = GetComponent<CapsuleCollider>();
        smallCollider.enabled = false;

        normalScale = transform.localScale;
        smallScale = normalScale * 0.5f;
>>>>>>> parent of 7ad0e3d (Revert "Merge branch 'aoom' of https://github.com/littleftt/final_program_project into aoom")
    }

    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        isGameOver = false;
        playerAnim.SetFloat("Speed_f", 1.0f);
        jumpAction.Enable();
    }

    void Update()
    {
        if ((jumpAction.triggered) && isOnGround && !isGameOver)
        {
            Jump();
        }
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        isOnGround = false;
        playerAnim.SetTrigger("Jump_trig");
        playerAudio.PlayOneShot(jumpSfx);
        dirt.Stop();
    }

    private void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirt.Play();
        }

        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(hitSfx);
            explosion.Play();
        }

        else if (collision.gameObject.CompareTag("healCollectible"))
        {
            playerAudio.PlayOneShot(healSfx);
        }

        else if (collision.gameObject.CompareTag("Coins"))
        {
            playerAudio.PlayOneShot(coinSfx);
        }
    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startingHealth);
        
        if(currentHealth == 0)
        {
            Debug.Log("Game Over");
=======
>>>>>>> e0ddeaf0a0b215cf2d9d5cdacc4e904818b4ab5d
            isGameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAudio.PlayOneShot(deathSfx);
            dirt.Stop();
            menuUI.GameOver();
        }
    }
<<<<<<< HEAD
    public void AddCoins(float coinValue)
    {
        coinsManager.coinsCount++;
    }
=======
<<<<<<< HEAD

<<<<<<< HEAD
>>>>>>> parent of 7ad0e3d (Revert "Merge branch 'aoom' of https://github.com/littleftt/final_program_project into aoom")
    public void AddHeart(float heartValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + heartValue, 0, startingHealth);
    }
}
=======
    public void ChangeToSmallForm()
    {
        if (isSmallMode) return;
        isSmallMode = true;

        transform.localScale = smallScale;

        normalCollider.enabled = false;
        smallCollider.enabled = true;
    }

    public void RevertToNormalForm()
    {
        if (!isSmallMode) return;
        isSmallMode = false;

        transform.localScale = normalScale;

        normalCollider.enabled = true;
        smallCollider.enabled = false;
    }
}
>>>>>>> e0ddeaf0a0b215cf2d9d5cdacc4e904818b4ab5d
