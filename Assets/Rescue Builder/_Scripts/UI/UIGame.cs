using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RescueProject
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] Text textStamina;
        [SerializeField] Text textMoney;
        [SerializeField] Text textCat;
        [SerializeField] TextMeshProUGUI tapToMove;

        void Update()
        {
            if (GameManager.Instance.GameState == GameState.TAP_MOD_SCREEN && !tapToMove.gameObject.activeInHierarchy)
                tapToMove.gameObject.SetActive(true);

            if (GameManager.Instance.GameState != GameState.TAP_MOD_SCREEN && tapToMove.gameObject.activeInHierarchy)
                tapToMove.gameObject.SetActive(false);
        }

        public void SetTextStamina(int stamina)
        {
            this.textStamina.text = $"Stamina: {stamina}";
        }

        public void SetTextMoney(float money)
        {
            this.textMoney.text = $"Money: {(float)Math.Round(money, 2)}";
        }

        public void SetTextCountCat(int countCat)
        {
            this.textCat.text = $"Cat: {countCat}";
        }
    }
}