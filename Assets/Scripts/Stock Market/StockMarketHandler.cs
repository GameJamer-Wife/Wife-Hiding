using TMPro;
using UnityEngine;

public class StockMarketHandler : MonoBehaviour
{
    public TextMeshProUGUI[] stockMarketMultiplier;
    public TMP_InputField[] stockMarketBuyInput;
    public TextMeshProUGUI[] stockMarketSellValue;

    public TextMeshProUGUI totalMoneyText;
    public float timer = 3f;
    private float resetTimer;

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
        float multi = float.Parse(stockMarketMultiplier[index].text);
        float value = float.Parse(stockMarketSellValue[index].text);
        totalMoneyText.text = value * multi + value.ToString();
        stockMarketSellValue[index].text = "0";
    }
}
