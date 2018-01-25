using System;
using System.Collections.Generic;
using System.Linq;
using MCTS.DST.Objects;
using MCTS.Math;
using Utilities;

namespace MCTS.DST.WorldModels
{
    public class Character
    {
        private const int MAX_HEALTH = 150;
        private const int MAX_HUNGER = 150;
        private const int MAX_SANITY = 200;
        private const int Maxinvsize = 15;
        private const int MaxSizeStackForItem = 20;

        public Vector2i WalterPosition = new Vector2i();
        public double WalkedDistance { get; set; } = 0;
        public EquipableObject EquipedObject { get; set; } = EquipableObject.None;
        private List<Pair<string, int>> _inventory = new List<Pair<string, int>>();
        public float Hunger { get; set; }
        public float Health { get; set; }
        public float Sanity { get; set; }

        //TODO update generateClone 
        public Character GenerateClone() {
            var toReturn = new Character();
            toReturn.Health = this.Health;
            toReturn.Hunger = this.Hunger;
            toReturn.Sanity = this.Sanity;
            toReturn.WalterPosition = new Vector2i(this.WalterPosition.x,this.WalterPosition.y);
            toReturn.WalkedDistance = this.WalkedDistance;
            toReturn.EquipedObject = this.EquipedObject;

            toReturn._inventory = this._inventory.Select(slot => new Pair<string, int>(slot.Item1,slot.Item2)).ToList();


            return toReturn;
        }

        public void makeSureMin()
        {
            Hunger = (Hunger < 0 ? 0 : Hunger);
            Health = (Health < 0 ? 0 : Health);
            Sanity = (Sanity < 0 ? 0 : Sanity);
        }

        public void makeSureMax()
        {
            Hunger = (Hunger > MAX_HUNGER ? MAX_HUNGER : Hunger);
            Health = (Health > MAX_HEALTH ? MAX_HEALTH : Health);
            Sanity = (Sanity > MAX_SANITY ? MAX_SANITY : Sanity);
        }

        #region inventory

       

        public bool IsInventoryFull(string entityType, int quantity = 1)
        {
            var hasObject = InventoryHasObject(entityType);
            Console.WriteLine((_inventory.Count + " - " + Maxinvsize));
            if (!hasObject && _inventory.Count >= Maxinvsize)
            {
                return true;
            }
            else if (hasObject)
            {
                foreach (var pair in _inventory.FindAll(p => p.Item1.Equals(entityType) && p.Item2 < MaxSizeStackForItem).ToList())
                {   // elements are copied
                    var remainingToStack = MaxSizeStackForItem - pair.Item2;
                    if (quantity > remainingToStack)
                    {
                        pair.Item2 += remainingToStack;
                        quantity -= remainingToStack;
                    }
                    else
                    {
                        pair.Item2 += quantity;
                        quantity = 0;
                    }
                }

                if (quantity > 0)
                {
                    return true;
                }
            }

            return false;
        }

        public void AddToInventory(string entityType, int quantity = 1)
        {
            var hasObject = InventoryHasObject(entityType);
            if (!hasObject && _inventory.Count >= Maxinvsize)
            {
                throw new InventoryFullException();
            }
            else if(hasObject)
            {
                foreach (var pair in _inventory.FindAll(p => p.Item1.Equals(entityType) && p.Item2 < MaxSizeStackForItem))
                {
                    var remainingToStack = MaxSizeStackForItem - pair.Item2;
                    if (quantity > remainingToStack)
                    {
                        pair.Item2 += remainingToStack;
                        quantity -= remainingToStack;
                    }
                    else
                    {
                        pair.Item2 += quantity;
                        quantity = 0;
                    }
                }

                if (quantity > 0)
                {
                    throw new InventoryFullException();
                }
            }
            else
            {
                _inventory.Add(new Pair<string, int>(entityType, quantity));
            }
        }


        public bool InventoryHasObject(string entityType)
        {
            return _inventory.Find(p => p.Item1.Equals(entityType)) != null;
        }

        public bool InventoryHasObject(string entityType, int quantity)
        {
            return NumberOfObjectsInInventory(entityType) >= quantity;
        }


        //public bool InventoryHasAtLeast(string entityType, int quantity = 1)
        //{
        //    return quantity <= _inventory.FindAll(p => p.Item1.Equals(entityType))
        //        .Select(item => item.Item2).Sum();
        //}

        public int NumberOfObjectsInInventory(string entityType)
        {
            return _inventory.FindAll(p => p.Item1.Equals(entityType))
                .Select(item => item.Item2).Sum();
        }

        public void RemoveFromInventory(string entityType, int quantity)
        {   
            
            foreach (var pair in _inventory.FindAll(p => p.Item1.Equals(entityType)))
            {
                if (quantity < 0)
                {
                    throw new ItemQuantityOverflowException();
                }

                if (quantity > pair.Item2)
                {
                    quantity -= pair.Item2;
                    _inventory.Remove(pair);
                }
                else
                {
                    var itemQuantity = pair.Item2;
                    pair.Item2 -= quantity;
                    quantity -= itemQuantity;
                }
            }
        }

        public int RemoveAllFromInventory(string entityType)
        {
            return _inventory.RemoveAll(p => p.Item1.Equals(entityType));
        }

        public class InventoryFullException : Exception
        {
        }

        public class ItemQuantityOverflowException : Exception
        {
        }

        #endregion


        public void EquipAxe()
        {
            EquipedObject = EquipableObject.Axe;
        }


        public void SetStats(int health = -1, int hunger = -1, int sanity = -1)
        {
            Health = health < 0 ? MAX_HEALTH : health;
            Hunger = hunger < 0 ? MAX_HUNGER : hunger;
            Sanity = sanity < 0 ? MAX_SANITY : sanity;
        }

        public void ReceiveDamage(int damage)
        {
            Health -= damage;
        }
    }

    public class StackTable
    {
        private Dictionary<string, int> _stackSize = new Dictionary<string, int>();

        public int GetStackSize(string entityType)
        {
            if (_stackSize.ContainsKey(entityType))
                return _stackSize[entityType];
            else
                throw new EntityTypeUnknownException();
        }

        public class EntityTypeUnknownException : Exception
        {
        }
    }
}