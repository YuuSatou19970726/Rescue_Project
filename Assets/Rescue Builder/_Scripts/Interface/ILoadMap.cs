using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public interface ILoadMap
    {
        void ImportData(int level);
        void ResetData();
    }
}