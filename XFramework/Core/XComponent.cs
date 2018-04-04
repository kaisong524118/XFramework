using System.Collections;
using System.Collections.Generic;

namespace XFramework
{
    public class XComponent : XObject
    {
        private XGameObject     m_XGameObject   = null;
        private bool            m_Enabled       = false;
        private bool            m_isStart = false;

        internal XComponent(XGameObject xgameObject)
        {
            this.m_Enabled      = true;
            this.m_XGameObject  = xgameObject;
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

        public bool Enabled
        {
            get { return m_Enabled; }
            set { m_Enabled = value; }
        }

        public XFramework.XGameObject XGameObject
        {
            get { return m_XGameObject; }
        }

        internal bool isStart
        {
            get { return m_isStart; }
            set { m_isStart = value; }
        }
    }
}
