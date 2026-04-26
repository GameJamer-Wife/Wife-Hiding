using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Stock_Market
{
    public class StocksManagerScript : MonoBehaviour
    {
        [SerializeField] private GameObject stockMarket;
        [SerializeField] private StockMarketHandler stockLogic;
    
        [SerializeField] private int Money { get; set;}
        [SerializeField] private TMPro.TextMeshProUGUI moneyText;
    
        [SerializeField] private int moneyNeeded;
        [SerializeField] private Button buyRingButton;
        [SerializeField] private TMPro.TextMeshProUGUI ringText;
        
        
        public bool theRingIsBought =  false;

        public void Start()
        {
            Money = stockLogic.StockMarketTransferFrom();
            moneyText.text = $"{Money}$";
            ringText.text = $"ONLY {moneyNeeded}";
        }
    
        public void OpenStockMarket()
        {
            stockLogic.StockMarketTransferTo(Money);
            stockMarket.SetActive(true);
        }
    
        public void CloseStockMarket()
        {
            Money = stockLogic.StockMarketTransferFrom();
            moneyText.text = $"{Money}$";
            stockMarket.SetActive(false);
        
            if (Money >= moneyNeeded)
            {
                buyRingButton.interactable = true;
            }
        }

        public void BuyRing()
        {
            if(Money < moneyNeeded) return;
        
            Money = Money - moneyNeeded;
            moneyText.text = $"{Money}$";
            ringText.text = "IT'S YOURS!";
            buyRingButton.interactable = false;
            theRingIsBought = true;
            
            Debug.Log("The ring is bought!");
        }
    
    }
}
