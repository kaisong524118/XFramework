namespace XFramework
{
    public class XObject
    {
        static long instanceIDCount = 0;


        private long    m_InstanceID = instanceIDCount++;
        private string  m_Name;
        public long InstanceID
        {
            get { return m_InstanceID; }
        }

        public string Name
        {
            get { return m_Name; }
            set { m_Name = value; }
        }
    }
}
