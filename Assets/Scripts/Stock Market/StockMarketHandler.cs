using System.Globalization;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Stock_Market
{
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
        private float _resetTimer;
        private readonly bool[] _stockChanged = new bool[10];

        [Header("Multiplier settings")]
        [SerializeField] private float minMultiplier;
        [SerializeField] private float maxMultiplier;
    
        [Header("Sound effects")]
        private AudioSource _soundEffectSource;
    
        private void Start()
        {
            _soundEffectSource = GetComponent<AudioSource>();
            UpdateStocks();
            _resetTimer = timer;
        }
        // Update is called once per frame
        void Update()
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                timer = _resetTimer;
                UpdateStocks();
            }
        }

        private void UpdateStocks()
        {
            for (int i  = 0; i < stockMarketMultiplier.Length; i++)
            {
                stockMarketMultiplier[i].text = Random.Range(minMultiplier, maxMultiplier).ToString(CultureInfo.InvariantCulture);
                var multiplier = float.Parse(stockMarketSellValue[i].text) * float.Parse(stockMarketMultiplier[i].text);
                stockMarketSellValue[i].text = multiplier.ToString(CultureInfo.InvariantCulture);
                _stockChanged[i] = true;
                stockMarketSellValue[i].color = Color.green;
            }
        }

        public void BuyStockMarket(int index)
        {
            if (!stockMarketBuyInput[index].text.Equals("") && float.Parse(stockMarketBuyInput[index].text) <= float.Parse(totalMoneyText.text)) {
                var valueToBuy = float.Parse(stockMarketBuyInput[index].text);
                var subtraction = float.Parse(totalMoneyText.text) - valueToBuy;
                totalMoneyText.text = subtraction.ToString(CultureInfo.InvariantCulture);
                var addition = float.Parse(stockMarketSellValue[index].text) + valueToBuy;
                stockMarketSellValue[index].text = addition.ToString(CultureInfo.InvariantCulture);
                _stockChanged[index] = false;
                stockMarketSellValue[index].color = Color.red;
            }
        }

        public void SellStockMarket(int index)
        {
            if (!stockMarketSellValue[index].text.Equals("0") && !stockMarketBuyInput[index].text.Equals("") && _stockChanged[index])
            {
                var value = float.Parse(stockMarketSellValue[index].text);
                var result = value + float.Parse(totalMoneyText.text);
                totalMoneyText.text = Mathf.Round(result).ToString(CultureInfo.InvariantCulture);
                stockMarketSellValue[index].text = "0";
                _soundEffectSource.Play();
            }
        }
    }
}