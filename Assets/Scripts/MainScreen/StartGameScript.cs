using System;
using Stock_Market;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace MainScreen
{
    public enum GameType
    {
        Painting,
        Knitting,
        Flower
    }

    public class StartGameScript : MonoBehaviour
    {
        [SerializeField] private GameObject mainCanvas;


        [SerializeField] private GameObject paintingGame;
        [SerializeField] private GameObject knittingGame;
        [SerializeField] private GameObject flowerGame;

        [SerializeField] private Button paintingButton;
        [SerializeField] private Button knittingButton;
        [SerializeField] private Button flowerButton;
        [SerializeField] private Button ringButton;
        
        [SerializeField] private bool isWindowOpen;
        [SerializeField] private StocksManagerScript  stockManager;
        [SerializeField] private GameOverShowScript  gameOverManager;

        private void Start()
        {
            mainCanvas.SetActive(false);
            ringButton.interactable = false;
        }

        private void CheckIfWin()
        {
            if (paintingButton) 
                Debug.Log("Painting button is still active, not completed yet.");
            if  (knittingButton)
                Debug.Log("Knitting button is still active, not completed yet.");
            if (flowerButton)
                Debug.Log("Flower button is still active, not completed yet.");
            
            if (!(!paintingGame || !knittingGame || !flowerGame)) return;
            Debug.Log("All minigames completed! Check if the ring is bought...");
            if(!stockManager.theRingIsBought) return;
    
            Debug.Log("The ring is bought! You win!");
            
            // TODO: add game
        }

        private void StartGame(GameObject game)
        {
            if (isWindowOpen) return;

            mainCanvas.SetActive(true);
            game.SetActive(true);
            isWindowOpen = true;

            paintingButton.interactable = false;
            knittingButton.interactable = false;
            flowerButton.interactable = false;
        }

        public void EndAndDisableGame(GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Painting:
                    paintingGame.SetActive(false);
                    paintingButton.interactable = false;
                    
                    paintingGame = null;
                    paintingButton = null;
                    
                    break;
                case GameType.Knitting:
                    knittingGame.SetActive(false);
                    knittingButton.interactable = false;

                    knittingGame = null;
                    knittingButton = null;
                    break;
                case GameType.Flower:
                    flowerGame.SetActive(false);
                    flowerButton.interactable = false;

                    flowerGame = null;
                    flowerButton = null;
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(gameType), gameType, null);
            }

            isWindowOpen = false;
            mainCanvas.SetActive(false);
            CheckIfWin();
        }

        public void EndGame()
        {
            mainCanvas.SetActive(false);
            if (paintingGame) 
                paintingGame.SetActive(false);
            if (knittingGame)
                knittingGame.SetActive(false);
            if (flowerGame)
                flowerGame.SetActive(false);
            
            isWindowOpen = false;

            if (paintingButton)
                paintingButton.interactable = true;
            if (knittingButton)
                knittingButton.interactable = true;
            if (flowerButton)
                flowerButton.interactable = true;

            CheckIfWin();
        }

        public void StartPaintingGame()
        {
            if (paintingGame)
                StartGame(paintingGame);
        }

        public void StartKnittingGame()
        {
            if (knittingGame)
                StartGame(knittingGame);    
        }

        public void StartFlowerGame()
        {
            if (flowerGame)
                StartGame(flowerGame);
        }
        
    }
}