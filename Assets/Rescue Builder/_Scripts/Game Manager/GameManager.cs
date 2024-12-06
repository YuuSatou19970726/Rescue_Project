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
        EnvironmentListMap environmentListMap;
        SpawnerCat spawnerCat;
        SpawnerObstacle spawnerObstacle;
        SpawnerPropsAndTimeline spawnerPropsAndTimeline;

        [Header("Game Setting")]
        private int level = 0;
        public int Level => level;

        [Header("Player Settings")]
        [SerializeField] private PlayerSettings playerSettings;
        public PlayerSettings PlayerSettings => playerSettings;
        private float priceStamina = 17;
        private float priceSpeed = 9;
        private float priceIncome = 12;

        [Header("Gameplay Setting")]
        private int countCat = 0;

        [SerializeField] private GameState gameState;
        public GameState GameState => gameState;
        private GameState moveToGameState = GameState.NONE;
        public GameState MoveToGameState => moveToGameState;

        [SerializeField] private PlayerController playerController;

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

        protected override void FixedUpdate()
        {
            if (gameState == GameState.PLAYGAME_SCREEN || gameState == GameState.TAP_MOD_SCREEN)
            {
                gameController.UpdateDataPlayer(this.playerController.Stamina, this.playerSettings.money, this.countCat);
            }
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

        protected virtual void LoadMapDataInterface()
        {
            this.environmentListMap = FindAnyObjectByType<EnvironmentListMap>();
            this.spawnerCat = FindAnyObjectByType<SpawnerCat>();
            this.spawnerObstacle = FindAnyObjectByType<SpawnerObstacle>();
            this.spawnerPropsAndTimeline = FindAnyObjectByType<SpawnerPropsAndTimeline>();

            this.interfaceLoadMapDatas.Add(environmentListMap);
            this.interfaceLoadMapDatas.Add(spawnerCat);
            this.interfaceLoadMapDatas.Add(spawnerObstacle);
            this.interfaceLoadMapDatas.Add(spawnerPropsAndTimeline);
        }

        protected virtual void LoadMap()
        {
            moveToGameState = GameState.MENU_SCREEN;
            SetGameState(GameState.LOADING_SCREEN);
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
                    this.gameController.SetValueUIMenu(this.playerSettings.speed, this.playerSettings.money, this.priceSpeed);
                    break;
                case GameState.PLAYGAME_SCREEN:
                    gameController.ShowUIGame();
                    gameController.SetActiveZombieZone();
                    break;
                case GameState.LOADING_SCREEN:
                    gameController.ShowUILoading();
                    gameController.SetHideZombieZone();

                    foreach (ILoadMap iLoadMap in this.interfaceLoadMapDatas)
                        iLoadMap.ResetData();

                    SetGameState(GameState.CREART_MAP);
                    break;
                case GameState.CREART_MAP:
                    this.playerController.transform.position = new Vector3(50, 0, 0);
                    foreach (ILoadMap iLoadMap in this.interfaceLoadMapDatas)
                        iLoadMap.ImportData(level);

                    SetGameState(moveToGameState);
                    break;
                case GameState.MISSION_COMPLETED_SCREEN:
                    this.level++;
                    int bonusCat = 10 * (level + 1);
                    int total = countCat * bonusCat * this.playerSettings.income;
                    this.playerSettings.money += total;
                    gameController.SetValueUIMission("MISSION COMPLETED", countCat, bonusCat, this.playerSettings.income, total);
                    gameController.ShowUIMission();
                    break;
                case GameState.MISSION_FAILED_SCREEN:
                    gameController.SetValueUIMission("MISSION FAILED", 0, 0, this.playerSettings.income, 0);
                    gameController.ShowUIMission();
                    break;
            }
        }

        public void SetMoveWithGameState(GameState newGameState)
        {
            this.moveToGameState = newGameState;
        }

        #region MENU GAME TASK
        public void IncreaseStamina()
        {
            this.playerSettings.stamina += 4;
            this.playerSettings.money -= this.priceStamina;
            this.priceStamina = (int)Math.Round(this.playerSettings.stamina * 0.3f);
        }

        public void IncreaseSpeed()
        {
            if (this.playerSettings.money < this.priceSpeed) return;
            this.playerSettings.speed += 0.1f;
            this.playerSettings.money -= this.priceSpeed;
            this.priceSpeed = this.playerSettings.speed * 9;

            this.gameController.SetValueUIMenu(this.playerSettings.speed, this.playerSettings.money, this.priceSpeed);
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
        #endregion

        #region GAME PLAY TASK
        public void IncreaseCat()
        {
            this.countCat++;
        }

        public void IncreaseMoney()
        {
            this.playerSettings.money += 5 + this.playerSettings.income;
        }
        #endregion
    }
}