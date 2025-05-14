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

    public CoinsManager coinsManager;

    void Awake()
    {
        currentHealth = startingHealth;

        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        if (CompareTag("Player1"))
        {
            jumpAction = InputSystem.actions.FindAction("Jump1");
        }

        if (CompareTag("Player2"))
        {
            jumpAction = InputSystem.actions.FindAction("Jump2");
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Physics.gravity *= gravityMultiplier;
        isGameOver = false;
        playerAnim.SetFloat("Speed_f", 1.0f);
        jumpAction.Enable();
    }

    // Update is called once per frame
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
            isGameOver = true;
            playerAnim.SetBool("Death_b", true);
            playerAudio.PlayOneShot(deathSfx);
            dirt.Stop();
            menuUI.GameOver();
        }
    }
    public void AddCoins(float coinValue)
    {
        coinsManager.coinsCount++;
    }
    public void AddHeart(float heartValue)
    {
        currentHealth = Mathf.Clamp(currentHealth + heartValue, 0, startingHealth);
    }
}
