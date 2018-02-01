using System;
using System.Collections.Generic;
using MCTS.DST.Objects;
using MCTS.DST.WorldModels;
using MCTS.Math;

namespace MCTS.DST.Actions
{
    
    internal class MineAction : WalktoAction
    {

        private readonly EmptyMineableStuff _toMine;
         
        public MineAction(Vector2i positionToGo, string guid, string entityType ) : base(positionToGo,guid, entityType)
        {
            if (entityType.Equals("rock1"))
            {
                _toMine = new Boulder();
            }
            else {
                _toMine = new EmptyMineableStuff();
                Console.WriteLine("TO_MINE: " +entityType + " unknown");
            }
        }
        public override string GetXmlName() {
            return "Mine " + TargetPosition;
        }


        public override void ApplyActionEffects(WorldModel worldModel)
        {
            base.ApplyActionEffects(worldModel);
            worldModel.RemoveMineableObject(EntityType ,TargetGuid);
            foreach (var pickable in _toMine.drops())
            {
                pickable.SetPosX(TargetPosition.x);
                pickable.SetPosZ(TargetPosition.y);
                pickable.PickWorkable = true;
                worldModel.AddPickableObject(pickable);
            }
        }


        public override bool CanExecute(WorldModel worldModel)
        {
            // todo: ver se tenho pickaxe no mundo
            return base.CanExecute(worldModel) && worldModel.Walter.EquipedObject == EquipableObject.Pickaxe;
        }

        public override int GetDuration(WorldModel worldModel)
        {
            return base.GetDuration(worldModel) + 1;
        }

        public override string GetDstInterpretableAction() {
            return "Action(MINE, -, -, -, -)";
        }
    }


    class EmptyMineableStuff
    {
        public virtual int numberHits()
        {
            return 6;
        }

        public virtual List<DSTObject> drops()
        {
            return new List<DSTObject>();
        }
    }

    class Boulder : EmptyMineableStuff
    {
        public override List<DSTObject> drops()
        {
            var obj1 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj1.SetEntityType("Rocks");
            obj1.PickWorkable = true;
            var obj2 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj2.SetEntityType("Rocks");
            obj2.PickWorkable = true;
            var obj3 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj3.SetEntityType("Rocks");
            obj3.PickWorkable = true;

            var obj4 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj4.SetEntityType("Flint");
            obj4.PickWorkable = true;

            var obj5 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj5.SetEntityType("Nitre");
            obj5.PickWorkable = true;

            var lista = new List<DSTObject> {obj1, obj2, obj3, obj4, obj5};
            return lista;
        }
    }

    class GoldVein : EmptyMineableStuff
    {
        public override List<DSTObject> drops()
        {
            var obj1 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj1.SetEntityType("Rocks");
            obj1.PickWorkable = true;
            var obj2 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj2.SetEntityType("Rocks");
            obj2.PickWorkable = true;
            var obj3 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj3.SetEntityType("Rocks");
            obj3.PickWorkable = true;

            var obj4 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj4.SetEntityType("Flint");
            obj4.PickWorkable = true;

            var obj5 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj5.SetEntityType("Gold Nugget");
            obj5.PickWorkable = true;

            var lista = new List<DSTObject> {obj1, obj2, obj3, obj4, obj5};
            return lista;
        }
    }

    class Flintless : EmptyMineableStuff {
        public override List<DSTObject> drops() {
            var obj1 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj1.SetEntityType("Rocks");
            obj1.PickWorkable = true;
            var obj2 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj2.SetEntityType("Rocks");
            obj2.PickWorkable = true;
            var obj3 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj3.SetEntityType("Rocks");
            obj3.PickWorkable = true;
            var obj4 = new DSTObject(DSTHelper.GenerateNewGuid());
            obj4.SetEntityType("Rocks");
            obj4.PickWorkable = true;

            var lista = new List<DSTObject> { obj1, obj2, obj3, obj4};
            return lista;
        }
    }
}

