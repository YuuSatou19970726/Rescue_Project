using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RescueProject
{
    public class UIMission : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI textTitle;
        [SerializeField] Text textCountCat;
        [SerializeField] Text textBonusPerCat;
        [SerializeField] Text textIncome;
        [SerializeField] Text textTotal;

        public void SetValue(string textMission, int countCat, int bonusPerCat, int income, int total)
        {
            textTitle.text = textMission;
            textCountCat.text = $"Cat: {countCat}";
            textBonusPerCat.text = $"Bonus Per Cat: {bonusPerCat}";
            textIncome.text = $"Income: {income}";
            textTotal.text = $"Total: {total}";
        }

        public void EventButtonLeave()
        {
            GameManager.Instance.SetMoveWithGameState(GameState.MENU_SCREEN);
            GameManager.Instance.SetGameState(GameState.LOADING_SCREEN);
        }
    }
}