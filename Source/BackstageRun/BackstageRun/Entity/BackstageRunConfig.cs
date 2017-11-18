namespace BackstageRun
{
    public class BackstageRunConfig
    {
        private string _BR_ID;
        private string _BR_DESC;
        private int _BR_TYPE;
        private int _PARANUM;
        private string _DLLNAME;
        private string _NAMESPACE_CLASS;
        private string _FUNCTION_NAME;
        private int _STEPTIME;

        public string BR_ID 
        {
            get { return _BR_ID; }
            set { _BR_ID = value; }
        }

        public string BR_DESC
        {
            get { return _BR_DESC; }
            set { _BR_DESC = value; }
        }

        public int BR_TYPE
        {
            get { return _BR_TYPE; }
            set { _BR_TYPE = value; }
        }

        public int PARANUM
        {
            get { return _PARANUM; }
            set { _PARANUM = value; }
        }

        public string DLLNAME
        {
            get { return _DLLNAME; }
            set { _DLLNAME = value; }
        }

        public string NAMESPACE_CLASS
        {
            get { return _NAMESPACE_CLASS; }
            set { _NAMESPACE_CLASS = value; }
        }

        public string FUNCTION_NAME
        {
            get { return _FUNCTION_NAME; }
            set { _FUNCTION_NAME = value; }
        }

        public int STEPTIME
        {
            get { return _STEPTIME; }
            set { _STEPTIME = value; }
        }
    }
}
