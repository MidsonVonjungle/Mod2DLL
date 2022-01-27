using System;
using LOR_DiceSystem;
using System.Collections.Generic;
using System.Linq;
using UI;
using HarmonyLib;

namespace TheOrganizedSaberDLL.TheHarmonyPatch
{
    public static class UnitUtil
    {
        public static void RefreshCombatUI(bool forceReturn = false)
        {
            foreach (var (battleUnit, num) in BattleObjectManager.instance.GetList()
                         .Select((value, i) => (value, i)))
            {
                SingletonBehavior<UICharacterRenderer>.Instance.SetCharacter(battleUnit.UnitData.unitData, num, true);
                if (forceReturn)
                    battleUnit.moveDetail.ReturnToFormationByBlink(true);
            }

            BattleObjectManager.instance.InitUI();
        }

        
        
        //Everthing below here is only for card related coding, proceed at your own risk
        private static void SetBaseKeywordCard(LorId id, ref Dictionary<LorId, DiceCardXmlInfo> cardDictionary,
           ref List<DiceCardXmlInfo> cardXmlList)
        {
            var keywordsList = GetKeywordsList(id.id).ToList();
            var diceCardXmlInfo2 = CardOptionChange(cardDictionary[id], new List<CardOption>(), true, keywordsList);
            cardDictionary[id] = diceCardXmlInfo2;
            cardXmlList.Add(diceCardXmlInfo2);
        }

        private static void SetCustomCardOption(CardOption option, LorId id, bool keywordsRequired,
            ref Dictionary<LorId, DiceCardXmlInfo> cardDictionary, ref List<DiceCardXmlInfo> cardXmlList)
        {
            var keywordsList = new List<string>();
            if (keywordsRequired) keywordsList = GetKeywordsList(id.id).ToList();
            var diceCardXmlInfo2 = CardOptionChange(cardDictionary[id], new List<CardOption> { option },
                keywordsRequired,
                keywordsList);
            cardDictionary[id] = diceCardXmlInfo2;
            cardXmlList.Add(diceCardXmlInfo2);
        }

        private static List<int> GetAllOnlyCardsId()
        {
            var onlyPageCardList = new List<int>();
            foreach (var cardIds in ModParameters.OnlyCardKeywords.Select(x => x.Item2))
                onlyPageCardList.AddRange(cardIds);
            return onlyPageCardList;
        }

        private static IEnumerable<string> GetKeywordsList(int id)
        {
            var keyword = ModParameters.OnlyCardKeywords.FirstOrDefault(x => x.Item2.Contains(id))?.Item1;
            return string.IsNullOrEmpty(keyword)
                ? new List<string> { "LoRModPage_Re21341" }
                : new List<string> { "LoRModPage_Re21341", keyword };
        }

        private static DiceCardXmlInfo CardOptionChange(DiceCardXmlInfo cardXml, List<CardOption> option,
            bool keywordRequired, List<string> keywords,
            string skinName = "", string mapName = "", int skinHeight = 0)
        {
            return new DiceCardXmlInfo(cardXml.id)
            {
                workshopID = cardXml.workshopID,
                workshopName = cardXml.workshopName,
                Artwork = cardXml.Artwork,
                Chapter = cardXml.Chapter,
                category = cardXml.category,
                DiceBehaviourList = cardXml.DiceBehaviourList,
                _textId = cardXml._textId,
                optionList = option.Any() ? option : cardXml.optionList,
                Priority = cardXml.Priority,
                Rarity = cardXml.Rarity,
                Script = cardXml.Script,
                ScriptDesc = cardXml.ScriptDesc,
                Spec = cardXml.Spec,
                SpecialEffect = cardXml.SpecialEffect,
                SkinChange = string.IsNullOrEmpty(skinName) ? cardXml.SkinChange : skinName,
                SkinChangeType = cardXml.SkinChangeType,
                SkinHeight = skinHeight != 0 ? skinHeight : cardXml.SkinHeight,
                MapChange = string.IsNullOrEmpty(mapName) ? cardXml.MapChange : mapName,
                PriorityScript = cardXml.PriorityScript,
                Keywords = keywordRequired ? keywords : cardXml.Keywords
            };
        }
        public static void ChangeCardItem(ItemXmlDataList instance)
        {
            Dictionary<LorId, DiceCardXmlInfo> source = (Dictionary<LorId, DiceCardXmlInfo>)instance.GetType().GetField("_cardInfoTable", AccessTools.all).GetValue(instance);
            List<DiceCardXmlInfo> list = (List<DiceCardXmlInfo>)instance.GetType().GetField("_cardInfoList", AccessTools.all).GetValue(instance);
            List<int> allOnlyCardsId = UnitUtil.GetAllOnlyCardsId();
            foreach (KeyValuePair<LorId, DiceCardXmlInfo> keyValuePair in (from x in source
                                                                           where x.Key.packageId == "SaeModSa21341.Mod"
                                                                           select x).ToList<KeyValuePair<LorId, DiceCardXmlInfo>>())
            {
                bool flag = ModParameters.PersonalCardList.Contains(keyValuePair.Key.id);
                if (flag)
                {
                    UnitUtil.SetCustomCardOption(CardOption.Personal, keyValuePair.Key, false, ref source, ref list);
                }
                else
                {
                    bool flag2 = ModParameters.EgoPersonalCardList.Contains(keyValuePair.Key.id);
                    if (flag2)
                    {
                        UnitUtil.SetCustomCardOption(CardOption.EgoPersonal, keyValuePair.Key, false, ref source, ref list);
                    }
                    else
                    {
                        bool flag3 = allOnlyCardsId.Contains(keyValuePair.Key.id);
                        if (flag3)
                        {
                            UnitUtil.SetCustomCardOption(CardOption.OnlyPage, keyValuePair.Key, true, ref source, ref list);
                        }
                        else
                        {
                            UnitUtil.SetBaseKeywordCard(keyValuePair.Key, ref source, ref list);
                        }
                    }
                }
            }
        }

        public static void ChangePassiveItem()
        {
            foreach (var passive in Singleton<PassiveXmlList>.Instance.GetDataAll().Where(passive =>
                         passive.id.packageId == ModParameters.PackageId &&
                         ModParameters.UntransferablePassives.Contains(passive.id.id)))
                passive.CanGivePassive = false;
        }
    }
}