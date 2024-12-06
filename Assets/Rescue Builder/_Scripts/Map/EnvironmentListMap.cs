using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

namespace RescueProject
{
    [RequireComponent(typeof(MapGenerator))]
    public class EnvironmentListMap : EnvironmentMap, ILoadMap
    {
        private static EnvironmentListMap instance;
        public static EnvironmentListMap Instance => instance;

        [SerializeField] private MapGenerator mapGenerator;

        private List<MapPhase> poolMapPhase = new List<MapPhase>();
        Vector3 mapPosition = new Vector3(50, 0, 0);

        private int countMapPhase = 0;

        private float zPositionMapPhaseOne;
        public float ZPositionMapPhaseOne => zPositionMapPhaseOne;

        [SerializeField] private GameObject changeGameMod;

        protected override void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        protected override void LoadComponents()
        {
            base.LoadComponents();
            this.LoadMapGenerator();
        }

        protected virtual void LoadMapGenerator()
        {
            if (this.mapGenerator != null) return;
            this.mapGenerator = GetComponent<MapGenerator>();
        }

        public override void LoopMaps()
        {
            if (this.poolMapPhase.Count > 0)
            {
                MapPhase mapPhaseToCreate = this.poolMapPhase[0];
                this.poolMapPhase.Remove(mapPhaseToCreate);

                mapPosition.z += mapPhaseToCreate.GetLenght() / 2;
                mapPhaseToCreate.transform.position = mapPosition;
                mapPhaseToCreate.transform.rotation = Quaternion.identity;
                mapPosition.z += mapPhaseToCreate.GetLenght() / 2;
                mapPhaseToCreate.gameObject.transform.parent = transform;
                mapPhaseToCreate.gameObject.SetActive(true);
            }
        }

        public override void CreateLoopMaps(int level)
        {
            mapPosition = new Vector3(50, 0, 0);
            List<MapPhase> mapPhases = this.mapGenerator.GetMapPhaseDataList().mapPhaseDatas[level].mapPhases;
            this.countMapPhase = mapPhases.Count;
            for (int i = 0; i < mapPhases.Count; i++)
            {
                MapPhase mapPhaseToCreate = this.GetMapFromPool(mapPhases[i]);

                if (i > 0)
                    mapPosition.z += mapPhaseToCreate.GetLenght() / 2;

                string original = mapPhaseToCreate.transform.name;
                string cleaned = Regex.Replace(original, @"\(Clone\)", "");
                mapPhaseToCreate.transform.name = cleaned;

                mapPhaseToCreate.transform.position = mapPosition;
                mapPhaseToCreate.transform.rotation = Quaternion.identity;
                mapPosition.z += mapPhaseToCreate.GetLenght() / 2;
                mapPhaseToCreate.gameObject.transform.parent = transform;
                mapPhaseToCreate.gameObject.SetActive(true);
            }
        }

        public override void CreateMaps(int level)
        {
            mapPosition = new Vector3(50, 0, -100);
            List<MapPhase> mapPhases = this.mapGenerator.GetMapPhaseDataList().mapPhaseDatas[level].mapPhases;
            this.countMapPhase = mapPhases.Count;
            this.zPositionMapPhaseOne = mapPhases[0].GetLenght();
            this.changeGameMod.transform.position = new Vector3(0, 0, this.zPositionMapPhaseOne);
            this.changeGameMod.gameObject.SetActive(true);

            List<MapPhase> modifiedMapPhases = new List<MapPhase>(mapPhases);
            modifiedMapPhases.Insert(0, this.mapStart);
            modifiedMapPhases.Add(this.mapEnd);
            for (int i = 0; i < modifiedMapPhases.Count; i++)
            {
                MapPhase mapPhaseToCreate = this.GetMapFromPool(modifiedMapPhases[i]);

                if (i > 0)
                    mapPosition.z += mapPhaseToCreate.GetLenght() / 2;

                string original = mapPhaseToCreate.transform.name;
                string cleaned = Regex.Replace(original, @"\(Clone\)", "");
                mapPhaseToCreate.transform.name = cleaned;

                mapPhaseToCreate.transform.position = mapPosition;
                mapPhaseToCreate.transform.rotation = Quaternion.identity;
                mapPosition.z += mapPhaseToCreate.GetLenght() / 2;
                mapPhaseToCreate.gameObject.transform.parent = transform;
                mapPhaseToCreate.gameObject.SetActive(true);
            }
        }

        private MapPhase GetMapFromPool(MapPhase mapPhase)
        {
            foreach (MapPhase poolMap in this.poolMapPhase)
            {
                if (poolMap.transform.name == mapPhase.transform.name)
                {
                    this.poolMapPhase.Remove(poolMap);
                    return poolMap;
                }
            }

            MapPhase mapInstance = Instantiate(mapPhase);
            return mapInstance;
        }

        public void Despawn(MapPhase mapPhase)
        {
            this.poolMapPhase.Add(mapPhase);
            mapPhase.gameObject.SetActive(false);
        }

        public bool IsReadyCreateMap()
        {
            return this.poolMapPhase.Count == this.countMapPhase;
        }

        public void ImportData(int level)
        {
            if (GameManager.Instance.MoveToGameState == GameState.MENU_SCREEN)
                this.CreateLoopMaps(level);
            if (GameManager.Instance.MoveToGameState == GameState.PLAYGAME_SCREEN)
                this.CreateMaps(level);
        }

        public void ResetData()
        {
            foreach (Transform child in transform)
            {
                MapPhase prefab = child.GetComponent<MapPhase>();
                if (prefab != null)
                {
                    this.Despawn(prefab);
                }
            }
        }
    }
}