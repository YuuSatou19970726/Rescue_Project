using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class SpawnerPropsAndTimeline : Spawn, ILoadMap
    {
        public void ImportData(int level)
        {
            float zPos = EnvironmentListMap.Instance.ZPositionMapPhaseOne;
            List<Vector3> positionList = new List<Vector3>();

            for (int i = 0; i < 4 + level; i++)
                positionList.Add(new Vector3(Random.Range(10f, 95f), 0.1f, Random.Range(30f, zPos)));

            for (int i = 0; i < positionList.Count; i++)
            {
                Transform newProps = this.SpawnObject(PrefabNameTags.MONEY, positionList[i]);
                newProps.gameObject.SetActive(true);
            }
        }
    }
}