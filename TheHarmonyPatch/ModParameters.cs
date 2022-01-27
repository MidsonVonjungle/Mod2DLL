using System;
using System.Collections.Generic;
using UnityEngine;

namespace TheOrganizedSaberDLL.TheHarmonyPatch
{
    public static class ModParameters
    {
        public const string PackageId = "Therealtest2";
        public static string Path;
        public static readonly Dictionary<string, Sprite> ArtWorks = new Dictionary<string, Sprite>();

        public static readonly List<int> PersonalCardList = new List<int> { 11 };
        public static readonly List<int> EgoPersonalCardList = new List<int> {  };
        public static readonly List<int> UntransferablePassives = new List<int> { };
        public static readonly List<int> NoInventoryCardList = new List<int> {  };
        public static List<Tuple<string, List<int>, int>> OnlyCardKeywords = new List<Tuple<string, List<int>, int>>{ };

        public static readonly List<SkinNames> SkinParameters = new List<SkinNames>
        {
            new SkinNames
            {
                Name = "SaberSkin2",
                SkinParameters = new List<SkinParameters>
                {
                    new SkinParameters
                    {
                        PivotPosX = float.Parse("0"), PivotPosY = float.Parse("-193"),
                        Motion = ActionDetail.S1, FileName = "S1.png"
                    },
                    new SkinParameters
                    {
                        PivotPosX = float.Parse("-46"), PivotPosY = float.Parse("-239"),
                        Motion = ActionDetail.S2, FileName = "S2.png"
                    }
                }
            }
        };

        public static List<Tuple<string, List<int>, string>> SkinNameIds = new List<Tuple<string, List<int>, string>>
        {
            new Tuple<string, List<int>, string>("EGOSaber", new List<int> { 10000001, 4 }, "SaberSkin2")
        };
    }
}