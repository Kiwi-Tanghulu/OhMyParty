namespace OMG.Utility
{
    [System.Serializable]
    public class OptOption<T>
    {
        public T PositiveOption;
        public T NegativeOption;

        public T GetOption(bool decision) => decision ? PositiveOption : NegativeOption;
    }
}