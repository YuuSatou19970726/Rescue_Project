using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Rendering;

namespace RescueProject
{
    public class GameManager : CustomMonobehaviour
    {
        private static GameManager instance;
        public static GameManager Instance => instance;

        private GameController gameController;

        [Header("Game Setting")]
        private int level = 0;

        [Header("Player Settings")]
        [SerializeField] private PlayerSettings playerSettings;
        public PlayerSettings PlayerSettings => playerSettings;
        private float priceStamina = 17;
        private float priceSpeed = 9;
        private float priceIncome = 12;

        [SerializeField] private GameState gameState;
        public GameState GameState => gameState;

        protected override void Awake()
        {
            base.Awake();
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

        protected override void LoadComponents()
        {
            this.LoadGameController();
            this.LoadMap();
        }

        protected override void OnDisable()
        {
            // this.SaveToJson();
        }

        protected virtual void LoadGameController()
        {
            if (this.gameController != null) return;
            this.gameController = GetComponent<GameController>();
        }

        protected virtual void LoadMap()
        {
            if (gameState == GameState.MENU_SCREEN)
                EnvironmentListMap.Instance.CreateLoopMaps(level);

            if (gameState == GameState.PLAYGAME_SCREEN)
            {
                EnvironmentListMap.Instance.CreateMaps(level);
                gameController.InitialiseGame();
            }
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

        public void SetGameState(GameState gameState)
        {
            this.gameState = gameState;

            if (this.gameState == GameState.MISSION_SCREEN)
                Time.timeScale = 0;
        }
    }
}