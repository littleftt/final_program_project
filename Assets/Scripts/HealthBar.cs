using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public string playerTag = "Player1";
    private PlayerController playerHealth;
    public Image totalHealthBar;
    public Image currentHealthBar;

    private void Start()
    {
        GameObject playerObj = GameObject.FindWithTag(playerTag);
        playerHealth = playerObj.GetComponent<PlayerController>();
        totalHealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }

    private void Update()
    {
        currentHealthBar.fillAmount = playerHealth.currentHealth / 10f;
    }

}
