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
        public MapPhaseDataList GetMapPhaseDataList() => mapPhaseDataList;
        private Dictionary<string, MapPhaseData> mapPhaseDataDictionary = new Dictionary<string, MapPhaseData>();
        [SerializeField] private List<MapPhase> mapPhaseList;

        private MapPhaseDataList mapPhaseDataListTemp;

        private int[][] generateMap = new int[10][];

        void Awake()
        {
            this.LoadMapInResources();
            this.LoadMapPhaseDataList();
        }

        void Reset()
        {
            this.LoadMapInResources();
#if UNITY_EDITOR
            this.GenerateMap();
#endif
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

#if UNITY_EDITOR
        void OnDisable()
        {
            StopAllCoroutines();
        }

        private void GenerateMapDefault()
        {
            MapPhaseData mapPhaseData;

            string currentMapName = "Map_0";
            mapPhaseDataListTemp = ScriptableObject.CreateInstance<MapPhaseDataList>();
            mapPhaseData = ScriptableObject.CreateInstance<MapPhaseData>();
            mapPhaseDataDictionary[currentMapName] = mapPhaseData;
            mapPhaseDataListTemp.mapPhaseDatas.Add(mapPhaseData);

            mapPhaseData = mapPhaseDataDictionary[currentMapName];
            mapPhaseData.mapName = currentMapName;

            for (int i = 0; i < mapPhaseList.Count; i++)
            {
                mapPhaseData.mapPhases.Add(mapPhaseList[i]);
            }
            AssetDatabase.CreateAsset(mapPhaseData, LinkCreateAssetTags.LINK_CREATE_MAP + currentMapName + ".asset");
            AssetDatabase.SaveAssetIfDirty(mapPhaseData);
        }

        private void GenerateMap()
        {
            this.GenerateMapDefault();

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

            AssetDatabase.CreateAsset(mapPhaseDataListTemp, LinkCreateAssetTags.LINK_CREATE_MAP_LIST + "MapPhaseDataList.asset");
            AssetDatabase.SaveAssetIfDirty(mapPhaseDataListTemp);
            Debug.Log("Generate Map Done");
        }

        private IEnumerator GenerateSingleMapData(int level)
        {
            MapPhaseData mapPhaseData;
            string currentMapName = "Map_" + (level + 1).ToString();
            if (!mapPhaseDataDictionary.ContainsKey(currentMapName))
            {
                mapPhaseData = ScriptableObject.CreateInstance<MapPhaseData>();
                mapPhaseDataDictionary[currentMapName] = mapPhaseData;
                mapPhaseDataListTemp.mapPhaseDatas.Add(mapPhaseData);
                mapPhaseData = mapPhaseDataDictionary[currentMapName];
                mapPhaseData.mapName = currentMapName;

                for (int i = 0; i < generateMap[level].Length; i++)
                {
                    int count = generateMap[level][i];
                    mapPhaseData.mapPhases.Add(mapPhaseList[count]);
                }
                AssetDatabase.CreateAsset(mapPhaseData, LinkCreateAssetTags.LINK_CREATE_MAP + currentMapName + ".asset");
                AssetDatabase.SaveAssetIfDirty(mapPhaseData);
            }
            yield return null;
        }
#endif
    }
}