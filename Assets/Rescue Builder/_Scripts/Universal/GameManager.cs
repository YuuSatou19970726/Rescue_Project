using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace RescueProject
{
    public class GameManager : CustomMonobehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

        [SerializeField] private PlayerSettings playerSettings;
        public PlayerSettings PlayerSettings => playerSettings;
        private float priceStamina = 17;
        private float priceSpeed = 9;
        private float priceIncome = 12;

        protected override void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);

            this.playerSettings = new PlayerSettings();
            this.LoadFromJson();

            if (this.playerSettings.stamina == 0)
            {
                this.playerSettings = new PlayerSettings
                {
                    stamina = 60,
                    speed = 1,
                    income = 3,
                    money = 50
                };
            }
            else
            {
                this.priceStamina = (int)Math.Round(this.playerSettings.stamina * 0.3f);
                this.priceSpeed = this.playerSettings.speed * 9;
                this.priceIncome = this.playerSettings.income * 4;
            }

        }

        protected override void OnDisable()
        {
            // this.SaveToJson();
        }

        public void IncreaseStamina()
        {
            this.playerSettings.stamina += 4;
            this.playerSettings.money -= this.priceStamina;
            this.priceStamina = (int)Math.Round(this.playerSettings.stamina * 0.3f);
        }

        public void IncreaseSpeed()
        {
            this.playerSettings.speed += 0.1f;
            this.playerSettings.money -= this.priceSpeed;
            this.priceSpeed = this.playerSettings.speed * 9;
        }

        public void IncreaseIncome()
        {
            this.playerSettings.income += 1;
            this.playerSettings.money -= this.priceIncome;
            this.priceIncome = this.playerSettings.income * 4;
        }

        public void IncreaseTapMoney()
        {
            this.playerSettings.money += 4 + this.playerSettings.income;
        }

        private void SaveToJson()
        {
            string playerSettingsData = JsonUtility.ToJson(this.playerSettings);
            string filePath = Application.persistentDataPath + JsonTags.PLAYER_SETTINGS_DATA;
            System.IO.File.WriteAllText(filePath, playerSettingsData);
        }

        private void LoadFromJson()
        {
            string filePath = Application.persistentDataPath + JsonTags.PLAYER_SETTINGS_DATA;

            if (!File.Exists(filePath)) return;

            string playerSettingsData = System.IO.File.ReadAllText(filePath);

            playerSettings = JsonUtility.FromJson<PlayerSettings>(playerSettingsData);
        }
    }
}