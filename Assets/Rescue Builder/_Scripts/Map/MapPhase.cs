using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    [System.Serializable]
    public class MapPhase : MonoBehaviour
    {
        [Header("Setting")]
        [SerializeField] private Vector3 size;
        public float GetLenght => size.z;

        void OnDrawGizmos()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireCube(transform.position, size);
        }
    }
}