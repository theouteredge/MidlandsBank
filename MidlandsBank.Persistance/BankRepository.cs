using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MidlandsBank.Domain;
using Newtonsoft.Json;

namespace MidlandsBank.Persistance
{
    public class BankRepository
    {
        private const string JsonFile = "bank.json";
        private readonly JsonSerializerSettings _settings = new JsonSerializerSettings() { TypeNameHandling = TypeNameHandling.All };

        public Bank Get()
        {
            if (File.Exists(JsonFile))
            {
                var json = File.ReadAllText(JsonFile);
                return JsonConvert.DeserializeObject<Bank>(json, _settings);
            }

            return new Bank();
        }

        public void Set(Bank bank)
        {
            var json = JsonConvert.SerializeObject(bank, Formatting.Indented, _settings);
            File.WriteAllText(JsonFile, json);
        }
    }
}
