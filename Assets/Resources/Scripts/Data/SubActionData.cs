using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SubActionData
{
    public interface TableData
    {
        public int Index
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }
    }

    public interface IUnitAction
    {
        public void UnitMove();

        public void UnitHold();

        public void UnitAttack();

        public void UnitStop();
    }

    public interface IUseSkill
    {
        public void UnitUseSkill();
    }

    public interface IWaveMove
    {
        public void WaveMove();
    }

    public interface IUnitManufacture
    {
        public void UnitManafacture();
    }

    public interface IUserActionButton
    {
        public void MoveButton();
        public void StopButton();
        public void AttackButton();
    }
}
