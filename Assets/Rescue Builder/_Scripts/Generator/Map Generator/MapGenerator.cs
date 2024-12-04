using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

namespace RescueProject
{
    public class MapGenerator : MonoBehaviour
    {
        [SerializeField] private MapPhaseDataList mapPhaseDataList;
        public MapPhaseDataList MapPhaseDataList => mapPhaseDataList;
        private Dictionary<string, MapPhaseData> mapPhaseDataDictionary = new Dictionary<string, MapPhaseData>();
        [SerializeField] private List<MapPhase> mapPhaseList;

        private int[][] generateMap = new int[10][];

        void Awake()
        {
            this.LoadMapInResources();
            this.LoadMapPhaseDataList();
            this.GenerateMapDefault();
        }

        void Reset()
        {
            this.LoadMapInResources();
            this.LoadMapPhaseDataList();
            this.GenerateMapDefault();
        }

        void Start()
        {
            this.GenerateMap();
        }

        void OnDisable()
        {
            StopAllCoroutines();
        }

        private void LoadMapInResources()
        {
            if (this.mapPhaseList.Count > 0) return;
            MapPhase map_1 = Resources.Load<MapPhase>(ResourcesTags.MAPS_MAP_1);
            MapPhase map_2 = Resources.Load<MapPhase>(ResourcesTags.MAPS_MAP_2);
            MapPhase map_3 = Resources.Load<MapPhase>(ResourcesTags.MAPS_MAP_3);

            mapPhaseList.Add(map_1);
            mapPhaseList.Add(map_2);
            mapPhaseList.Add(map_3);
        }


        private void LoadMapPhaseDataList()
        {
            if (this.mapPhaseDataList != null) return;
            this.mapPhaseDataList = Resources.Load<MapPhaseDataList>(ResourcesTags.MAPS_MAP_PHASE_DATA_LIST);
        }

        private void GenerateMapDefault()
        {
            if (this.mapPhaseDataList.mapPhaseDatas.Count > 0) return;

            MapPhaseData mapPhaseData;
            string currentMapName = "Map_0";

#if UNITY_EDITOR
            mapPhaseData = ScriptableObject.CreateInstance<MapPhaseData>();
            AssetDatabase.CreateAsset(mapPhaseData, LinkCreateAssetTags.LINK_CREATE_MAP + currentMapName + ".asset");
            AssetDatabase.SaveAssets();
#endif
            mapPhaseDataDictionary[currentMapName] = mapPhaseData;
            mapPhaseDataList.mapPhaseDatas.Add(mapPhaseData);

            mapPhaseData = mapPhaseDataDictionary[currentMapName];
            mapPhaseData.mapName = currentMapName;

            for (int i = 0; i < mapPhaseList.Count; i++)
            {
                mapPhaseData.mapPhases.Add(mapPhaseList[i]);
            }
        }

        private void GenerateMap()
        {
            if (this.mapPhaseDataList.mapPhaseDatas.Count > 10) return;

            int index = 0;
            for (int i = 0; i <= 1; i++)
            {
                for (int j = 0; j <= 2; j++)
                {
                    for (int k = 0; k <= 2; k++)
                    {
                        if (index < generateMap.Length)
                        {
                            if (i == 0 && j == 1 && k == 2) continue;
                            generateMap[index] = new int[] { i, j, k };
                            index++;
                        }
                    }
                }
            }

            StartCoroutine(GenerateAllMap());
        }

        private IEnumerator GenerateAllMap()
        {
            for (int i = 0; i < generateMap.Length; i++)
            {
                yield return GenerateSingleMapData(i);
                yield return null;
            }
        }

        private IEnumerator GenerateSingleMapData(int level)
        {
            MapPhaseData mapPhaseData;
            string currentMapName = "Map_" + (level + 1).ToString();
            if (!mapPhaseDataDictionary.ContainsKey(currentMapName))
            {
#if UNITY_EDITOR
                mapPhaseData = ScriptableObject.CreateInstance<MapPhaseData>();
                AssetDatabase.CreateAsset(mapPhaseData, LinkCreateAssetTags.LINK_CREATE_MAP + currentMapName + ".asset");
                AssetDatabase.SaveAssets();
#endif
                mapPhaseDataDictionary[currentMapName] = mapPhaseData;
                mapPhaseDataList.mapPhaseDatas.Add(mapPhaseData);
            }

            mapPhaseData = mapPhaseDataDictionary[currentMapName];
            mapPhaseData.mapName = currentMapName;

            for (int i = 0; i < generateMap[level].Length; i++)
            {
                int count = generateMap[level][i];
                mapPhaseData.mapPhases.Add(mapPhaseList[count]);
            }

            yield return null;
        }
    }
}