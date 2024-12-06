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
        public static string LINK_CREATE_MAP_LIST = "Assets/Rescue Builder/Resources/Maps/";
    }

    public class ObjectNameTags
    {
        public static string HOLDER = "Holder";
        public static string PREFABS = "Prefabs";
    }

    public class PrefabNameTags
    {
        //Animals
        public static string ANIMAL_CAT_01 = "Animal Cat 01";
        public static string ANIMAL_CAT_02 = "Animal Cat 02";
        public static string ANIMAL_CAT_03 = "Animal Cat 03";
        public static string ANIMAL_CAT_04 = "Animal Cat 04";
        public static string ANIMAL_CAT_05 = "Animal Cat 05";

        //Obstacles
        public static string SANDBAGS = "Sandbags";
        public static string ROCKS = "Rocks";
        public static string BURNING_TIRES = "BurningTires";
        public static string BARRELS = "Barrels";

        //Props
        public static string MONEY = "Money";

        //Zombies
        public static string SIMPLE_ZOMBIES_01_BLUE = "SimpleZombies_01_Blue";
        public static string SIMPLE_ZOMBIES_03_GREY = "SimpleZombies_03_Grey";
        public static string SIMPLE_ZOMBIES_BUSINESSMAN_GREEN = "SimpleZombies_BusinessMan_Green";
        public static string SIMPLE_ZOMBIES_NAZI_GREY = "SimpleZombies_Nazi_Grey";
        public static string SIMPLE_ZOMBIES_POLICE_BLUE = "SimpleZombies_Police_Blue";
        public static string SIMPLE_ZOMBIES_PUNK_BLUE = "SimpleZombies_Punk_Blue";
        public static string SIMPLE_ZOMBIES_RIOT_COP_GREEN = "SimpleZombies_RiotCop_Green";
        public static string SIMPLE_ZOMBIES_SHERIF_GREY = "SimpleZombies_Sherif_Grey";
    }

    public class PlayerPrefsTags
    {
        public static string level = "level";
    }
}