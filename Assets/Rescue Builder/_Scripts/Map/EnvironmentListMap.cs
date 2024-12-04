using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class EnvironmentListMap : EnvironmentMap
    {
        private static EnvironmentListMap instance;
        public EnvironmentListMap Instance => instance;

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
        }

        protected override void LoopMaps()
        {
            // if (transform.position.z + halfLength < targetPlayer.transform.position.z - endOffset)
            // {
            //     transform.position = new Vector3(otherMap.position.x, otherMap.position.y, otherMap.position.z + halfLength * 2);
            // }
        }

        public override void CreateMaps()
        {
            // Vector3 mapPosition = Vector3.zero;
            // for (int i = 0; i < levelMaps.Length; i++)
            // {
            //     Map mapToCreate = _levelMaps[i];

            //     if (i > 0)
            //         mapPosition.z += mapToCreate.GetLenght() / 2;

            //     Map mapInstance = Instantiate(mapToCreate, mapPosition, Quaternion.identity, transform);
            //     mapPosition.z += mapInstance.GetLenght() / 2;
            // }
        }
    }
}