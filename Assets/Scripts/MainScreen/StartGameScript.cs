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


        [SerializeField] private bool isWindowOpen;

        private void Start()
        {
            mainCanvas.SetActive(false);
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

        public void Endgame(GameType gameType)
        {
            
        }
        
        public void EndGame()
        {
            mainCanvas.SetActive(false);
            paintingGame.SetActive(false);
            knittingGame.SetActive(false);
            flowerGame.SetActive(false);
            isWindowOpen = false;

            paintingButton.interactable = true;
            knittingButton.interactable = true;
            flowerButton.interactable = true;
        }

        public void StartPaintingGame()
        {
            StartGame(paintingGame);
        }

        public void StartKnittingGame()
        {
            StartGame(knittingGame);
        }

        public void StartFlowerGame()
        {
            StartGame(flowerGame);
        }
    }
}