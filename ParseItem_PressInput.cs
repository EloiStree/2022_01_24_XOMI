namespace Test
{
    internal class ParseItem_PressInput : ParseItem
    {
        public PressType press;
        public XBoxInputType inputType;

        public ParseItem_PressInput(PressType press, XBoxInputType inputType)
        {
            this.press = press;
            this.inputType = inputType;
        }
    }
}