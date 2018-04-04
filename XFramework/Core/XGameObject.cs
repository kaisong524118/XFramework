using System;
using System.Collections.Generic;
using System.Text;

namespace XFramework
{
    public class XGameObject : XObject
    {
        private List<XComponent> m_XComponents;

        public XGameObject()
            : this("XGameObject")
        {
        }

        public XGameObject(string name)
            :this(name,null)
        {
        }

        public XGameObject(string name,params Type[] components)
        {
            this.Name = name;
            this.AddComponents(components);
        }

        private XComponent AddComponentInternal(Type type)
        {
            if (m_XComponents == null)
                m_XComponents = new List<XComponent>();
            XComponent component = (XComponent)Activator.CreateInstance(type, this);
            m_XComponents.Add(component);
            return component;
        }

        public XComponent GetComponentInternal(Type type)
        {
            if (m_XComponents == null)
                return null;
            int count = m_XComponents.Count;
            for (int i = 0; i < count; i++)
                if (m_XComponents[i].GetType() == type)
                    return m_XComponents[i];
            return null;
        }

        public XComponent[] GetComponentsInternal(Type type)
        {
            if (m_XComponents == null)
                return null;
            List<XComponent> list = null;
            int count = m_XComponents.Count;
            for (int i = 0; i < count; i++)
                if (m_XComponents[i].GetType() == type)
                {
                    if (list == null)list = new List<XComponent>();
                    list.Add(m_XComponents[i]);
                } 
            return list.ToArray();
        }

        public void AddComponents(Type[] components)
        {
            if (components == null)
                return;
            int count = components.Length;
            for (int i = 0; i < count; i++)
                this.AddComponentInternal(components[i]);
        }



        public XComponent AddComponent(Type type)
        {
            return this.AddComponentInternal(type);
        }

        public T AddComponent<T>()where T : XComponent
        {
            return this.AddComponent(typeof(T)) as T;
        }


        public XComponent GetComponent(Type type)
        {
            return this.GetComponentInternal(type);
        }

        public T GetComponent<T>() where T : XComponent
        {
            return this.GetComponentInternal(typeof(T)) as T;
        }

        public XComponent[] GetComponents(Type type)
        {
            return this.GetComponentsInternal(type);
        }

        public T[] GetComponents<T>() where T : XComponent
        {
            return this.GetComponentsInternal(typeof(T)) as T[];
        }
    }
}
