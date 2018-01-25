using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MCTS.DST.WorldModels.CharacterModel
{
    public class StackTable
    {
        private readonly Dictionary<string, int> _stackSize = new Dictionary<string, int>();

        public StackTable()
        {
            foreach (var type in GetDerivedTypesFor(typeof(StackQuantity)))
            {
                if(Activator.CreateInstance(type) is StackQuantity obj)
                    _stackSize.Add(obj.PrefabName, obj.StackSize);
            }
            
        }

        public int GetStackSize(string entityType)
        {
            if (_stackSize.ContainsKey(entityType))
                return _stackSize[entityType];
            else
                throw new EntityTypeUnknownException();
        }

        public static IEnumerable<Type> GetDerivedTypesFor(Type baseType)
        {
            var assembly = Assembly.GetExecutingAssembly();

            return assembly.GetTypes()
                .Where(baseType.IsAssignableFrom)
                .Where(t => baseType != t);
        }

        public class EntityTypeUnknownException : Exception
        {
        }
    }
}