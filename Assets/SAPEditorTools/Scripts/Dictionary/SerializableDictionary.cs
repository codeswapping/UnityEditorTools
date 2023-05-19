using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableDictionary<TKey, TValue> : ISerializedDictionary
{
	[SerializeField]
	private List<TKey> keys = new List<TKey>();

	[SerializeField]
	private List<TValue> values = new List<TValue>();

	private Dictionary<TKey, TValue> thisDictionary = new();

	public TValue this[TKey key]
	{
		get
		{
			if(thisDictionary.Count == 0)
			{
				OnAfterDeserialize();
			}
			return thisDictionary[key];
		}
		set
		{
			if (thisDictionary.Count == 0)
			{
				OnAfterDeserialize();
			}
			thisDictionary[key] = value;
		}
	}
	// save the dictionary to lists
	public void OnBeforeSerialize()
	{
		keys.Clear();
		values.Clear();
		foreach (KeyValuePair<TKey, TValue> pair in thisDictionary)
		{
			keys.Add(pair.Key);
			values.Add(pair.Value);
		}
	}

	// load dictionary from lists
	public void OnAfterDeserialize()
	{
		thisDictionary.Clear();

		if (keys.Count != values.Count)
			throw new System.Exception(string.Format("there are {0} keys and {1} values after deserialization. Make sure that both key and value types are serializable."));

		for (int i = 0; i < keys.Count; i++)
		{
			if (thisDictionary.ContainsKey(keys[i]))
				continue;
			thisDictionary.Add(keys[i], values[i]);
		}
	}
}
