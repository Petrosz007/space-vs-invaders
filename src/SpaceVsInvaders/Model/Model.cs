namespace SpaceVsInvaders.Model
{
    public class SVsIModel
    {
        public int TickCount { get; private set; }

        public int N { get; private set; }
        SVsIModel()
        {
            TickCount = 0;
        }

        void NewGame() {}
    }
}