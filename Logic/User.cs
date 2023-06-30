namespace Logic
{
    internal class User
    {
        private string m_Name;
        private bool m_IsComputer;
        private int m_Score;

        internal string Name
        {
            get
            {
                return m_Name;
            }
            set
            {
                m_Name = value;
            }
        }

        internal bool IsComputer
        {
            get
            {
                return m_IsComputer;
            }
            set
            {
                m_IsComputer = value;
            }
        }

        internal int Score
        {
            get
            {
                return m_Score;
            }
        }

        internal void UpdateScore()
        {
            m_Score++;
        }

    }


}
