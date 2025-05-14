using TMPro;
using UnityEngine;

public class CoinsManager : MonoBehaviour
{
    public float coinsCount;
    public TextMeshProUGUI coinsCountText;

    void Update()
    {
        coinsCountText.text = coinsCount.ToString();
    }
}
