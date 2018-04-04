using System;
using System.Collections.Generic;
using System.Text;

namespace XFramework
{
    public class XComponentManager
    {
        private static Dictionary<XGameObject, List<XComponent>> m_XComponentMap = new Dictionary<XGameObject, List<XComponent>>();
        private static Dictionary<long, XComponent> m_StartMap = new Dictionary<long, XComponent>();
        private static XComponent AddGameObjectComponentInternal(XGameObject xgameObject, Type type)
        {
            List<XComponent> components = null;
            if (!m_XComponentMap.TryGetValue(xgameObject, out components))
            {
                //可优化池 先懒一下
                components = new List<XComponent>();
                m_XComponentMap.Add(xgameObject, components);
            }

            XComponent component = (XComponent)Activator.CreateInstance(type);
            component.XGameObject = xgameObject; 
            components.Add(component);
            if (!m_StartMap.ContainsKey(component.GetInstanceID()))
                m_StartMap.Add(component.GetInstanceID(), component);
            component.OnAwake();
            return component;
        }

        internal static XComponent AddGameObjectComponent(XGameObject xgameObject, Type type)
        {
            return AddGameObjectComponentInternal(xgameObject, type);
        }

        internal static XComponent GetGameObjectComponent(XGameObject xgameObject, Type type)
        {
            List<XComponent> components;
            if (!m_XComponentMap.TryGetValue(xgameObject, out components))
                return null;
            int count = components.Count;
            for (int i = 0; i < count; i++)
                if (components[i].GetType() == type)
                    return components[i];
            return null;
        }

        internal static XComponent[] GetGameObjectComponents(XGameObject xgameObject, Type type)
        {
            List<XComponent> components;
            if (!m_XComponentMap.TryGetValue(xgameObject, out components))
                return null;

            List<XComponent> results = null;
            int count = components.Count;
            for (int i = 0; i < count; i++)
                if (components[i].GetType() == type)
                {
                    if (results == null)
                        results = new List<XComponent>();
                    results.Add(components[i]);
                }


            return results != null ? results.ToArray() : null;
        }

        internal static void DestroyGameObjectComponent(XObject obj)
        {
            XGameObject xgo = null;
            XComponent xcom = null;
            if (obj is XGameObject)
            {
                xgo = (XGameObject)obj;
                List<XComponent> components;
                if (!m_XComponentMap.TryGetValue(xgo, out components))
                    throw new Exception(string.Concat("m_XComponentMap :", xgo, " null"));

                int count = components.Count;
                for (int i = 0; i < count; i++)
                {
                    if (m_StartMap.ContainsKey(components[i].GetInstanceID()))
                        m_StartMap.Remove(components[i].GetInstanceID());
                    components[i].OnDestory();
                    components[i].SetDestroy();
                }

                //可回收List 偷个懒
                xgo.SetDestroy();
                m_XComponentMap.Remove(xgo);
            }
            else if (obj is XComponent)
            {
                xcom = (XComponent)obj;
                List<XComponent> components;
                xgo = ((XComponent)obj).xGameObject;
                if (!m_XComponentMap.TryGetValue(xgo, out components))
                    throw new Exception(string.Concat("m_XComponentMap :", xgo, " null"));

                int idx = components.IndexOf(xcom);
                if (idx < 0 || idx >= components.Count)
                    throw new Exception(string.Concat("m_XComponentMap : XComponent", xcom, " null"));

               
                xcom.OnDestory();
                xcom.SetDestroy();
                components.RemoveAt(idx);
                if (m_StartMap.ContainsKey(xcom.GetInstanceID()))
                    m_StartMap.Remove(xcom.GetInstanceID());
            }
        }


        static void StartComponent()
        {
            if (m_StartMap.Count == 0)
                return;

            foreach (var item in m_StartMap)
            {
                if (!item.Value.enabled)
                    continue;

                if (!item.Value.isStart)
                {
                    item.Value.isStart = true;
                    item.Value.OnStart();
                }
            }
        }

        public static void Update()
        {
            if (m_StartMap.Count > 0)
                StartComponent();

            foreach (KeyValuePair<XGameObject, List<XComponent>> item in m_XComponentMap)
            {
                foreach (XComponent component in item.Value)
                {
                    if (!component.enabled)
                        continue;
                    component.OnUpdate();
                }
            }
        }

        internal static void LateUpdate()
        {
            foreach (KeyValuePair<XGameObject, List<XComponent>> item in m_XComponentMap)
            {
                foreach (XComponent component in item.Value)
                {
                    if (!component.enabled)
                        continue;
                    component.OnLateUpdate();
                }
            }
        }
    }
}
