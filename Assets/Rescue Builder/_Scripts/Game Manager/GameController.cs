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

        public void ShowUIGame()
        {
            uiController.uIGame.gameObject.SetActive(true);
            uiController.uILoading.gameObject.SetActive(false);
            uiController.uIMenu.gameObject.SetActive(false);
            uiController.uIMission.gameObject.SetActive(false);
        }

        public void ShowUILoading()
        {
            uiController.uILoading.gameObject.SetActive(true);
            uiController.uIGame.gameObject.SetActive(false);
            uiController.uIMenu.gameObject.SetActive(false);
            uiController.uIMission.gameObject.SetActive(false);
        }

        public void ShowUIMenu()
        {
            uiController.uIMenu.gameObject.SetActive(true);
            uiController.uIGame.gameObject.SetActive(false);
            uiController.uILoading.gameObject.SetActive(false);
            uiController.uIMission.gameObject.SetActive(false);
        }

        public void ShowUIMission()
        {
            uiController.uIMission.gameObject.SetActive(true);
            uiController.uIGame.gameObject.SetActive(false);
            uiController.uILoading.gameObject.SetActive(false);
            uiController.uIMenu.gameObject.SetActive(false);
        }

        public void UpdateDataPlayer(int stamina, float money, int countCat)
        {
            uiController.uIGame.SetTextStamina(stamina);
            uiController.uIGame.SetTextMoney(money);
            uiController.uIGame.SetTextCountCat(countCat);
        }
    }
}