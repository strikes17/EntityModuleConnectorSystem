using System;
using System.Collections.Generic;
using System.Linq;

namespace _Project.Scripts
{
    public class Pool<T> where T : IPoolable<T>
    {
        public event Action<T> ElementEnabled = delegate { };

        public event Action<T> ElementDisabled = delegate { };

        private List<T> m_Elements = new();
        private T m_MasterElement;

        public Pool(T masterElement) => m_MasterElement = masterElement;

        public IEnumerable<T> GetActiveElements()
        {
            return m_Elements.Where(x => x.IsAvailable);
        }
        
        public void DisableAll()
        {
            foreach (var poolable in m_Elements)
            {
                poolable.Disable();
                ElementDisabled(poolable);
            }
        }

        public void EnableAll()
        {
            foreach (var poolable in m_Elements)
            {
                poolable.Enable();
                ElementEnabled(poolable);
            }
        }

        public T Get()
        {
            T target = default;
            foreach (var element in m_Elements)
            {
                if (element.IsAvailable)
                {
                    return element;
                }
            }

            var instance = m_MasterElement.CreateInstance();
            instance.Enabled += Enabled;
            instance.Disabled += Disabled;
            m_Elements.Add(instance);

            return instance;
        }

        private void Disabled(T element)
        {
            ElementDisabled(element);
        }

        private void Enabled(T element)
        {
            ElementEnabled(element);
        }
    }
}