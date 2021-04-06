using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LI.CSharp.Lab.DataStorage
{
    public class FileDataStorage<TObject> where TObject : class, IStorable
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BudgetsStorage", typeof(TObject).Name);

        public FileDataStorage()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public async Task AddOrUpdateAsync(TObject obj)
        {
            if (!Directory.Exists(Path.Combine(BaseFolder, obj.OwnerGuid)))
                Directory.CreateDirectory(Path.Combine(BaseFolder, obj.OwnerGuid));
            var stringObj = JsonSerializer.Serialize(obj);
            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.OwnerGuid, obj.Guid.ToString("N")), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task<TObject> GetAsync(Guid guid, string ownerGuid)
        {
            if (!Directory.Exists(Path.Combine(BaseFolder, ownerGuid)))
                Directory.CreateDirectory(Path.Combine(BaseFolder, ownerGuid));
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, ownerGuid, guid.ToString("N"));

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<TObject>(stringObj);
        }

        public async Task<List<TObject>> GetAllAsync(string ownerGuid)
        {
            if (!Directory.Exists(Path.Combine(BaseFolder, ownerGuid)))
                Directory.CreateDirectory(Path.Combine(BaseFolder, ownerGuid));
            var res = new List<TObject>();
            foreach (var file in Directory.EnumerateFiles(Path.Combine(BaseFolder, ownerGuid)))
            {
                string stringObj = null;

                using (StreamReader sw = new StreamReader(file))
                {
                    stringObj = await sw.ReadToEndAsync();
                }

                res.Add(JsonSerializer.Deserialize<TObject>(stringObj));
            }

            return res;
        }

        public async Task DeleteAllFiles(string ownerGuid)
        {
            string[] files = Directory.GetFiles(Path.Combine(BaseFolder, ownerGuid));
            foreach (string file in files)
                File.Delete(file);
        }
    }
}