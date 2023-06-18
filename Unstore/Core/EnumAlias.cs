using System.Collections.Generic;

namespace XOMI.Unstore.Core
{

    [System.Serializable]
    public class EnumAlias<T>
    {
        public EnumAlias(T e, params string[] alias)
        {
            m_enum = e;
            m_alias.AddRange(alias);
            m_alias.Add(e.ToString());
        }
        public T m_enum;
        public List<string> m_alias = new List<string>();
        public static T m_default;

        public void GetBoolable(string buttonName, out bool buttonNameFound, out T button)
        {
            button = m_default;
            buttonNameFound = false;
            buttonName = buttonName.ToLower();

            for (int y = 0; y < m_alias.Count; y++)
            {
                if (m_alias[y].ToLower() == buttonName)
                {
                    buttonNameFound = true;
                    button = m_enum;
                    return;
                }
            }
        }
    }
}