using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace DirRecursive
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string prdFile; prdFile = textBox1.Text;
            int minprdFile = 16;
            int maxnprdFile = 20;

            if (prdFile != null && prdFile.Length >= minprdFile || prdFile.Length <= maxnprdFile)
            {
                string prdFileExtension = prdFile + ".prd.enc";
                foreach (string file in FileUtil.GetFiles(@"U:\PAYMENT\", prdFileExtension)) WriteLine(file);
            }
            else
            {
                MessageBox.Show("Arquivo não encontrado na rede: " + prdFile);
            }
        }
        public static class FileUtil
        {
            public static IEnumerable<string> GetFiles(string root, string searchPattern)
            {
                var pending = new Stack<string>();
                pending.Push(root);
                while (pending.Count != 0)
                {
                    var path = pending.Pop();
                    string[] next = null;
                    try
                    {
                        next = Directory.GetFiles(path, searchPattern);
                    }
                    catch { }
                    if (next != null && next.Length != 0) foreach (var file in next) yield return file;
                    try
                    {
                        next = Directory.GetDirectories(path);
                        foreach (var subdir in next) pending.Push(subdir);
                    }
                    catch { }
                }
            }
        }
        private void WriteLine(string file)
        {
            DateTime dateNow = DateTime.Now;
            string prd = Path.GetFileName(file);
            string targetPath = $@"C:\DATA_ENC\{prd}";
            File.Copy(file, targetPath);

            if (file != null)
            {
                //MessageBox.Show($"Arquivo {prd} Copiado com sucesso \npara {targetPath}");

                System.IO.StreamWriter logFiles = new System.IO.StreamWriter(@"C:\UPP\CopyProductionFileMX0000\Log\" + "FileTransfer_" + dateNow.ToString("ddMMyyyy") + ".txt", true);
                string dataImport = file + " - Arquivo encontrado e copiado às " + dateNow.ToString("HH:mm:ss");
                string savedPathFiles = String.Format(dataImport);
                logFiles.WriteLine(savedPathFiles);
                logFiles.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
                logFiles.Close();

                listBox1.Items.Add(file);
                textBox1.Clear();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
