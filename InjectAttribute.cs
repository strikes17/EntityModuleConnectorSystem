using System;

namespace _Project.Scripts
{
    public class InjectAttribute : Attribute
    {
        private Type m_Type;

        public Type Type => m_Type;

        public InjectAttribute()
        {
        }

        public InjectAttribute(Type type)
        {
            m_Type = type;
        }
    }
}