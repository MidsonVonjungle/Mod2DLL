namespace TheOrganizedSaberDLL.Saber_Passives
{
    public class PassiveAbility_CloudedCounter_Md5488 : PassiveAbilityBase
    {
        private static readonly string _packageId = "Therealtest2";

        public override void OnStartBattle()
        {
            if (owner.bufListDetail.GetKewordBufStack(KeywordBuf.Smoke) >= 4)
            {
                var battleDiceCardModel =
                    BattleDiceCardModel.CreatePlayingCard(
                        ItemXmlDataList.instance.GetCardItem(new LorId(_packageId, 12)));
                owner.cardSlotDetail.keepCard.AddBehaviours(battleDiceCardModel,
                    battleDiceCardModel.CreateDiceCardBehaviorList());
                owner.allyCardDetail.ExhaustCardInHand(battleDiceCardModel);
            }
        }
    }
}