namespace Logic
{
    internal class LineDetails
    {
        private eSigns m_Sign;
        private bool m_IsOneSignInLine;
        private int m_CountSign;

        internal LineDetails()
        {
            m_Sign = eSigns.Empty;
            m_IsOneSignInLine = true;
            m_CountSign = 0;
        }

        internal bool IsOneSignInLine
        {
            get
            {
                return m_IsOneSignInLine;
            }
        }

        internal int CountSign
        {
            get
            {
                return m_CountSign;
            }
        }

        internal void UpdateLineDetails(eSigns i_NewSign)
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
