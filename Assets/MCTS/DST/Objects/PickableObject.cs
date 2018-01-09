using System;
using MCTS.Math;
using System.Collections.Generic;
using MCTS.DST.Actions;

namespace MCTS.DST.Objects
{
    public class PickableObject 
    {
        private string entityType;

        private int posX;

        private int posZ;

        public PickableObject(string guid)
        {
            Guid = guid;
        }

        public bool Pickable { get; set; } = false;
        private bool PosXSet { get; set; }
        private bool PosZSet { get; set; }
        private bool EntitySet { get; set; }

        public int SquaredDistance { get; private set; }

        public string Guid { get; }

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

        public bool isPickableComplete()
        {
            return Pickable && EntitySet && PosXSet && PosZSet;
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
            return "PICKABLE OBJ - " + entityType + " :x: " + posX + " :z: " + posZ + " :picakble: " + Pickable + " :complete: " + isPickableComplete();
        }

        //public override bool Equals(object obj) {
        //    var @object = obj as PickableObject;
        //    return @object != null &&
        //           entityType == @object.entityType &&
        //           posX == @object.posX &&
        //           posZ == @object.posZ &&
        //           Pickable == @object.Pickable &&
        //           PosXSet == @object.PosXSet &&
        //           PosZSet == @object.PosZSet &&
        //           EntitySet == @object.EntitySet &&
        //           SquaredDistance == @object.SquaredDistance &&
        //           Guid == @object.Guid;
        //}

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