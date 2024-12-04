using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    [CreateAssetMenu(fileName = "MapPhaseData", menuName = "ScriptableObject/Map/MapPhaseData")]
    [System.Serializable]
    public class MapPhaseData : ScriptableObject
    {
        public string mapName;
        public List<MapPhase> mapPhases = new List<MapPhase>();
    }
}