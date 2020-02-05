using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Xml;
using Ionic.Zip;
using Radyn.ModuleGeneretor;
using Radyn.Utility;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using MessageBox = System.Windows.MessageBox;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Radyn.ModuleGeneretorWinApp
{

    public partial class MainWindow : Window
    {


        public MainWindow()
        {

            InitializeComponent();


        }



        private XmlDocument _xmlDocument;

        public XmlDocument XmlDocument
        {
            get
            {
                if (_xmlDocument == null)
                    _xmlDocument = new XmlDocument();
                return _xmlDocument;
            }

        }

        public string SelectedPath { get; set; }
        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            drpType.ItemsSource = Radyn.Utility.EnumUtils.ConvertEnumToIEnumerable<Enums.ModuleType>();


        }







        private void MainWindow_OnKeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
                Btngeneratezip_OnClick(null, null);
        }






        private void Btnclose_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }





        private void Btngenereratexml_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (drpType.SelectedValue == null || string.IsNullOrEmpty(drpType.SelectedValue.ToString()))
                    throw new Exception("لطفا نوع ماژول را انتخاب نماید");
                using (var fbd = new FolderBrowserDialog() { ShowNewFolderButton = true, SelectedPath = SelectedPath })
                {
                    DialogResult result = fbd.ShowDialog();
                    SelectedPath = fbd.SelectedPath;
                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(SelectedPath))
                    {
                        var moduleXml = Radyn.ModuleGeneretor.ModuleGeneretor.Generate(drpType.SelectedValue.ToString().ToEnum<Enums.ModuleType>(),SelectedPath);
                        XmlDocument.InnerXml = moduleXml.XmlSeserialize();
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Text documents (.xml)|*.xml";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            File.WriteAllText(saveFileDialog.FileName, XmlDocument.InnerXml, Encoding.UTF8);
                            MessageBox.Show("اطلاعات با موفقیت ذخیره شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Btngeneratezip_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (drpType.SelectedValue == null || string.IsNullOrEmpty(drpType.SelectedValue.ToString()))
                    throw new Exception("لطفا نوع ماژول را انتخاب نماید");
                using (var fbd = new FolderBrowserDialog() { ShowNewFolderButton = true, SelectedPath = SelectedPath })
                {
                    DialogResult result = fbd.ShowDialog();
                    SelectedPath = fbd.SelectedPath;
                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(SelectedPath))
                    {
                        var pathprefix = SelectedPath + "\\";
                        var findxmlurl = SelectedPath;
                        var type = drpType.SelectedValue.ToString();
                        if (drpType.SelectedValue.ToString().ToEnum<Enums.ModuleType>() != Enums.ModuleType.Base)
                            findxmlurl += "\\Areas\\" + drpType.SelectedValue;
                        var firstOrDefault = Directory.EnumerateFiles(findxmlurl, "*.xml").FirstOrDefault(x => x.Replace(findxmlurl + "\\", "").ToLower() == string.Format("{0}.{1}", type.ToLower(), "xml"));
                        if (firstOrDefault == null)
                            throw new Exception(String.Format("xml :{0} not found in directory {1} ", type, SelectedPath));

                        XmlDocument.Load(firstOrDefault);
                        var moduleXml = Radyn.ModuleGeneretor.Serialize.XmlDeserialize<ModuleXml>(XmlDocument.InnerXml);
                        using (ZipFile zip = new ZipFile())
                        {

                            foreach (var VARIABLE in moduleXml.DllModel.Dlls)
                            {
                                zip.AddFile(FileName(pathprefix, VARIABLE.Path), DirectoryPathInArchive(VARIABLE.Path));
                            }
                            foreach (var VARIABLE in moduleXml.ResourcesModel.Resources)
                            {
                                zip.AddFile(FileName(pathprefix, VARIABLE.Path), DirectoryPathInArchive(VARIABLE.Path));
                            }
                            foreach (var VARIABLE in moduleXml.DBModel.DBs)
                            {
                                zip.AddFile(FileName(pathprefix, VARIABLE.Path), @"\");
                            }
                            if (moduleXml.xmlpath != null)
                                zip.AddFile(FileName(pathprefix, moduleXml.xmlpath.Path), @"\");
                            zip.Save(Constast.TemplateAddress);

                        }
                        byte[] readBytes = null;
                        SaveFileDialog saveFileDialog = new SaveFileDialog();
                        saveFileDialog.Filter = "Text documents (.zip)|*.zip";
                        if (saveFileDialog.ShowDialog() == true)
                        {
                            using (var fs = new FileStream(Constast.TemplateAddress, FileMode.Open, FileAccess.Read))
                            {
                                var reader = new BinaryReader(fs);
                                readBytes = reader.ReadBytes((int)fs.Length);
                                fs.Close();
                            }
                            File.WriteAllBytes(saveFileDialog.FileName, readBytes);
                            MessageBox.Show("اطلاعات با موفقیت ذخیره شد", "", MessageBoxButton.OK, MessageBoxImage.Information);
                            System.IO.File.Delete(Constast.TemplateAddress);
                        }
                    }
                }


            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private static string FileName(string pathprefix, string path)
        {
            return pathprefix + path.Replace("//", "\\");
        }

        private static string DirectoryPathInArchive(string path)
        {
            var fileName = Path.GetFileName(path);
            if (string.IsNullOrEmpty(fileName)) return @"\" + path.Replace("//", "\\");
            return @"\" + path.Replace(fileName, "").Replace("//", "\\");

        }
    }
}
