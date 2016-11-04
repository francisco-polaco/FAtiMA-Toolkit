﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Utilities;
using WellFormedNames;
using IQueryable = WellFormedNames.IQueryable;

namespace KnowledgeBase
{
	using BeliefPair = Pair<Name, IEnumerable<SubstitutionSet>>;

	public partial class KB : IDynamicPropertiesRegister
	{
		private const string TMP_MARKER = "_arg";

		private delegate IEnumerable<Pair<Name, SubstitutionSet>> DynamicPropertyCalculator(IQueryable kb, Name perspective, IList<Name> args, IEnumerable<SubstitutionSet> constraints);

		private sealed class DynamicKnowledgeEntry
		{
			private readonly DynamicPropertyCalculator _surogate;
			private readonly Name[] _parameters;
			public readonly string description;

			public DynamicKnowledgeEntry(DynamicPropertyCalculator surogate, Name[] parameters, string description)
			{
				_surogate = surogate;
				_parameters = parameters;
				this.description = description;
			}

			public IEnumerable<Pair<Name, SubstitutionSet>> Evaluate(IQueryable kb, Name perspective, SubstitutionSet args2, IEnumerable<SubstitutionSet> constraints)
			{
				//var dic = ObjectPool<Dictionary<Name, Name>>.GetObject();
				var args = ObjectPool<List<Name>>.GetObject();

				try
				{
					//foreach(var s in args2)
					//{
					//	var p = s.Variable.RemoveBoundedVariables(TMP_MARKER);
					//	dic[p] = s.Value;
					//	if (!s.Value.IsVariable)
					//		continue;
					//	dic[s.Value] = p;
					//}

					args.AddRange(_parameters.Select(p => args2[p]).Select(v => v == null ? Name.UNIVERSAL_SYMBOL : v.RemoveBoundedVariables(TMP_MARKER)));
					//args.AddRange(_parameters.Select(p =>
					//{
					//	Name v;
					//	if (dic.TryGetValue(p, out v))
					//		return v;
					//	return Name.UNIVERSAL_SYMBOL;
					//}));
					return _surogate(kb, perspective, args, constraints);
				}
				finally
				{
					args.Clear();
					ObjectPool<List<Name>>.Recycle(args);

					//dic.Clear();
					//ObjectPool<Dictionary<Name, Name>>.Recycle(dic);
				}
			}
		}

		#region Dynamic Property Registry

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T1 surrogate, string description = null)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.GetMethodInfo(),
				(kb, perspective, args, constraints) => surrogate(kb,constraints,perspective,args[0]));
		}

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T2 surrogate, string description = null)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.GetMethodInfo(),
				(kb, perspective, args, constraints) => surrogate(kb, constraints, perspective, args[0],args[1]));
		}

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T3 surrogate, string description = null)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.GetMethodInfo(),
				(kb, perspective, args, constraints) => surrogate(kb, constraints, perspective, args[0], args[1],args[2]));
		}

		public void RegistDynamicProperty(Name propertyName, DynamicPropertyCalculator_T4 surrogate, string description = null)
		{
			if (surrogate == null)
				throw new ArgumentNullException(nameof(surrogate));

			internal_RegistDynamicProperty(propertyName, description, surrogate.GetMethodInfo(),
				(kb, perspective, args, constraints) => surrogate(kb, constraints, perspective, args[0], args[1], args[2], args[3]));
		}

		#endregion

		private void internal_RegistDynamicProperty(Name propertyName, string description, MethodInfo surogate, DynamicPropertyCalculator converted)
		{
			if (!propertyName.IsPrimitive)
				throw new ArgumentException("The property name must be a primitive symbol.", nameof(propertyName));

			var p = surogate.GetParameters();
			var propertyParameters = p.Skip(3).Select(p2 => (Name) $"[{p2.Name}]").ToArray();
			var template = Name.BuildName(propertyParameters.Prepend(propertyName));
			
			var r = m_dynamicProperties.Unify(template).FirstOrDefault();
			if (r != null)
				throw new ArgumentException($"There is already a registed property with the name {propertyName} that receives {p.Length - 3} parameters.");

			if (m_knowledgeStorage.Unify(template).Any())
				throw new ArgumentException($"There are already stored property values that will collide with the given dynamic property.");

			m_dynamicProperties.Add(template, new DynamicKnowledgeEntry(converted,propertyParameters, description));
		}

		public void UnregistDynamicProperty(Name propertyTemplate)
		{
			if (!m_dynamicProperties.Remove(propertyTemplate))
				throw new Exception($"Unknown Dynamic Property {propertyTemplate}");
		}

		public IEnumerable<DynamicPropertyEntry> GetDynamicProperties()
		{
			return m_dynamicProperties.Select(p => new DynamicPropertyEntry() { PropertyTemplate = p.Key, Description = p.Value.description ?? "No Description" });
		}

		private IEnumerable<BeliefPair> AskDynamicProperties(Name property, Name perspective, IEnumerable<SubstitutionSet> constraints)
		{
			if (m_dynamicProperties.Count == 0)
				yield break;

			Name tmpPropertyName = property.ReplaceUnboundVariables(TMP_MARKER);

			var d = m_dynamicProperties.Unify(tmpPropertyName).ToList();
			if (d.Count == 0)
				yield break;

			var results = d.SelectMany(p => p.Item1.Evaluate(this, perspective, p.Item2, constraints).ToList());

			foreach (var g in results.GroupBy(p => p.Item1, p => p.Item2))
			{
				yield return Tuples.Create(g.Key, g.Distinct());
			}
		}
	}
}