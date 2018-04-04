using System.Collections;
using System.Collections.Generic;

namespace XFramework
{
    public class XComponent : XObject
    {
        private XGameObject     m_XGameObject   = null;
        private bool            m_Enabled       = true;
        private bool            m_isStart = false;

        public virtual void OnAwake()
        {

        }

        public virtual void OnStart()
        {

        }

        public virtual void OnUpdate()
        {

        }

        public virtual void OnLateUpdate()
        {

        }

        public virtual void OnDestory()
        {

        }

        public bool enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }


        public XFramework.XGameObject xGameObject
        {
            get { return m_XGameObject; }
        }

        internal XFramework.XGameObject XGameObject
        {
            get { return m_XGameObject; }
            set { m_XGameObject = value; }
        }

        internal bool isStart
        {
            get { return m_isStart; }
            set { m_isStart = value; }
        }
    }
}
