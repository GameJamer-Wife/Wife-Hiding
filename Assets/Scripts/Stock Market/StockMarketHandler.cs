using TMPro;
using UnityEngine;

public class StockMarketHandler : MonoBehaviour
{
    [Header("Stock Market References")]
    [Tooltip("Point it to object named 'multiplier'.")]public TextMeshProUGUI[] stockMarketMultiplier;
    [Tooltip("Point it to object named 'Amount to buy'")]public TMP_InputField[] stockMarketBuyInput;
    [Tooltip("Point it to object named 'Value'")]public TextMeshProUGUI[] stockMarketSellValue;

    [Tooltip("Do not change this!")]public TextMeshProUGUI totalMoneyText;
    [Header("Timer")]
    public float timer = 3f;
    private float resetTimer;

    [Header("Multiplier settings")]
    public float minMultiplier;
    public float maxMultiplier;

    private void Start()
    {
        resetTimer = timer;
    }
    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            stockMarketMultiplier[0].text = Random.Range(minMultiplier, maxMultiplier).ToString();
            stockMarketMultiplier[1].text = Random.Range(minMultiplier, maxMultiplier).ToString();
            timer = resetTimer;
        }
    }

    public void BuyStockMarket(int index)
    {
        float valueToBuy = float.Parse(stockMarketBuyInput[index].text);
        if (valueToBuy <= float.Parse(totalMoneyText.text))
        {
            float subtraction = float.Parse(totalMoneyText.text) - valueToBuy;
            totalMoneyText.text = subtraction.ToString();
            float addition = float.Parse(stockMarketSellValue[index].text) + valueToBuy;
            stockMarketSellValue[index].text = addition.ToString();
        }
    }

    public void SellStockMarket(int index)
    {
        if (!stockMarketSellValue[index].text.Equals("0"))
        {
            float multi = float.Parse(stockMarketMultiplier[index].text);
            float value = float.Parse(stockMarketSellValue[index].text);
            float result = (value * multi) + float.Parse(totalMoneyText.text);
            totalMoneyText.text = result.ToString();
            stockMarketSellValue[index].text = "0";
        }
    }
}