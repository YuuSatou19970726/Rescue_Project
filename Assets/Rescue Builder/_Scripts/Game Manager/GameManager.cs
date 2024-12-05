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

        private List<ILoadMap> interfaceLoadMapDatas = new List<ILoadMap>();
        SpawnerCat spawnerCat;
        SpawnerObstacle spawnerObstacle;
        SpawnerPropsAndTimeline spawnerPropsAndTimeline;

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
        private GameState moveToGameState = GameState.NONE;

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
            this.LoadMapDataInterface();
        }

        protected override void Start()
        {
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
            this.gameController.InitialiseGame();
        }

        protected virtual void LoadMapDataInterface()
        {
            this.spawnerCat = FindAnyObjectByType<SpawnerCat>();
            this.spawnerObstacle = FindAnyObjectByType<SpawnerObstacle>();
            this.spawnerPropsAndTimeline = FindAnyObjectByType<SpawnerPropsAndTimeline>();

            this.interfaceLoadMapDatas.Add(spawnerCat);
            this.interfaceLoadMapDatas.Add(spawnerObstacle);
            this.interfaceLoadMapDatas.Add(spawnerPropsAndTimeline);
        }

        protected virtual void LoadMap()
        {
            moveToGameState = GameState.PLAYGAME_SCREEN;
            SetGameState(GameState.LOADING_SCREEN);
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

        public void LevelUp()
        {
            this.level++;
            SetGameState(GameState.LOADING_SCREEN);
            this.moveToGameState = GameState.MENU_SCREEN;
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

        public void SetGameState(GameState newGameState)
        {
            this.gameState = newGameState;

            switch (gameState)
            {
                case GameState.MENU_SCREEN:
                    gameController.ShowUIMenu();
                    break;
                case GameState.PLAYGAME_SCREEN:
                    gameController.ShowUIGame();
                    break;
                case GameState.LOADING_SCREEN:
                    gameController.ShowUILoading();
                    if (EnvironmentListMap.Instance.IsReadyCreateMap())
                        SetGameState(GameState.CREART_MAP);
                    break;
                case GameState.CREART_MAP:
                    if (this.moveToGameState == GameState.MENU_SCREEN)
                    {
                        EnvironmentListMap.Instance.CreateLoopMaps(level);
                    }

                    if (this.moveToGameState == GameState.PLAYGAME_SCREEN)
                    {
                        EnvironmentListMap.Instance.CreateMaps(level);
                        foreach (ILoadMap iLoadMap in this.interfaceLoadMapDatas)
                            iLoadMap.ImportData(level);
                    }

                    SetGameState(moveToGameState);
                    break;
                case GameState.MISSION_COMPLETED_SCREEN:
                    Time.timeScale = 0;
                    gameController.ShowUIMission();
                    break;
                case GameState.MISSION_FAILED_SCREEN:
                    Time.timeScale = 0;
                    gameController.ShowUIMission();
                    break;
            }
        }

        public void SetMoveWithGameState(GameState newGameState)
        {
            this.moveToGameState = newGameState;
        }
    }
}