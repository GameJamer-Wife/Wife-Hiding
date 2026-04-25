using System;
using NUnit.Framework.Constraints;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

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
    private bool[] stockChanged = new bool[10];

    [Header("Multiplier settings")]
    [SerializeField] private float minMultiplier;
    [SerializeField] private float maxMultiplier;
    
    [Header("Sound effects")]
    private AudioSource soundEffectSource;
    
    private void Start()
    {
        soundEffectSource = GetComponent<AudioSource>();
        UpdateStocks();
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
            var multiplier = float.Parse(stockMarketSellValue[i].text) * float.Parse(stockMarketMultiplier[i].text);
            stockMarketSellValue[i].text = multiplier.ToString();
            stockChanged[i] = true;
            stockMarketSellValue[i].color = Color.green;
        }
    }

    public void BuyStockMarket(int index)
    {
        if (!stockMarketBuyInput[index].text.Equals("") && float.Parse(stockMarketBuyInput[index].text) <= float.Parse(totalMoneyText.text)) {
            var valueToBuy = float.Parse(stockMarketBuyInput[index].text);
            var subtraction = float.Parse(totalMoneyText.text) - valueToBuy;
            totalMoneyText.text = subtraction.ToString();
            var addition = float.Parse(stockMarketSellValue[index].text) + valueToBuy;
            stockMarketSellValue[index].text = addition.ToString();
            stockChanged[index] = false;
            stockMarketSellValue[index].color = Color.red;
        }
    }

    public void SellStockMarket(int index)
    {
        if (!stockMarketSellValue[index].text.Equals("0") && !stockMarketBuyInput[index].text.Equals("") && stockChanged[index])
        {
            var multi = float.Parse(stockMarketMultiplier[index].text);
            var value = float.Parse(stockMarketSellValue[index].text);
            var result = (value * multi) + float.Parse(totalMoneyText.text);
            totalMoneyText.text = result.ToString();
            stockMarketSellValue[index].text = "0";
            soundEffectSource.Play();
        }
    }
}