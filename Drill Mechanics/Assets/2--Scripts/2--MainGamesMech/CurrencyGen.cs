using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class CurrencyGen : MonoBehaviour
{
    DrillBase drillBase;
    [SerializeField] string drillTag;
    [SerializeField] float moneyPerSecondBase = 1f;  
    [SerializeField] float money;
    [SerializeField] Text moneyText;
    private void Start()
    {
        drillBase = GameObject.FindGameObjectWithTag(drillTag).GetComponent<DrillBase>();
    }

    private void Update()
    {
        if (drillBase != null)
        {
            if (drillBase.isDrillOn)
            {
                GenerateMoney();
                moneyText.text = money.ToString("0");
            }
        }
    }

    void GenerateMoney()
    {
        // Assuming drillBase has a speed variable (float) that controls the speed of the drill
        float drillSpeed = drillBase.rotationsPerMinute;

        // Generate money based on drill speed
        float moneyGenerated = moneyPerSecondBase * drillSpeed * Time.deltaTime;

        // Add the generated money to the total money
        money += moneyGenerated;

        // Optionally, display the current money for debugging
        Debug.Log("Money Generated: " + money);
    }
}
