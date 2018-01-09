using System;
using MCTS.DST.Objects;
using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    enum Tree
    {
        Evergreen,
        Birchnut //etc
    }
    internal class ChopAction : Action
    {
        private readonly string _targetGuid;
        private readonly Vector2i _positionToGo;
        private readonly int _numberOfDroppedLogs;

        public ChopAction(string targetGuid, Vector2i positionToGo, Tree kind) : base("CHOP")
        {
            _targetGuid = targetGuid;
            this._positionToGo = positionToGo;
            // no idea from the values xD
            // age could also be considered, but it may explode the number of actions
            if (kind == Tree.Evergreen)
            {
                _numberOfDroppedLogs = 2;
            }else if (kind == Tree.Birchnut)
            {
                _numberOfDroppedLogs = 3;
            }
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            worldModel.RemovePickableObject(_targetGuid);
            worldModel.walkedDistanced(_positionToGo);
            for (int i = 0; i < _numberOfDroppedLogs; i++)
            {
                var pickable = new PickableObject(Guid.NewGuid().ToString());
                pickable.SetPosX(_positionToGo.x);
                pickable.SetPosZ(_positionToGo.y);
                pickable.SetEntityType("log"); // tronco
                worldModel.AddPickableObject(pickable);
            }
        }


        public override bool CanExecute(WorldModel worldModel)
        {
            if (!base.CanExecute(worldModel)) return false;
            return worldModel.GotAxeEquiped();
        }

        public override bool CanExecute()
        {
            if (!base.CanExecute()) return false;
            // todo: ver se tenho machado no mundo
            return base.CanExecute();
        }

        public override void Execute()
        {
            base.Execute();
        }

        public override double GetDuration()
        {
            return base.GetDuration();
        }

        public override double GetDuration(WorldModel worldModel)
        {
            return base.GetDuration(worldModel);
        }

        public override string getTarget()
        {
            return _targetGuid;
        }
    }
}

