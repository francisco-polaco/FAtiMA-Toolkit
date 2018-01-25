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
    internal class ChopAction : WalktoAction
    {

        private readonly int _numberOfDroppedLogs;
        //private readonly string _typeOfTree;

        public override string GetXmlName() {
            return "Chop " + TargetPosition;
        }


        public ChopAction(Vector2i positionToGo, string guid, string typeOfTree) : base(positionToGo,guid,typeOfTree)
        {
            // TODO age MUST also be considered, but it may explode the number of actions
            _numberOfDroppedLogs = 2;


            //Evergreen
            //Birchnut
            //Spiky
            //Mushtree
            //Lumpy
            //"Normal"
            //Palm
            //Jungle
            //Mangrove
            //"Regular"
            //Ash
            //Twiggy
        }

        
        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
            worldModel.RemoveChopableObject(EntityType ,TargetGuid);
            
            for (int i = 0; i < _numberOfDroppedLogs; i++)
            {
                var pickable = new DSTObject(Guid.NewGuid().ToString());
                pickable.SetPosX(TargetPosition.x);
                pickable.SetPosZ(TargetPosition.y);
                pickable.SetEntityType("log"); // tronco
                pickable.PickWorkable = true;
                worldModel.AddPickableObject(pickable);
            }
        }


        public override bool CanExecute(WorldModel worldModel)
        {
            // todo: ver se tenho machado no mundo
            //Console.WriteLine("Entrei aquiiiiiiiiiiiiiiiiiiiiiiiiiiiiiii"+worldModel.GotAxeEquiped());
            return base.CanExecute(worldModel) && worldModel.Walter.EquipedObject == EquipableObject.Axe;
        }

        //public override bool CanExecute()
        //{
        //    if (!base.CanExecute()) return false;
        //    // todo: ver se tenho machado no mundo
        //    return base.CanExecute();
        //}

        //public override void Execute()
        //{
        //    base.Execute();
        //}

        //public override double GetDuration()
        //{
        //    return base.GetDuration();
        //}

        public override int GetDuration(WorldModel worldModel)
        {
            return base.GetDuration(worldModel) + 1;
        }

        public override string GetDstInterpretableAction() {
            return "Action(CHOP, -, -, -, -)";
        }
    }
}

