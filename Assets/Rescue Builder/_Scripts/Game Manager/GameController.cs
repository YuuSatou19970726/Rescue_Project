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

        private void Start()
        {
            this.InitialiseGame();
        }

        private void InitialiseGame()
        {
            uiController.Initialise();
            uiController.InitialisePages();
        }

        public void ShowUIGame()
        {
            UIController.ShowPage<UIGame>();
        }
    }
}