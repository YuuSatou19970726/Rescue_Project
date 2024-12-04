using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    [RequireComponent(typeof(MapGenerator))]
    public class EnvironmentListMap : EnvironmentMap
    {
        private static EnvironmentListMap instance;
        public static EnvironmentListMap Instance => instance;

        [SerializeField] private MapGenerator mapGenerator;

        private List<MapPhase> poolMapPhase = new List<MapPhase>();
        Vector3 mapPosition = new Vector3(50, 0, 0);

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
            for (int i = 0; i < mapPhases.Count; i++)
            {
                // MapPhase mapPhaseToCreate = mapPhases[i];
                MapPhase mapPhaseToCreate = this.GetMapFromPool(mapPhases[i]);

                if (i > 0)
                    mapPosition.z += mapPhaseToCreate.GetLenght() / 2;

                // MapPhase mapInstance = Instantiate(mapPhaseToCreate, mapPosition, Quaternion.identity, transform);
                // mapPosition.z += mapInstance.GetLenght() / 2;
                // mapInstance.gameObject.transform.parent = transform;

                mapPhaseToCreate.transform.position = mapPosition;
                mapPhaseToCreate.transform.rotation = Quaternion.identity;
                mapPosition.z += mapPhaseToCreate.GetLenght() / 2;
                mapPhaseToCreate.gameObject.transform.parent = transform;
                mapPhaseToCreate.gameObject.SetActive(true);
            }
        }

        public override void CreateMaps(int level)
        {
            mapPosition = new Vector3(50, 0, 0);
            List<MapPhase> mapPhases = this.mapGenerator.GetMapPhaseDataList().mapPhaseDatas[level].mapPhases;
            List<MapPhase> modifiedMapPhases = new List<MapPhase>(mapPhases);
            modifiedMapPhases.Insert(0, this.mapStart);
            modifiedMapPhases.Add(this.mapEnd);
            for (int i = 0; i < modifiedMapPhases.Count; i++)
            {
                // MapPhase mapPhaseToCreate = modifiedMapPhases[i];
                MapPhase mapPhaseToCreate = this.GetMapFromPool(modifiedMapPhases[i]);

                if (i > 0)
                    mapPosition.z += mapPhaseToCreate.GetLenght() / 2;

                // MapPhase mapInstance = Instantiate(mapPhaseToCreate, mapPosition, Quaternion.identity, transform);
                // mapPosition.z += mapInstance.GetLenght() / 2;
                // mapInstance.gameObject.transform.parent = transform;

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
    }
}