using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
    internal class TextParseToActions
    {
        private XBoxActionStack m_waitingActions;
        public  int m_millisecondsBetweenPress = 30;

        public static GroupOfAlias<XBoxInputType> m_boolAlias = new GroupOfAlias<XBoxInputType>(
          new EnumAlias<XBoxInputType>(XBoxInputType.TriggerLeft, "tl", "lt", "l2",  "TriggerLeft"),
          new EnumAlias<XBoxInputType>(XBoxInputType.TriggerRight, "tr", "rt", "r2", "TriggerRight"),
          new EnumAlias<XBoxInputType>(XBoxInputType.SideButtonLeft, "sbl",  "l1", "SideButtonLeft"),
          new EnumAlias<XBoxInputType>(XBoxInputType.SideButtonRight, "sbr",  "r1", "SideButtonRight"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ArrowLeft, "al", "ArrowLeft"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ArrowRight, "ar", "ArrowRight"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ArrowDown, "ad", "ArrowDown"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ArrowUp, "au", "ArrowUp"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ButtonDown, "a", "ba","bd", "paddown", "pd", "buttondown"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ButtonRight, "b", "bb", "br", "padright", "pr", "ButtonRight"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ButtonLeft, "x", "bx", "bl", "padleft", "pl", "ButtonLeft"),
          new EnumAlias<XBoxInputType>(XBoxInputType.ButtonUp, "y", "by", "bu", "padup", "pu", "ButtonUp"),
          new EnumAlias<XBoxInputType>(XBoxInputType.MenuLeft, "m", "ml", "menu", "b", "back", "MenuLeft"),
          new EnumAlias<XBoxInputType>(XBoxInputType.MenuRight, "s", "mr", "start", "buttoMenuRightndown"),
          new EnumAlias<XBoxInputType>(XBoxInputType.JoystickLeftButton, "jl", "JoystickLeftButton"),
          new EnumAlias<XBoxInputType>(XBoxInputType.JoystickRightButton, "jr", "JoystickRightButton"),

               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickLeft_Right, "jlr", "jle", "JoystickLeftRight"),
               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickLeft_Up, "jlu", "jln", "JoystickLeftUp"),
               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickLeft_Down, "jld", "jls", "JoystickLeftDown"),
               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickLeft_Left, "jll", "jlw", "JoystickLeftLeft"),
               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickRight_Right, "jrr", "jre", "JoystickRightRight"),
               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickRight_Up, "jru", "jrn", "JoystickRightUp"),
               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickRight_Down, "jrd", "jrs", "JoystickRightDown"),
               new EnumAlias<XBoxInputType>(XBoxInputType.JoystickRight_Left, "jrl", "jrw", "JoystickRightLeft")
  );

        public TextParseToActions(XBoxActionStack m_waitingActions)
        {
            this.m_waitingActions = m_waitingActions;
        }

        public  void TryToAppendParseToWaitingActions(string receivedMessage)
        {
            receivedMessage = receivedMessage.Replace("(", " ( ");
            receivedMessage = receivedMessage.Replace(")", " ) ");
            while (receivedMessage.IndexOf("  ") > -1)
            {
                receivedMessage = receivedMessage.Replace("  ", " ");
            }

            RawMessageToParse message = new RawMessageToParse(receivedMessage);

            message.GetTimeOfDayToExecute(out bool hasTimeInMessage, out DateTime whenToExecute);
            message.GetMessageAsString(out string messageToParse);


            List<ParseItemAsString> parseItemsAsString = new List<ParseItemAsString>();

            string[] spaceTokens = messageToParse.Split(" ");
            for (int i = 0; i < spaceTokens.Length; i++)
            {
                string t = spaceTokens[i].Trim();
                if (t.Length > 0)
                {
                    parseItemsAsString.Add(new ParseItemAsString(t));
                }
            }

            List<ParseItem> parseItems = new List<ParseItem>();

            for (int i = 0; i < parseItemsAsString.Count; i++)
            {
                string m = parseItemsAsString[i].Message();

                string aliasName = m.Substring(0, m.Length - 1);

                if (parseItemsAsString[i].EndWithChar('↓'))
                {
                    m_boolAlias.Get(aliasName, out bool found, out XBoxInputType inputType);
                    if (found && inputType != XBoxInputType.Undefined)
                        parseItems.Add(new ParseItem_PressInput(PressType.Press, inputType));
                }
                else if (parseItemsAsString[i].EndWithChar('↑'))
                {
                    m_boolAlias.Get(aliasName, out bool found, out XBoxInputType inputType);
                    if (found && inputType != XBoxInputType.Undefined)
                        parseItems.Add(new ParseItem_PressInput(PressType.Release, inputType));

                }
                else if (parseItemsAsString[i].EndWithChar('↕'))
                {

                    m_boolAlias.Get(aliasName, out bool found, out XBoxInputType inputType);
                    if (found && inputType != XBoxInputType.Undefined)
                    {
                        parseItems.Add(new ParseItem_PressInput(PressType.Press, inputType));
                        parseItems.Add(new ParseItem_DelayNextItems(m_millisecondsBetweenPress));
                        parseItems.Add(new ParseItem_PressInput(PressType.Release, inputType));
                    }

                }
                else if (
                    parseItemsAsString[i].EndWithChar('>'))
                {
                    if (int.TryParse(aliasName, out int ms))
                    {
                        parseItems.Add(new ParseItem_DelayNextItems(ms));
                    }

                }
                else if (
                    parseItemsAsString[i].ContainChar('↑'))
                { }
                else if (
                    parseItemsAsString[i].ContainChar('↓'))
                { }

            }


            List<TimedXBoxAction> actions = m_waitingActions.GetRefToActionStack();
            DateTime timeCount = whenToExecute;
            for (int i = 0; i < parseItems.Count; i++)
            {
                if (parseItems[i] is ParseItem_DelayNextItems)
                {
                    ParseItem_DelayNextItems item = (ParseItem_DelayNextItems)parseItems[i];
                    timeCount = timeCount.AddMilliseconds(item.GetValueInMilliseconds());
                }
                if (parseItems[i] is ParseItem_PressInput)
                {
                    ParseItem_PressInput item = (ParseItem_PressInput)parseItems[i];
                    // Console.WriteLine(string.Format("{0}| {1}-{2}", timeCount.ToString("yyyy-dd-HH-mm-ss-fff"), item.press, item.inputType));

                    actions.Add(new TimedXBoxAction_ApplyChange(timeCount, item.press, item.inputType));
                }
            }

        }

    }
}