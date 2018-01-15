using System.Collections.Generic;
using MCTS.DST.Objects;
using MCTS.Math;

namespace MCTS.DST.WorldModels
{
    public class Character
    {
        private const int Maxinvsize = 15;
        public Vector2i WalterPosition = new Vector2i();
        public double WalkedDistance { get; set; } = 0;
        public EquipableObject EquipedObject { get; set; } = EquipableObject.None;
        public List<string> Inventory { get; } = new List<string>();
        public int Hunger { get; set; }
        public int Health { get; set; }
        public int Sanity { get; set; }

        public bool IsInventoryFull()
        {
            return Inventory.Count >= Maxinvsize;
        }

        public void AddInventory(string guid)
        {
            if (Inventory.Count < Maxinvsize)
            {
                Inventory.Add(guid);
            }
            // Exception not thrown. It fails silently because CanExecute should check inventory size.
        }

        public bool InventoryHasObject(string guid)
        {
            return Inventory.Contains(guid);
        }

        public void RemoveInventory(string guid)
        {
            Inventory.Remove(guid);
        }

        public void EquipAxe()
        {
            EquipedObject = EquipableObject.Axe;
        }
    }
}