namespace XMixDrixUpsideDown
{
    public class LineDetails
    {
        private eSigns m_Sign;
        private bool m_IsOneSignInLine;
        private int m_CountSign;

        public LineDetails()
        {
            m_Sign = eSigns.Empty;
            m_IsOneSignInLine = true;
            m_CountSign = 0;
        }

        public eSigns Sign 
        { 
            get
            {
                return m_Sign;
            }
            set
            {
                m_Sign = value;
            }
        }

        public bool IsOneSignInLine
        {
            get
            {
                return m_IsOneSignInLine;
            }
        }

        public int CountSign
        {
            get
            {
                return m_CountSign;
            }
        }

        public void UpdateLineDetails(eSigns i_NewSign)
        {
            if(!m_IsOneSignInLine)
            {
                return;
            }

            if(m_CountSign == 0)
            {
                m_Sign = i_NewSign;
                m_CountSign++;
            }
            else if(m_Sign == i_NewSign)
            {
                m_CountSign++;
            }
            else
            {
                m_IsOneSignInLine = false;
            }
        }

    }
}
