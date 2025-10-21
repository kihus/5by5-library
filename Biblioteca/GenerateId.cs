namespace Library
{
    internal class GenerateId
    {
        private static int _id = 1;

        public int GetId()
        {
            return _id++;
        }
    }
}
