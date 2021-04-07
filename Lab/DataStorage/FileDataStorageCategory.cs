using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace LI.CSharp.Lab.DataStorage
{
    public class FileDataStorageCategory<TObject> where TObject : class, IStorableCategory
    {
        private static readonly string BaseFolder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "BudgetsStorage", typeof(TObject).Name);

        public FileDataStorageCategory()
        {
            if (!Directory.Exists(BaseFolder))
                Directory.CreateDirectory(BaseFolder);
        }

        public async Task AddOrUpdateAsync(TObject obj)
        {
            if (!Directory.Exists(Path.Combine(BaseFolder)))
                Directory.CreateDirectory(Path.Combine(BaseFolder));
            var stringObj = JsonSerializer.Serialize(obj);
            using (StreamWriter sw = new StreamWriter(Path.Combine(BaseFolder, obj.Guid.ToString("N")), false))
            {
                await sw.WriteAsync(stringObj);
            }
        }

        public async Task<TObject> GetAsync(Guid guid)
        {
            if (!Directory.Exists(Path.Combine(BaseFolder)))
                Directory.CreateDirectory(Path.Combine(BaseFolder));
            string stringObj = null;
            string filePath = Path.Combine(BaseFolder, guid.ToString("N"));

            if (!File.Exists(filePath))
                return null;

            using (StreamReader sw = new StreamReader(filePath))
            {
                stringObj = await sw.ReadToEndAsync();
            }

            return JsonSerializer.Deserialize<TObject>(stringObj);
        }

        public async Task<List<TObject>> GetAllAsync()
        {
            if (!Directory.Exists(Path.Combine(BaseFolder)))
                Directory.CreateDirectory(Path.Combine(BaseFolder));
            var res = new List<TObject>();
            foreach (var file in Directory.EnumerateFiles(Path.Combine(BaseFolder)))
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

        public async Task DeleteAllFiles()
        {
            string[] files = Directory.GetFiles(Path.Combine(BaseFolder));
            foreach (string file in files)
                File.Delete(file);
        }
    }
}