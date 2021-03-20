using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
//using System.Threading.Tasks;
using System.Windows.Forms;

namespace TreeView
{
    public enum CRUD { Create, Read, Update, Delete };
    public partial class Form1 : Form
    {
        private Dictionary<string, string> ChildTable = new Dictionary<string, string>() { {"People","Student" },{ "Student", "card" },{ "card", "card" } };
        static string appPath = Application.StartupPath.Replace("\\TreeView\\bin\\Debug", ""); //чтобы можно было дб хранить не в bin
        private string str = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={appPath}\Lab.mdf;Integrated Security=True;";
        public Form1()
        {
            InitializeComponent();
        }
        
        private void Form1_Load(object sender, EventArgs e)
        {
            createToolStripMenuItem.Click += CreateNode;
            readToolStripMenuItem.Click += ReadNode;
            updateToolStripMenuItem.Click += UpdateNode;
            deleteToolStripMenuItem.Click += DeleteNode;
            refreshButton_Click(new object(), new EventArgs());
        }
        
        
        #region Refresh_Load
        private void refreshButton_Click(object sender, EventArgs e) //кнопка обновить что снизу
        {
            treeView1.Nodes.Clear();
            using (var conn = new SqlConnection())
            {
                conn.ConnectionString = str;
                conn.Open();
                SqlCommand command = new SqlCommand("SELECT [Id], [Name] FROM [People]", conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int id = int.Parse(reader["Id"].ToString());
                        TreeNode node = new TreeNode(reader["Name"].ToString());
                        node.Tag = reader["Id"];
                        node.Name = "People";
                        treeView1.Nodes.Add(node);
                        LoadStudents(id, node);
                    }
                }
            }

        }
        private void LoadStudents(int id, TreeNode node) 
        {
            using(var conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT Id, Name FROM Student Where [IdScience]={id}", conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TreeNode tree = new TreeNode(reader["Name"].ToString(),1,1);
                        tree.Tag = reader["Id"];
                        tree.Name = "Student";
                        node.Nodes.Add(tree);
                        Loadcard(int.Parse(reader["Id"].ToString()), tree);
                    }
                }
            }
        }
        private void Loadcard(int id, TreeNode node)
        {
            using (var conn = new SqlConnection(str))
            {
                conn.Open();
                SqlCommand command = new SqlCommand($"SELECT Id, Name FROM card Where [StudentId]={id}", conn);
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        TreeNode tree = new TreeNode(reader["Name"].ToString(), 1, 1);
                        tree.Tag = reader["Id"];
                        tree.Name = "card";
                        node.Nodes.Add(tree);
                    }
                }
            }
        }
        #endregion


        #region Create
        private void CreateChild(string name)
        {
            using (var conn = new SqlConnection(str))
            {
                conn.Open();
                string table = ChildTable[treeView1.SelectedNode.Name];
                int parentId = int.Parse(treeView1.SelectedNode.Tag.ToString());
                SqlCommand command = new SqlCommand($@"INSERT INTO {table} VALUES (@name, '{parentId}')", conn);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
                Loadcard(parentId, treeView1.SelectedNode);  //обновляем только ту часть куда добавляем

            }
        }

        private void Create(string name)
        {
            using (var conn = new SqlConnection(str))
            {
                conn.Open();
                int parentId;
                if (treeView1.SelectedNode.Parent != null)
                    parentId = int.Parse(treeView1.SelectedNode.Parent.Tag.ToString());
                else
                    parentId = 0;
                SqlCommand command = new SqlCommand($@"INSERT INTO {treeView1.SelectedNode.Name} VALUES (@name, '{parentId}')", conn);
                command.Parameters.AddWithValue("@name", name);
                command.ExecuteNonQuery();
                LoadStudents(parentId, treeView1.SelectedNode);  //обновляем только ту часть куда добавляем

            }
        }
        private void CreateNode(object sender, EventArgs e)
        {
            NameForm form = new NameForm(CRUD.Create);
            //form.textBox1.Text = treeView1.SelectedNode.Text; //лучше оставить только на обновление
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                string name = form.NameAtte;
                if (form.child == true)
                {
                    CreateChild(name);
                }
                else
                {
                    Create(name);
                }
                //refreshButton_Click(new object(), new EventArgs());
            } 
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }
        #endregion

        private void ReadNode(object sender, EventArgs e)
        {        } //не знаю зачем это создал, текст везде и так видно

        private void UpdateNode(object sender, EventArgs e)
        {
            NameForm form = new NameForm(CRUD.Update);
            form.textBox1.Text = treeView1.SelectedNode.Text; //текст вершины в текстбокс
            var result = form.ShowDialog();
            if (result == DialogResult.OK)
            {
                string name = form.NameAtte;
                using (var conn = new SqlConnection(str))
                {
                    conn.Open();
                    int id = int.Parse(treeView1.SelectedNode.Tag.ToString());
                    SqlCommand command = new SqlCommand($@"UPDATE {treeView1.SelectedNode.Name} SET Name=@name WHERE Id={id}", conn);
                    command.Parameters.AddWithValue("@name", name);
                    command.ExecuteNonQuery();
                }
                refreshButton_Click(new object(), new EventArgs());
            }
            else if (result == DialogResult.Cancel)
            {
                return;
            }
        }

        private void DeleteNode(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode == null)
                return;
            using (SqlConnection conn = new SqlConnection(str))
            {
                conn.Open();
                int id = int.Parse(treeView1.SelectedNode.Tag.ToString());
                //MessageBox.Show(id.ToString());
                SqlCommand command = new SqlCommand($"DELETE FROM {treeView1.SelectedNode.Name} where Id={id}", conn);
                command.ExecuteNonQuery();
            }
            treeView1.SelectedNode.Remove();
        }
    }
}
