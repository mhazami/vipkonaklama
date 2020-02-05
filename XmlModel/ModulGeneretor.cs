using System.IO;

namespace Radyn.ModuleGeneretor
{
    public static class ModuleGeneretor
    {
        public static ModuleXml Generate(Enums.ModuleType type,string path)
        {
            var generate = new ModuleXml();
            ReadDirectory(type,path, path, generate);
            foreach (string file in Directory.EnumerateFiles(path))
            {
                ReadFiles(type,path, file, generate, true);
            }
            return generate;


        }

        private static void ReadDirectory(Enums.ModuleType type, string orginalpath, string path, ModuleXml generate)
        {

            var files = Directory.GetDirectories(path);
            foreach (var subpath in files)
            {
                foreach (string file in Directory.EnumerateFiles(subpath))
                {
                    ReadFiles(type,orginalpath, file, generate, false);
                }
                ReadDirectory(type,orginalpath, subpath, generate);
            }
        }

        private static void ReadFiles(Enums.ModuleType type, string orginalpath, string path, ModuleXml generate, bool root)
        {
            var extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension)) return;
            switch (extension)
            {
                case ".sql":
                    generate.DBModel.DBs.Add(new PathXml() { Path = path.Replace(orginalpath, "").Replace("\\", "/") });
                    break;
                case ".dll":
                    generate.DllModel.Dlls.Add(new PathXml() { Path = path.Replace(orginalpath, "").Replace("\\", "/") });
                    break;
                case ".xml":
                    if (root)
                    {
                        var fileName = Path.GetFileName(path);
                        if (fileName != null)
                        {
                            if (fileName.ToLower() == string.Format("{0}.{1}", type.ToString().ToLower(), "xml"))
                                generate.xmlpath.Path = path.Replace(orginalpath, "").Replace("\\", "/");
                        }
                       
                    }
                    break;
                default:
                    generate.ResourcesModel.Resources.Add(new PathXml() { Path = path.Replace(orginalpath, "").Replace("\\", "/") });
                    break;
            }
        }
    }
}
