using System.Windows.Forms;

namespace WinFormsApp2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string rootPath = @"D:\c#-projects\winforms\WinFormsApp2";
            
            ImageList imageList = new ImageList();
            imageList.Images.Add("folderIcon", Image.FromFile(@"C:\Users\Влад\Desktop\folderIcon.png"));
            imageList.Images.Add("fileIcon", Image.FromFile(@"C:\Users\Влад\Desktop\fileIcon.png"));
            imageList.Images.Add("vsIcon", Image.FromFile(@"C:\Users\Влад\Desktop\vsIcon.png"));
            imageList.Images.Add("propsIcon", Image.FromFile(@"C:\Users\Влад\Desktop\propsIcon.png"));
            treeView1.ImageList = imageList;
                       
            TreeNode rootNode = treeView1.Nodes.Add(rootPath);
            rootNode.Tag = rootPath;
            rootNode.Text = Path.GetFileName(rootPath);
            LoadDirectory(rootNode);
        }

        private void LoadDirectory(TreeNode parentNode)
        {
            string path = (string)parentNode.Tag;

            try
            {                
                string[] directories = Directory.GetDirectories(path);
                foreach (string directory in directories)
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(directory);
                    TreeNode node = parentNode.Nodes.Add(dirInfo.Name);
                    node.Tag = directory;
                    
                    node.ImageKey = "folderIcon";
                    node.SelectedImageKey = "folderIcon";

                    LoadDirectory(node);
                }
                
                string[] files = Directory.GetFiles(path);
                foreach (string file in files)
                {
                    FileInfo fileInfo = new FileInfo(file);
                    TreeNode node = parentNode.Nodes.Add(fileInfo.Name);
                    node.Tag = file;

                    string extension = fileInfo.Extension.ToLower();
                    
                    if (extension == ".cs")
                    {
                        node.ImageKey = "vsIcon";
                        node.SelectedImageKey = "vsIcon";
                    }
                    else if (extension == ".props")
                    {
                        node.ImageKey = "propsIcon";
                        node.SelectedImageKey = "propsIcon";
                    }
                    else
                    {
                        node.ImageKey = "fileIcon";
                        node.SelectedImageKey = "fileIcon";
                    }
                }
            }
            catch (Exception ex)
            {                
                MessageBox.Show("Ошибка при загрузке директории: " + ex.Message);
            }
        }
    }
}