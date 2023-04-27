using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProxyPattern
{
    internal class CacheDB : IDatabaseProxy
    {
        private readonly IDatabase _database;

        private Dictionary<string, string> _cache = new(5);

        public CacheDB(IDatabase database) => _database = database;

        public bool Exists(string key)
        {
            if (_cache.TryGetValue(key, out string? value))
            {
                _cache.Remove(key);
                _cache = new(_cache.Prepend(new(key, value)));
                Console.WriteLine($"Found key \"{key}\" in cache");

                return true;
            }

            return _database.Exists(key);
        }

        public string Get(string key)
        {
            if(_cache.TryGetValue(key, out string? value))
            {
                _cache.Remove(key);
                _cache = new(_cache.Prepend(new(key, value)));
                Console.WriteLine($"Found key \"{key}\" in cache");

                return value;
            }

            try
            {
                value = _database.Get(key);

                if(_cache.Count == 5)
                    _cache.Remove(_cache.Last().Key);

                _cache = new(_cache.Prepend(new(key, value)));
            }
            catch(ArgumentException argEx)
            {
                Console.WriteLine(argEx.Message);
            }

            return value ?? "";
        }

        public string GetID()
        {
            return _database.GetID();
        }

        public string Inspect()
        {
            StringBuilder stringBuilder = new();

            foreach(var item in _cache)
            {
                stringBuilder.Append("[" + item.Key + "], ");
            }

            return stringBuilder.Append(Environment.NewLine).ToString();
        }
    }
}
