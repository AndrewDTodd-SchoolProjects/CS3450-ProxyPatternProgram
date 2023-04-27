using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    internal class Database : IDatabase
    {
        private readonly FileInfo _databaseFile;

        public Database(string databaseFileName)
        {
            _databaseFile = new FileInfo(databaseFileName);

            if (!_databaseFile.Exists)
                throw new ArgumentException($"{databaseFileName} does not exist");
        }

        public bool Exists(string key)
        {
            using StreamReader sr = new(_databaseFile.OpenRead());

            string? line = sr.ReadLine();

            while(line != null)
            {
                string[] contents = line.Split(" ");

                if (contents[0] == key)
                    return true;

                line = sr.ReadLine();
            }

            return false;
        }

        public string Get(string key)
        {
            using StreamReader sr = new(_databaseFile.OpenRead());

            string? line = sr.ReadLine();

            while (line != null)
            {
                string[] contents = line.Split(" ");

                if (contents[0] == key)
                    return string.Join(" ", contents[1..]);

                line = sr.ReadLine();
            }

            throw new ArgumentException($"No record with key: {key}");
        }

        public string GetID()
        {
            return _databaseFile.Name;
        }
    }
}
