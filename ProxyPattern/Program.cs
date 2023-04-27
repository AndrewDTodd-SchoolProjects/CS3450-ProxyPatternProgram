namespace ProxyPattern
{
    internal class Program
    {
        static void Main()
        {
            Database db = new("db.dat");
            Test(db);

            Database userdb = new("userdb.dat");
            SecureDB securedb = new(db, userdb);
            Test(securedb);

            CacheDB cdb = new(securedb);
            Test(cdb);
            Console.WriteLine(cdb.Inspect());

            try
            {
                Database db2 = new("noname.dat");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.WriteLine("Press Any Key to Exit");
            Console.ReadKey();
        }

        static void Test(IDatabase db)
        {
            try
            {
                Console.WriteLine(db.Get("one"));
                Console.WriteLine(db.Get("two"));
                Console.WriteLine(db.Exists("two")); // Call to exists
                Console.WriteLine(db.Get("three"));
                Console.WriteLine(db.Get("four"));
                Console.WriteLine(db.Get("four"));
                Console.WriteLine(db.Get("five"));
                Console.WriteLine(db.Get("six"));
                Console.WriteLine(db.Get("one"));
                Console.WriteLine(db.Get("seven"));
            }
            catch (ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }

            Console.WriteLine();
        }
    }
}