using System;
using System.Collections.Generic;
using System.Linq;

namespace Test
{
    internal class RawMessageToParse
    {
        private string m_rawMessage;
        public string m_message;
        public bool m_hasTimeInMessage;
        public DateTime m_whenToExecute;

        public RawMessageToParse(string rawMessage)
        {
            this.m_rawMessage = rawMessage.Trim().ToLower() ;

            m_hasTimeInMessage = false;
            m_whenToExecute = DateTime.Now;
            string[] tokens = m_rawMessage.Split(":");

            if (m_rawMessage.IndexOf("tms:") == 0 && tokens.Length > 2)
            {


                TimeInMilliseconds milliseconds = new TimeInMilliseconds(50);
                TimeOfDayToExecute timeOfDay = new TimeOfDayToExecute(DateTime.Now, milliseconds);
                timeOfDay.GetTime(out m_whenToExecute);
                m_hasTimeInMessage = true;
                if (tokens.Length == 3)
                {
                    m_message = tokens[2];
                }
                else
                {
                    List<string> m = tokens.ToList();
                    m.RemoveAt(0);
                    m.RemoveAt(0);

                    m_message = string.Join(":", tokens);

                }
            }
            else if (m_rawMessage.IndexOf("t:") == 0 && tokens.Length > 2)
            {
                //t:24-60-60-999
                string[] timeToken = tokens[1].Split("-");
                int hours = 0, minutes = 0, seconds = 0, milliseconds = 0;
                if (timeToken.Length >= 1 && int.TryParse(timeToken[0], out hours)) { }
                if (timeToken.Length >= 2 && int.TryParse(timeToken[1], out minutes)) { }
                if (timeToken.Length >= 3 && int.TryParse(timeToken[2], out seconds)) { }
                if (timeToken.Length >= 4 && int.TryParse(timeToken[3], out milliseconds)) { }

                DateTime now = DateTime.Now;
                TimeOfDayToExecute timeOfDay = new TimeOfDayToExecute(new DateTime(now.Year, now.Month, now.Day, hours, minutes, seconds, milliseconds));

                timeOfDay.GetTime(out m_whenToExecute);
                m_hasTimeInMessage = true;
                if (tokens.Length == 3)
                {
                    m_message = tokens[2];
                }
                else
                {
                    List<string> m = tokens.ToList();
                    m.RemoveAt(0);
                    m.RemoveAt(0);

                    m_message = string.Join(":", tokens);

                }
            }
            else {

                m_hasTimeInMessage = false;
                m_whenToExecute = DateTime.Now;
                m_message = m_rawMessage;

            }
        }

        public void GetTimeOfDayToExecute(out bool hasTimeInMessage, out DateTime whenToExecute)
        {

            hasTimeInMessage = m_hasTimeInMessage;
            whenToExecute = m_whenToExecute;
;
        }

        public void GetMessageAsString(out string message)
        {
            message= m_message;
        }
    }
}