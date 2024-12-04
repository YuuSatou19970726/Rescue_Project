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

        protected override void LoopMaps()
        {
            // if (transform.position.z + halfLength < targetPlayer.transform.position.z - endOffset)
            // {
            //     transform.position = new Vector3(otherMap.position.x, otherMap.position.y, otherMap.position.z + halfLength * 2);
            // }
        }

        public override void CreateMaps(int level)
        {
            Vector3 mapPosition = new Vector3(50, 0, 0);
            List<MapPhase> mapPhases = this.mapGenerator.GetMapPhaseDataList().mapPhaseDatas[level].mapPhases;
            for (int i = 0; i < mapPhases.Count; i++)
            {
                MapPhase mapPhaseToCreate = mapPhases[i];

                if (i > 0)
                    mapPosition.z += mapPhaseToCreate.GetLenght() / 2;

                MapPhase mapInstance = Instantiate(mapPhaseToCreate, mapPosition, Quaternion.identity, transform);
                mapPosition.z += mapInstance.GetLenght() / 2;
                mapInstance.gameObject.transform.parent = transform;
            }
        }
    }
}