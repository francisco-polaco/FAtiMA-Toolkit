﻿using MCTS.DST.WorldModels;

namespace MCTS.DST.Actions
{
    internal class PickAction : Action
    {
        private readonly string _guid;

        public PickAction(string name, string guid) : base(name)
        {
            _guid = guid;
        }

        public override void ApplyActionEffects(WorldModel worldModel)
        {
            worldModel.RemoveObject(_guid);
        }

        public override bool CanExecute(WorldModel worldModel)
        {
            return base.CanExecute(worldModel);
        }

        public override bool CanExecute()
        {
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
    }

    //internal class PickableObject : GameObject
    //{
    //    public PickableObject(string guid) : base(guid)
    //    {
    //    }
    //}
}