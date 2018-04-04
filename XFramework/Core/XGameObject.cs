using System;
using System.Collections.Generic;
using System.Text;

namespace XFramework
{
    public class XGameObject : XObject
    {
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



        internal XComponent AddComponentInternal(Type type)
        {
            return XComponentManager.AddGameObjectComponent(this, type);
        }

        internal XComponent GetComponentInternal(Type type)
        {
            return XComponentManager.GetGameObjectComponent(this, type);
        }

        internal XComponent[] GetComponentsInternal(Type type)
        {
            return XComponentManager.GetGameObjectComponents(this, type);
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
