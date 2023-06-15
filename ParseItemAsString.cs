using System;

namespace Test
{
    internal class ParseItemAsString
    {
        private string m_text;

        public ParseItemAsString(string text)
        {
            this.m_text = text;
        }


        internal bool ContainChar(char c)
        {
            return m_text.IndexOf(c) > -1;
        }
        internal bool ContainChar(string c)
        {
            return m_text.Contains(c);
        }

        internal bool EndWithChar(char c)
        {
            if (m_text.Length > 0)
              return   m_text[m_text.Length - 1] == c;
            return false;
        }

        internal void GetText(out string text)
        {
            text = m_text;
        }

        internal string Message()
        {
            return m_text;
        }
    }
}