namespace TheOrganizedSaberDLL.Saber_Passives
{
    public class PassiveAbility_SmokeStimulants_Md5488 : PassiveAbilityBase
    {
        private int limit = 2;
        private int SmokeStacks;

        public override void OnRoundStart()
        {
            SmokeStacks = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke).stack;
            limit = 0;
        }

        public override void OnUseCard(BattlePlayingCardDataInUnitModel curCard)
        {
            base.OnUseCard(curCard);
            if (CheckCondition(curCard) && limit < 2)
                if (SmokeStacks > owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke).stack)
                {
                    owner.cardSlotDetail.RecoverPlayPoint(1);
                    owner.allyCardDetail.DrawCards(1);
                    limit += 1;
                }

            SmokeStacks = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke).stack;
        }

        public override void OnRollDice(BattleDiceBehavior behavior)
        {
            var curCard = behavior.card;
            if (CheckCondition(curCard) && limit < 2)
                if (SmokeStacks > owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke).stack)
                {
                    owner.cardSlotDetail.RecoverPlayPoint(1);
                    owner.allyCardDetail.DrawCards(1);
                    limit += 1;
                }

            SmokeStacks = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke).stack;
        }

        private bool CheckCondition(BattlePlayingCardDataInUnitModel card)
        {
            if (card != null)
            {
                var xmlData = card.card.XmlData;
                if (xmlData == null) return false;
                if (xmlData.Keywords.Contains("Smoke_Keyword")) return true;
                var abilityKeywords = Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords(xmlData);
                for (var i = 0; i < abilityKeywords.Count; i++)
                    if (abilityKeywords[i] == "Smoke_Keyword")
                        return true;
                foreach (var diceBehaviour in card.card.GetBehaviourList())
                {
                    var abilityKeywords_byScript =
                        Singleton<BattleCardAbilityDescXmlList>.Instance.GetAbilityKeywords_byScript(diceBehaviour
                            .Script);
                    for (var j = 0; j < abilityKeywords_byScript.Count; j++)
                        if (abilityKeywords_byScript[j] == "Smoke_Keyword")
                            return true;
                }

                return false;
            }

            return false;
        }

        public override void OnAddKeywordBufByCardForEvent(KeywordBuf keywordBuf, int stack, BufReadyType readyType)
        {
            if (keywordBuf == KeywordBuf.Smoke)
                SmokeStacks = owner.bufListDetail.GetActivatedBuf(KeywordBuf.Smoke).stack;
        }
    }
}