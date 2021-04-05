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
            //var entradas = GetFilesRecursive(@"C:\temp\PAYMENT"); // declaracao de array
            /*foreach (string pathProduction in entradas) // dentro de entrada terá diversas entradas
              {
                listBox1.Items.Add(pathProduction);
                MessageBox.Show("ARQUIVO COPIADO!");*
               }*/
            var prdFile = "ABCDEFGHIJK123456789";
            var prdFileExtension = prdFile + ".prd.enc";

            foreach (var file in FileUtil.GetFiles(@"A:\UPP_HOMOLOG\prd_upp\PAYMENT\", prdFileExtension)) WriteLine(file);


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
            DateTime DataAtual = DateTime.Now;

            if (file != null)
            {
                string prd = Path.GetFileName(file);
                string targetPath = $@"C:\DATA_ENC\{prd}";
                File.Copy(file, targetPath);
                listBox1.Items.Add(file);
                //MessageBox.Show($"Arquivo {prd} Copiado com sucesso \npara {targetPath}");

                System.IO.StreamWriter logFiles = new System.IO.StreamWriter(@"C:\DATA_ENC\Log\" + "DownloadedFiles_" + DataAtual.ToString("MMddyyyy") + ".txt", true);
                string dataimportacao = file + " - Arquivo encontrado e copiado às " + DataAtual.ToString("HH:mm:ss");
                string savedPathFiles = String.Format(dataimportacao);
                logFiles.WriteLine(savedPathFiles);
                logFiles.WriteLine("----------------------------------------------------------------------------------------------------------------------------");
                logFiles.Close();
            }
            else
            {
                MessageBox.Show("ARQUIVO NÃO ENCONTRADO NA REDE!");
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void button1_Click(object sender, EventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}
