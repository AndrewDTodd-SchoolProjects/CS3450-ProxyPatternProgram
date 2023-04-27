using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    internal class SecureDB : IDatabaseProxy
    {
        private readonly IDatabase _database;
        private readonly IDatabase _credentialsDB;
        private bool _authenticated = false;

        public SecureDB(IDatabase databe, IDatabase credentialsDB) => (_database, _credentialsDB) = (databe, credentialsDB);

        public bool Exists(string key)
        {
            if (!_authenticated)
                Authenticate();

            return _database.Exists(key);
        }

        public string Get(string key)
        {
            if (!_authenticated)
                Authenticate();

            return _database.Get(key);
        }

        public string GetID()
        {
            return _database.GetID();
        }

        private void Authenticate()
        {
            Console.Write("Enter Username: ");
            string? userName = Console.ReadLine();
            Console.Write("Enter Password: ");
            string? password = Console.ReadLine();

            if (!_credentialsDB.Exists(userName ?? ""))
                throw new ArgumentException("Invalid Credentials. Access Denied!!!");

            if (_credentialsDB.Get(userName ?? "") != password)
                throw new ArgumentException("Invalid Credentials. Access Denied!!!");

            _authenticated = true;
        }
    }
}
