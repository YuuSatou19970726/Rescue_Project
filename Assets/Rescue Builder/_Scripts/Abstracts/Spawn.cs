using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public abstract class Spawn : CustomMonobehaviour
    {
        protected Transform holder;
        protected List<Transform> prefabs = new List<Transform>();
        protected List<Transform> poolObjs = new List<Transform>();

        protected override void LoadComponents()
        {
            this.LoadHolder();
            this.LoadPrefabs();
        }

        protected virtual void LoadHolder()
        {
            if (this.holder != null) return;
            this.holder = transform.Find(ObjectNameTags.HOLDER);
        }

        protected virtual void LoadPrefabs()
        {
            if (this.prefabs.Count > 0) return;
            Transform prefabObject = transform.Find(ObjectNameTags.PREFABS);
            foreach (Transform prefab in prefabObject)
                this.prefabs.Add(prefab);

            this.HidePrefabs();
        }

        private void HidePrefabs()
        {
            foreach (Transform prefab in this.prefabs)
                prefab.gameObject.SetActive(false);
        }

        protected Transform SpawnObject(string prefabName, Vector3 spawnPos, Quaternion? rotation = null)
        {
            if (rotation == null) rotation = Quaternion.identity;

            Transform prefab = this.GetPrefabByName(prefabName);
            if (prefab == null) return null;

            Transform newPrefab = this.GetObjectFromPool(prefab);
            newPrefab.SetPositionAndRotation(spawnPos, (Quaternion)rotation);

            newPrefab.parent = this.holder;
            return newPrefab;
        }

        private Transform GetPrefabByName(string prefabName)
        {
            foreach (Transform prefab in this.prefabs)
                if (prefab.name == prefabName) return prefab;

            return null;
        }

        private Transform GetObjectFromPool(Transform prefab)
        {
            foreach (Transform poolObj in this.poolObjs)
            {
                if (poolObj.name == prefab.name)
                {
                    this.poolObjs.Remove(poolObj);
                    return poolObj;
                }
            }

            Transform newPrefab = Instantiate(prefab);
            newPrefab.name = prefab.name;

            return newPrefab;
        }

        public void Despawn(Transform obj)
        {
            this.poolObjs.Add(obj);
            obj.gameObject.SetActive(false);
        }
    }
}