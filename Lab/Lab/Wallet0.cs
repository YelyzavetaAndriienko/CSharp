namespace LI.CSharp.Lab
{
    public class Wallet0
    {
        public string Name { get; set; }
        public decimal Balance { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Balance})";
        }
    }
}