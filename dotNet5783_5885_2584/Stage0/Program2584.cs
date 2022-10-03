namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome2584();
            Welcome5885();
            Console.ReadKey();
        }

        private static void Welcome2584()
        {
            Console.WriteLine("Enter your name: ");
            string userName = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application", userName);
        }
        static partial void Welcome5885();
    }
}