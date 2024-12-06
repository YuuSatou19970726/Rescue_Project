using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class SpawnerObstacle : Spawn, ILoadMap
    {
        public void ImportData(int level)
        {
            if (GameManager.Instance.MoveToGameState != GameState.PLAYGAME_SCREEN) return;
            float zPos = EnvironmentListMap.Instance.ZPositionMapPhaseOne;
            List<Vector3> positionList = new List<Vector3>();
            for (int i = 0; i < 7 + level; i++)
                positionList.Add(new Vector3(Random.Range(10f, 95f), 0.1f, Random.Range(30f, zPos)));

            for (int i = 0; i < positionList.Count; i++)
            {
                int type = Random.Range(0, this.prefabs.Count - 1);
                Transform newObstacle = SpawnObject(this.prefabs[type].name, positionList[i]);
                newObstacle.gameObject.SetActive(true);
            }
        }

        public void ResetData()
        {
            Transform prefabObject = this.holder;
            foreach (Transform prefab in prefabObject)
            {
                this.Despawn(prefab);
            }
        }
    }
}