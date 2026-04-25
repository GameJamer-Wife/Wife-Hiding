using TMPro;
using UnityEngine;

public class StockMarketHandler : MonoBehaviour
{
    [SerializeField][TextArea] protected string description;
    [Header("Stock Market References")]
    [Tooltip("Point it to object named 'multiplier'.")][SerializeField] private TextMeshProUGUI[] stockMarketMultiplier;
    [Tooltip("Point it to object named 'Amount to buy'")][SerializeField] private TMP_InputField[] stockMarketBuyInput;
    [Tooltip("Point it to object named 'Value'")][SerializeField] private TextMeshProUGUI[] stockMarketSellValue;

    [Tooltip("Do not change this!")][SerializeField] private TextMeshProUGUI totalMoneyText;
    [Header("Timer")]
    [SerializeField] private float timer = 3f;
    private float resetTimer;

    [Header("Multiplier settings")]
    [SerializeField] private float minMultiplier;
    [SerializeField] private float maxMultiplier;
    
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
            timer = resetTimer;
            UpdateStocks();
        }
    }

    public void UpdateStocks()
    {
        for (int i  = 0; i < stockMarketMultiplier.Length; i++)
        {
            stockMarketMultiplier[i].text = Random.Range(minMultiplier, maxMultiplier).ToString();
            float multiplier = float.Parse(stockMarketSellValue[i].text) * float.Parse(stockMarketMultiplier[i].text);
            stockMarketSellValue[i].text = multiplier.ToString();
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