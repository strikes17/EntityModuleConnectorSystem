namespace _Project.Scripts
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    [Serializable]
    public class SerializableHashSet<T>
    {
        [SerializeField] private List<T> m_List = new List<T>();

        //This is how you access hashset, note that get method always updates the _list to be up-to-date with hashset.
        public HashSet<T> Set
        {
            get
            {
                m_HashSet = new HashSet<T>(m_List);
                return m_HashSet;
            }
        }

        private HashSet<T> m_HashSet = new HashSet<T>();

        public int Count => m_HashSet.Count;

        //This will ensure that when you assign to set, it updates list
        public void SetValue(HashSet<T> hashSet)
        {
            m_HashSet = hashSet;
            m_List = new List<T>(hashSet);
        }

        public void Add(T item)
        {
            if (m_HashSet == null) m_HashSet = new HashSet<T>();
            m_HashSet.Add(item);
            m_List = new List<T>(m_HashSet);
        }

        public void Remove(T item)
        {
            if (m_HashSet == null) m_HashSet = new HashSet<T>();
            m_HashSet.Remove(item);
            m_List = new List<T>(m_HashSet);
        }

        public bool Contains(T item)
        {
            if (m_HashSet == null) m_HashSet = new HashSet<T>();
            return m_HashSet.Contains(item);
        }

    }
}