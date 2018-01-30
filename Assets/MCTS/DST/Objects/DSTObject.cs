using System;
using MCTS.Math;
using System.Collections.Generic;
using MCTS.DST.Actions;

namespace MCTS.DST.Objects
{
    public class DSTObject 
    {
        private string entityType;
        private int posX;
        private int posZ;

        public DSTObject(string guid)
        {
            Guid = guid;
        }

        public bool PickWorkable { get; set; } = false;
        public bool CollectWorkable { get; set; } = false;
        public bool ChopWorkable { get; set; } = false;
        public bool HammerWorkable { get; set; } = false;
        public bool DigWorkable { get; set; } = false;
        public bool MineWorkable { get; set; } = false;
        public bool InInventory { get; set; } = false;
        public bool Equippable { get; set; }
        public int quantity { get; set; } = 0;

        private bool PosXSet { get; set; }
        private bool PosZSet { get; set; }
        private bool EntitySet { get; set; }

        public int SquaredDistance { get; private set; }

        public string Guid { get; }
        public bool IsEatable { get; set; }
        public bool IsLightSource { get; set; }

        public int GetPosX()
        {
            return posX;
        }

        public void SetPosX(int value)
        {
            PosXSet = true;
            posX = value;
        }

        public int GetPosZ()
        {
            return posZ;
        }

        public void SetPosZ(int value)
        {
            PosZSet = true;
            posZ = value;
        }

        public string GetEntityType()
        {
            return entityType;
        }

        public void SetEntityType(string value)
        {
            EntitySet = true;
            entityType = value;
        }

        public bool isComplete()
        {
            return EntitySet && PosXSet && PosZSet;
        }

        public void calculateDistanceToChar(Vector2i walterPosition)
        {
            var x = (int) walterPosition.x - posX;
            var y = (int) walterPosition.y - posZ;
            SquaredDistance = x * x + y * y;
        }

        internal Vector2i GetPosition() {
            return new Vector2i(posX, posZ);
        }

        public override string ToString() {
            return "PICKABLE OBJ - " + entityType + " :x: " + posX + " :z: " + posZ + " :picakble: " + PickWorkable + " :complete: " + isComplete();
        }

        public override bool Equals(object obj) {
            return obj is DSTObject @object &&
                   Guid == @object.Guid;
        }

        //public override int GetHashCode() {
        //    var hashCode = -698353903;
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(entityType);
        //    hashCode = hashCode * -1521134295 + posX.GetHashCode();
        //    hashCode = hashCode * -1521134295 + posZ.GetHashCode();
        //    hashCode = hashCode * -1521134295 + Pickable.GetHashCode();
        //    hashCode = hashCode * -1521134295 + PosXSet.GetHashCode();
        //    hashCode = hashCode * -1521134295 + PosZSet.GetHashCode();
        //    hashCode = hashCode * -1521134295 + EntitySet.GetHashCode();
        //    hashCode = hashCode * -1521134295 + SquaredDistance.GetHashCode();
        //    hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Guid);
        //    return hashCode;
        //}
    }
}