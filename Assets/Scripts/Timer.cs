using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public int startTime = 400;
    public float timeScale = 2f;
    public TextMeshProUGUI displayText;

    private float currentTime;

    //-----------------------------------------------------------------------------
    void Start()
    {
        currentTime = startTime;
    }

    //-----------------------------------------------------------------------------
    void Update()
    {
        currentTime -= Time.deltaTime * timeScale;
        int integerTime = (int)currentTime;
        displayText.text = $"{integerTime:D3}";

        if (integerTime == 0)
        {
            Debug.Log("You failed to complete the level in time");
        }
    }
}