namespace XFramework
{
    public class XObject
    {
        static long     InstanceIDCount = 0;
        private string  m_Name;
        private bool    m_IsDestroy = false;
        private long    m_InstanceID = InstanceIDCount++;

        public static void Destroy(XObject obj)
        {
            Destroy(obj,0.0f);
        }

        public static void Destroy(XObject obj,float t = 0.0f)
        {
            XComponentManager.DestroyGameObjectComponent(obj);
        }

        public bool isDestroy
        {
            get { return m_IsDestroy; }
        }

        internal void SetDestroy()
        {
            m_IsDestroy = true;
        }

        public long GetInstanceID()
        {
            return m_InstanceID;
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
    }
}
