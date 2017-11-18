namespace BackstageRun
{
    public class Task
    {
        private string _TASKID;
        private string _BR_ID;
        private string _DATA1;
        private string _DATA2;
        private string _DATA3;
        private string _DATA4;
        private string _DATA5;
        private string _DATA6;
        private string _DATA7;
        private string _BEGINDATE;
        private string _CREATEDATE;


        public string TASKID
        {
            get
            {
                if (_TASKID == null)
                    return string.Empty;
                else
                    return _TASKID;
            }
            set { _TASKID = value; }
        }

        public string BR_ID
        {
            get
            {
                if (_BR_ID == null)
                    return string.Empty;
                else
                    return _BR_ID;
            }
            set { _BR_ID = value; }
        }

        public string DATA1
        {
            get
            {
                if (_DATA1 == null)
                    return string.Empty;
                else
                    return _DATA1;
            }
            set { _DATA1 = value; }
        }

        public string DATA2
        {
            get
            {
                if (_DATA2 == null)
                    return string.Empty;
                else
                    return _DATA2;
            }
            set { _DATA2 = value; }
        }

        public string DATA3
        {
            get
            {
                if (_DATA3 == null)
                    return string.Empty;
                else
                    return _DATA3;
            }
            set { _DATA3 = value; }
        }

        public string DATA4
        {
            get
            {
                if (_DATA4 == null)
                    return string.Empty;
                else
                    return _DATA4;
            }
            set { _DATA4 = value; }
        }

        public string DATA5
        {
            get
            {
                if (_DATA5 == null)
                    return string.Empty;
                else
                    return _DATA5;
            }
            set { _DATA5 = value; }
        }

        public string DATA6
        {
            get
            {
                if (_DATA6 == null)
                    return string.Empty;
                else
                    return _DATA6;
            }
            set { _DATA6 = value; }
        }

        public string DATA7
        {
            get
            {
                if (_DATA7 == null)         
                    return string.Empty;
                else
                    return _DATA7;
            }
            set { _DATA7 = value; }
        }

        public string BEGINDATE
        {
            get
            {
                if (_BEGINDATE == null)
                    return string.Empty;
                else
                    return _BEGINDATE;
            }
            set { _BEGINDATE = value; }
        }

        public string CREATEDATE
        {
            get
            {
                if (_CREATEDATE == null)
                    return string.Empty;
                else
                    return _CREATEDATE;
            }
            set { _CREATEDATE = value; }
        }
    }
}
