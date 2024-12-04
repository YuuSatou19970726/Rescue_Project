using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RescueProject
{
    public class Tags
    {
        public static string PLAYER = "Player";
    }

    public class JsonTags
    {
        public static string PLAYER_SETTINGS_DATA = "PlayerSettingsData.json";
    }

    public class AnimationPlayerTags
    {
        public static string MOVEMENT_MULTIPLIER_FLOAT = "Movement Multiplier";
        public static string RUN_BOOL = "Run";

        public static string HOLDING_ANIMATION = "Holding";
    }

    public class ResourcesTags
    {
        public static string MAPS_MAP_START = "Maps/Map_Start";
        public static string MAPS_MAP_END = "Maps/Map_End";
        public static string MAPS_MAP_1 = "Maps/Map_1";
        public static string MAPS_MAP_2 = "Maps/Map_2";
        public static string MAPS_MAP_3 = "Maps/Map_3";

        public static string MAPS_MAP_PHASE_DATA_LIST = "Maps/MapPhaseDataList";
    }

    public class LinkCreateAssetTags
    {
        public static string LINK_CREATE_MAP = "Assets/Rescue Builder/Common/Prefabs/Maps/";
    }
}