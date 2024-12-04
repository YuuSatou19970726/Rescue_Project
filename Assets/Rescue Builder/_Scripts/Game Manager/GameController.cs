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

        public void InitialiseGame()
        {
            uiController.Initialise();
            uiController.InitialisePages();
            uiController.uIGame.gameObject.SetActive(true);
            UIController.ShowPage<UIGame>();
        }
    }
}