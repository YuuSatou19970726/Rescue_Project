using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

namespace RescueProject
{
    public class GameController : MonoBehaviour
    {
        [Header("Refferences")]
        [SerializeField] private UIController uiController;

        // Start is called before the first frame update
        void Start()
        {
            this.InitialiseGame();
        }

        public void InitialiseGame()
        {
            uiController.Initialise();
            uiController.InitialisePages();
            UIController.ShowPage<UIGame>();
        }
    }
}