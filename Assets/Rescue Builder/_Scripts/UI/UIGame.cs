using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RescueProject
{
    public class UIGame : MonoBehaviour
    {
        [SerializeField] Text textStamina;
        [SerializeField] Text textMoney;
        [SerializeField] Text textCat;

        public void SetTextStamina(int stamina)
        {
            this.textStamina.text = $"Stamina: {stamina}";
        }

        public void SetTextMoney(float money)
        {
            this.textMoney.text = $"Money: {money}";
        }

        public void SetTextCountCat(int countCat)
        {
            this.textCat.text = $"Cat: {countCat}";
        }
    }
}