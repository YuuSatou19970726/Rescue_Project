using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RescueProject
{
    public class UIMenu : MonoBehaviour
    {
        [SerializeField] Text textSpeed;
        [SerializeField] Text textMoney;
        [SerializeField] Text textSpeedUp;
        [SerializeField] Text textPriceSpeed;

        public void SetValue(float speed, float money, float price)
        {
            this.textMoney.text = $"{(float)Math.Round(money, 2)}$";
            this.textSpeed.text = $"{speed * 10}";
            this.textSpeedUp.text = $"{speed}";
            this.textPriceSpeed.text = $"{(float)Math.Round(price, 2)}";
        }

        public void EventButtonRace()
        {
            GameManager.Instance.SetMoveWithGameState(GameState.PLAYGAME_SCREEN);
            GameManager.Instance.SetGameState(GameState.LOADING_SCREEN);
        }
    }
}