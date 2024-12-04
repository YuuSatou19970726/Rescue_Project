using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    [CreateAssetMenu(fileName = "MapPhaseDataList", menuName = "ScriptableObject/Map/MapPhaseDataList")]
    [System.Serializable]
    public class MapPhaseDataList : ScriptableObject
    {
        public List<MapPhaseData> mapPhaseDatas = new List<MapPhaseData>();
    }
}