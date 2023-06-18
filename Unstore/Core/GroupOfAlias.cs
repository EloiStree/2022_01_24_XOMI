using System.Collections.Generic;

namespace XOMI.Unstore.Core
{

    [System.Serializable]
    public class GroupOfAlias<T>
    {
        public List<EnumAlias<T>> m_alias = new List<EnumAlias<T>>();
        public static T m_default;

        public GroupOfAlias(params EnumAlias<T>[] alias)
        {
            m_alias.AddRange(alias);
        }

        public void Get(string enumName, out bool enumFound, out T enumType)
        {
            enumType = m_default;
            enumFound = false;
            enumName = enumName.ToLower();

            for (int y = 0; y < m_alias.Count; y++)
            {
                m_alias[y].GetBoolable(enumName, out enumFound, out enumType);
                if (enumFound)
                    return;
            }
        }

    }

}