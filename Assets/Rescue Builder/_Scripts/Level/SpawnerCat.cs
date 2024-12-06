using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class SpawnerCat : Spawn, ILoadMap
    {
        private static SpawnerCat instance;
        public static SpawnerCat Instance => instance;

        protected override void Awake()
        {
            if (instance == null) instance = this;
            else Destroy(gameObject);

            base.Awake();
        }

        public virtual void LoadAnimalCatSettingBegin()
        {
            List<Vector3> positionList = new List<Vector3>();
            positionList.Add(new Vector3(80, 0, 50));
            positionList.Add(new Vector3(20, 0, 90));
            positionList.Add(new Vector3(70, 0, 160));
            positionList.Add(new Vector3(30, 0, 240));
            positionList.Add(new Vector3(80, 0, 290));
            positionList.Add(new Vector3(60, 0, 370));

            for (int i = 0; i < positionList.Count; i++)
            {
                Transform newCat = SpawnObject(PrefabNameTags.ANIMAL_CAT_01, positionList[i]);
                newCat.gameObject.SetActive(true);
            }
        }

        public virtual void LoadAnimalCatSettingsByLevel(int level)
        {
            float zPos = EnvironmentListMap.Instance.ZPositionMapPhaseOne;
            List<Vector3> positionList = new List<Vector3>();
            for (int i = 0; i < 6 + level; i++)
                positionList.Add(new Vector3(Random.Range(10f, 95f), 0.1f, Random.Range(30f, zPos)));

            for (int i = 0; i < positionList.Count; i++)
            {
                int type = Random.Range(0, this.prefabs.Count - 1);
                Transform newCat = SpawnObject(this.prefabs[type].name, positionList[i]);
                newCat.gameObject.SetActive(true);
            }
        }

        public void ImportData(int level)
        {
            if (level == 0)
                LoadAnimalCatSettingBegin();
            else
                LoadAnimalCatSettingsByLevel(level);
        }

        public void ResetData()
        {
            Transform prefabObject = this.holder;
            foreach (Transform prefab in prefabObject)
                this.Despawn(prefab);
        }
    }
}