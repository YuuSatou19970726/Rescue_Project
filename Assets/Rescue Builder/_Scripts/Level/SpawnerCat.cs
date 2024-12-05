using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class SpawnerCat : Spawn, ILoadMap
    {
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

        public void ImportData(int level)
        {
            if (level == 0)
                LoadAnimalCatSettingBegin();
        }
    }
}