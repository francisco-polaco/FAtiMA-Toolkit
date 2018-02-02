using System;
using System.Collections.Generic;
using System.Linq;
using MCTS.DST.Objects;
using MCTS.Math;
using Utilities;

namespace MCTS.DST.WorldModels.CharacterModel
{
    public class Character
    {
        private const int MAX_HEALTH = 150;
        private const int MAX_HUNGER = 150;
        private const int MAX_SANITY = 200;
        private const int MaxInventorySize = 15;
        private const int MaxDefaultStackSize = 20;

        public Vector2i WalterPosition = new Vector2i();
        public double WalkedDistance { get; set; } = 0;
        public EquipableObject EquipedObject { get; set; } = EquipableObject.None;
        private List<Pair<string, int>> _inventory = new List<Pair<string, int>>();
        private StackTable _stackTable = new StackTable();
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

        //public bool HasSlotVacatedAfterRemoval(string entityType, int quantity = 1)
        //{
        //    int maxSizeStackForItem;
        //    try
        //    {
        //        maxSizeStackForItem = _stackTable.GetStackSize(entityType);
        //    }
        //    catch (StackTable.EntityTypeUnknownException)
        //    {
        //        maxSizeStackForItem = MaxDefaultStackSize;
        //    }
        //    return NumberOfObjectsInInventory(entityType) % maxSizeStackForItem - quantity <= 0;
        //}

        public bool IsInventoryFull(string entityType, int quantity = 1)
        {
            var hasObject = InventoryHasObject(entityType);
            //Console.WriteLine("Inventory: " + _inventory.Count + " - " + MaxInventorySize);
            if (!hasObject && _inventory.Count >= MaxInventorySize)
            {
                return true;
            }
            else if (hasObject)
            {
                int maxSizeStackForItem;
                try
                {
                    maxSizeStackForItem = _stackTable.GetStackSize(entityType);
                }
                catch (StackTable.EntityTypeUnknownException)
                {
                    maxSizeStackForItem = MaxDefaultStackSize;
                }

                var invCopy = _inventory.FindAll(p => p.Item1.Equals(entityType) && p.Item2 < maxSizeStackForItem)
                    .ToList();
                foreach (var pair in invCopy)
                {   // elements are copied
                    var remainingToStack = maxSizeStackForItem - pair.Item2;
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

                if (quantity > 0 )
                {
                    if (_inventory.Count >= MaxInventorySize)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public void AddToInventoryKB(string entityType, int quantity = 1)
        {
            var hasObject = InventoryHasObject(entityType);
            if (hasObject)
            {
                int maxSizeStackForItem;
                try
                {
                    maxSizeStackForItem = _stackTable.GetStackSize(entityType);
                }
                catch (StackTable.EntityTypeUnknownException)
                {
                    maxSizeStackForItem = MaxDefaultStackSize;
                }
                foreach (var pair in _inventory.FindAll(p => p.Item1.Equals(entityType) && p.Item2 < maxSizeStackForItem))
                {
                    var remainingToStack = maxSizeStackForItem - pair.Item2;
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
            }
            else
            {
                _inventory.Add(new Pair<string, int>(entityType, quantity));
            }
        }

        public void AddToInventory(string entityType, int quantity = 1)
        {
            var hasObject = InventoryHasObject(entityType);
            if (!hasObject && _inventory.Count >= MaxInventorySize)
            {
                throw new InventoryFullException();
            }
            else if(hasObject)
            {
                int maxSizeStackForItem;
                try
                {
                    maxSizeStackForItem = _stackTable.GetStackSize(entityType);
                }
                catch (StackTable.EntityTypeUnknownException)
                {
                    maxSizeStackForItem = MaxDefaultStackSize;
                }
                foreach (var pair in _inventory.FindAll(p => p.Item1.Equals(entityType) && p.Item2 < maxSizeStackForItem))
                {
                    var remainingToStack = maxSizeStackForItem - pair.Item2;
                    //Console.WriteLine(remainingToStack);
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

                if(quantity > 0)
                {
                    if (_inventory.Count >= MaxInventorySize)
                    {
                        throw new InventoryFullException(entityType);

                    }
                    else
                    {
                        _inventory.Add(new Pair<string, int>(entityType, quantity));
                    }
                }
            }
            else
            {
                _inventory.Add(new Pair<string, int>(entityType, quantity));
            }
        }


        //public bool InventoryHasObject(string entityType)
        //{
        //    return _inventory.Find(p => p.Item1.Equals(entityType)) != null;
        //}

        public bool InventoryHasObject(string entityType, int quantity = 1)
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

        public void RemoveFromInventory(string entityType, int quantity = 1)
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
            public InventoryFullException()
            {
            }

            public InventoryFullException(string entityType)
            {
                this.Message = entityType;
            }

            public override string Message { get; }
        }

        public class ItemQuantityOverflowException : Exception
        {
        }

        #endregion

        #region equip




        public bool CanEquip(string entityType)
        {
             return InventoryHasObject(entityType) && CanUnequip();
        }

        public void Equip(string entityType)
        {
            if (entityType.Equals("axe"))
            {
                RemoveFromInventory(entityType);
                Unequip();
                EquipAxe();
            }
            else if (entityType.Equals("pickaxe"))
            {
                RemoveFromInventory(entityType);
                Unequip();
                EquipPickAxe();
            }
            else if (entityType.Equals("torch"))
            {
                RemoveFromInventory(entityType);
                Unequip();
                EquipTorch();
            }
        }

        private void EquipAxe()
        {
            EquipedObject = EquipableObject.Axe;
        }

        private void EquipPickAxe()
        {
            EquipedObject = EquipableObject.Pickaxe;
        }

        private void EquipTorch()
        {
            EquipedObject = EquipableObject.Torch;
        }
        public void Unequip()
        {
            switch (EquipedObject)
            {
                case EquipableObject.Axe:
                    AddToInventory("axe");
                    break;
                case EquipableObject.Pickaxe:
                    AddToInventory("pickaxe");
                    break;
                case EquipableObject.Torch:
                    AddToInventory("torch");
                    break;
                default:
                    return;
            }

            EquipedObject = EquipableObject.None;
        }

        public bool CanUnequip()
        {
            switch (EquipedObject)
            {
                case EquipableObject.Axe:
                    return IsInventoryFull("axe");
                case EquipableObject.Pickaxe:
                    return IsInventoryFull("pickaxe");
                case EquipableObject.Torch:
                    return IsInventoryFull("torch");
                default:
                    return true;
            }
        }

        public bool IsUnequiped()
        {
            return EquipedObject == EquipableObject.None;
        }
        #endregion

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

        public void Eat(string entityType, int quantity = 1)
        {
            float increaseHealth = 0, increaseHunger = 0;
            if (entityType.Equals("carrot"))
            {
                increaseHunger = 12.5f;
                increaseHealth = 1f;
            }
            // add more cases
            // add more cases
            for (var i = 0; i < quantity; i++)
            {
                Health += increaseHealth;
                Hunger += increaseHunger;
            }
        }
    }
}