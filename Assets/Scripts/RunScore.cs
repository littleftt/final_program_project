using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class RunScore : MonoBehaviour
{
    public float runningScore = 0;
    public TextMeshProUGUI runningScoreText;

    private float timer = 0f;
    public float scoreUpdateInterval;

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;

        if (timer >= scoreUpdateInterval)
        {
            timer = 0f;
            runningScore++;
            runningScoreText.text = runningScore.ToString() + " M";
        }
    }

}
