using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;

    public float gravityMultiplier;

    public bool isGameOver;

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

    private bool isSmallMode = false;
    private Vector3 normalScale;
    private Vector3 smallScale;
    private BoxCollider normalCollider;
    private CapsuleCollider smallCollider;

    public float normalFormJumpForce;
    public float smallFormJumpForce = 5f;

    private Coroutine upSideDownRoutine;
    private Quaternion rotation;

    private Coroutine smallFormRoutine;

    public GameObject upperPlatformPrefab;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    private Coroutine doublePlatformRoutine;
    void Awake()
    {
        
        normalFormJumpForce = jumpForce;
        currentHealth = startingHealth;

        rb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();

        normalCollider = GetComponent<BoxCollider>();
        smallCollider = GetComponent<CapsuleCollider>();
        smallCollider.enabled = false;

        normalScale = transform.localScale;
        smallScale = normalScale * 0.5f;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CompareTag("Player1") || CompareTag("Single"))
        {
            jumpAction = InputSystem.actions.FindAction("Jump1");
        }

        if (CompareTag("Player2"))
        {
            jumpAction = InputSystem.actions.FindAction("Jump2");
        }


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
            switch (gameObject.tag)
            {
                case "Player1":
                    menuUI.Player2Win();
                    break;
                case "Player2":
                    menuUI.Player1Win();
                    break;
                case "Single":
                    menuUI.GameOver();
                    break;
            }
            
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

    public void ChangeToSmallForm()
    {
        if (isSmallMode) return;
        isSmallMode = true;

        transform.localScale = smallScale;

        jumpForce = smallFormJumpForce;

        normalCollider.enabled = false;
        smallCollider.enabled = true;
    }

    public void RevertToNormalForm()
    {
        if (!isSmallMode) return;
        isSmallMode = false;

        transform.localScale = normalScale;

        jumpForce = normalFormJumpForce;

        normalCollider.enabled = true;
        smallCollider.enabled = false;
    }
    public void UpsideDown()
    {
        if (upSideDownRoutine != null)
        {
            StopCoroutine(upSideDownRoutine);
        }
        upSideDownRoutine = StartCoroutine(UpsideDownRoutine());
    }
    IEnumerator UpsideDownRoutine()
    {
        rotation = Camera.main.transform.rotation;
        Debug.Log(rotation);
        Camera.main.transform.rotation = Quaternion.Euler(8.676f, 0, 180);

        yield return new WaitForSeconds(10f);

        Camera.main.transform.rotation = rotation;
        Debug.Log(rotation);
    }

    public void SmallForm()
    {
        if (smallFormRoutine != null)
        {
            StopCoroutine(smallFormRoutine);
        }
        smallFormRoutine = StartCoroutine(SmallFormRoutine());
    }
    IEnumerator SmallFormRoutine()
    {
        ChangeToSmallForm();

        yield return new WaitForSeconds(10f);

        RevertToNormalForm();
    }

    public void DoublePlatform()
    {
        if (doublePlatformRoutine != null)
        {
            StopCoroutine(doublePlatformRoutine);
        }
        doublePlatformRoutine = StartCoroutine(DoublePlatformRoutine());
    }
    IEnumerator DoublePlatformRoutine()
    {
        GameObject[] spawnPoints = GameObject.FindGameObjectsWithTag("PlatformSpawnPoint");

        foreach (GameObject point in spawnPoints)
        {
            GameObject platform = Instantiate(upperPlatformPrefab, point.transform.position, Quaternion.identity);
            spawnedPlatforms.Add(platform);
        }

        yield return new WaitForSeconds(10f);

        foreach (GameObject platform in spawnedPlatforms)
        {
            Destroy(platform);
        }
        spawnedPlatforms.Clear();
    }


}
