using MCTS.Math;

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

        public void calculateDistanceToChar(Vector2d walterPosition)
        {
            var x = (int) walterPosition.x - posX;
            var y = (int) walterPosition.y - posZ;
            SquaredDistance = x * x + y * y;
        }
    }
}