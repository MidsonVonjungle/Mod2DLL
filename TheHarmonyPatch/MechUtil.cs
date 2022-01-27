using System;

namespace TheOrganizedSaberDLL.TheHarmonyPatch
{
    public class MechUtilBase
    {
        private readonly MechUtilBaseModel _model;
        private MechUtilBaseModel model;

        public MechUtilBase(MechUtilBaseModel model)
        {
            _model = model;
            if (model.HasEgo && model.EgoCardId != null) model.Owner.personalEgoDetail.AddCard(model.EgoCardId);
        }

        public virtual bool EgoCheck()
        {
            return _model.EgoActivated;
        }

        public virtual bool CheckSkinChangeIsActive()
        {
            return !string.IsNullOrEmpty(_model.SkinName);
        }

        public virtual void EgoActive()
        {
            if (_model.Owner.bufListDetail.HasAssimilation()) return;
            _model.EgoActivated = false;
            if (_model.IsSummonEgo && BattleObjectManager.instance.GetAliveList(Faction.Player).Count > 1)
            {
                _model.Owner.personalEgoDetail.AddCard(_model.SecondaryEgoCardId);
                return;
            }

            if (!string.IsNullOrEmpty(_model.SkinName)) _model.Owner.view.SetAltSkin(_model.SkinName);
            _model.Owner.bufListDetail.AddBufWithoutDuplication(
                (BattleUnitBuf)Activator.CreateInstance(_model.EgoType));
            _model.Owner.cardSlotDetail.RecoverPlayPoint(_model.Owner.cardSlotDetail.GetMaxPlayPoint());
            if (_model.HasEgoAttack) _model.Owner.personalEgoDetail.AddCard(_model.EgoAttackCardId);
            if (_model.RefreshUI) UnitUtil.RefreshCombatUI();
        }

        public virtual void OnUseExpireCard(LorId cardId)
        {
            if (_model.LorIdArray != null && _model.LorIdArray.Contains(cardId))
                _model.Owner.personalEgoDetail.RemoveCard(cardId);
            if (_model.EgoAttackCardExpire && _model.EgoAttackCardId == cardId)
                _model.Owner.personalEgoDetail.RemoveCard(_model.EgoAttackCardId);
            if (!_model.HasEgo || _model.EgoCardId != cardId) return;
            if (_model.EgoCardId != null) _model.Owner.personalEgoDetail.RemoveCard(_model.EgoCardId);
            if (_model.HasAdditionalPassive) _model.Owner.passiveDetail.AddPassive(_model.AdditionalPassiveId);
            _model.Owner.breakDetail.ResetGauge();
            _model.Owner.breakDetail.RecoverBreakLife(1, true);
            _model.Owner.breakDetail.nextTurnBreak = false;
            _model.EgoActivated = true;
        }
    }
}