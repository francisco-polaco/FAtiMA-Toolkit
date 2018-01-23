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
        private const int Maxinvsize = 15;
        private const int MaxSizeStackForItem = 20;

        public Vector2i WalterPosition = new Vector2i();
        public double WalkedDistance { get; set; } = 0;
        public EquipableObject EquipedObject { get; set; } = EquipableObject.None;
        private readonly List<Pair<string, int>> _inventory = new List<Pair<string, int>>();
        public int Hunger { get; set; }
        public int Health { get; set; }
        public int Sanity { get; set; }

        #region inventory
        
        public bool IsInventoryFull(string entityType, int quantity = 1)
        {
            var hasObject = InventoryHasObject(entityType);
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
    }
}