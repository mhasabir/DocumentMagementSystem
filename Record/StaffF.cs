using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Record
{
    public partial class StaffF : Form
    {

        public StaffF()
        {
            InitializeComponent();
        }
    
        private void staff_document_showbtn_Click(object sender, EventArgs e)
        {
            showPersonalFiles(); 
        }
     
        private void staff_logout_btn_Click(object sender, EventArgs e)
        {
            logOut();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            openFileUsingAnotherSoftware();
        }
        private void button4_Click(object sender, EventArgs e)
        {
            showAllGroupFiles();
        }

        /////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////
        /////////////////////////////////////////////////////////////
        //Other Functions//

        UserCl uobj = new UserCl();
        DocumentCl dobj = new DocumentCl();
        GroupCl gobj = new GroupCl();
        DatabaseCl temp = new DatabaseCl();

        string user, usertype;
        string docName, docPath, docType, downloadPath;
        int showGroupCount = 0;
        int showDocumentCount = 0;

        OpenFileDialog ofd = new OpenFileDialog();

        public void start(string user, string usertype)
        {
            this.usertype = usertype;
            uobj.setuser(user);
            dobj.setUser(user);
            this.user = user;

            label11.Text = uobj.UserName();
            label12.Text = uobj.UserId();
            label13.Text = uobj.department();
            label14.Text = uobj.emailId();


            string picturePath = uobj.showProfilePicture();
            Image img = Image.FromFile(picturePath);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;

            string a = uobj.UserId();

            label8.Text = "";
            label9.Text = "";
            label10.Text = "";

            int b = dobj.getNoOfDoc(user);
            int c = temp.getNoOfGroup(user);
            if (b > 0)
            {
                showPersonalFiles();    
            }
            if (c > 0)
            {
                showAllGroupFiles();
            }
        }
        public void logOut()
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.ShowDialog();
            this.Close();
        }
        public void showAllGroupFiles()
        {
            listView1.Items.Clear();
            listView2.Visible = false;
            listView1.Visible = true;
            int serialNo = 1;
            int c = gobj.getNoOfGroup(user);
            string[] infos = new string[7];
            string[] groupIds = new string[c];
            for (int i = 0; i < c; i++)
            {
                groupIds[i] = temp.userAndGroupTableSelect(user);
            }

            for (int j = 0; j < c; j++)
            {
                int a = temp.getTotalDocument(groupIds[j]);

                for (int i = 0; i < a; i++)
                {
                    string docId = temp.documentAndGroupTableSelect(groupIds[j]);

                    dobj.setDocument(docId);

                    infos[0] = serialNo.ToString();
                    infos[1] = temp.getGroupName(groupIds[j]);
                    infos[2] = temp.getDocName(docId);
                    infos[3] = temp.getDocType(docId);
                    infos[4] = temp.getFilingPath(docId);
                    infos[5] = temp.getDocCheckInTime(docId);
                    infos[6] = temp.getDocSize(docId);

                    ListViewItem item2 = new ListViewItem(infos[0]);

                    item2.SubItems.Add(infos[1]);
                    item2.SubItems.Add(infos[2]);
                    item2.SubItems.Add(infos[3]);
                    item2.SubItems.Add(infos[4]);
                    item2.SubItems.Add(infos[5]);
                    item2.SubItems.Add(infos[6]);

                    listView1.Items.Add(item2);
                    serialNo++;

                }
                DatabaseCl.usergroupid = 0;
                DatabaseCl.docgroupid = 0;

            }
        }

        public void showPersonalFiles()
        {
            listView2.Items.Clear();
            listView1.Visible = false;
            listView2.Visible = true;
            
            int a = dobj.getNoOfDoc(user);
            string[] docIds = new string[a];

            string[] info = new string[6];

            for (int i = 0; i < a; i++)
            {
                string ab;

                docIds[i] = temp.userAndDocumentTableSelect(user);
                ab = temp.documentTableSelect(docIds[i]);

                info[0] = (i + 1).ToString();
                string name = temp.getDocName();

                info[1] = name;
                info[2] = temp.getDocType(docIds[i]);
                info[3] = temp.getFilingPath(docIds[i]);
                info[4] = temp.getDocCheckInTime(docIds[i]);
                info[5] = temp.getDocSize(docIds[i]);
                ListViewItem item = new ListViewItem(info[0]);

                item.SubItems.Add(info[1]);
                item.SubItems.Add(info[2]);
                item.SubItems.Add(info[3]);
                item.SubItems.Add(info[4]);
                item.SubItems.Add(info[5]);
                listView2.Items.Add(item);

            }
            DatabaseCl.userdocid = 0;
        }
        public void openFileUsingAnotherSoftware()
        {

            string selectedDocName = listBox1.SelectedItem.ToString();
            int a = temp.getNoofdocument(user);
            int b = 0;
            for (int i = 0; i < a; i++)
            {
                string docId = temp.userAndDocumentTableSelect(user);
                string docName = temp.getDocName(docId);
                if (docName.Equals(selectedDocName))
                {
                    string docFilingPath = temp.getFilingPathByDocId(docId);
                    ProcessCl pobj = new ProcessCl();
                    pobj.openFileToShow(docFilingPath);
                    break;

                }
                b++;
            }
            if (b > a)
                label8.Text = "File Format is not supported";

            DatabaseCl.userdocid = 0;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectDownloadLocation();
        }
        public void selectDownloadLocation()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string targetPath;
            int index = listBox1.SelectedIndex;
            string docId = temp.userAndDocumentTableSelectByListBoxIndex(user, index);
            DatabaseCl.userdocidforsharelist = 0;
            docId = temp.documentTableSelect(docId);
            string sourcePath = temp.getFilingPath(docId);

            if (result == DialogResult.OK)
            {
                targetPath = fbd.SelectedPath;
                downloadPath = targetPath;
                textBox1.Text = targetPath;
                //  File.Copy(sourcePath, targetPath, true);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            downloadFile();
        }

        public void downloadFile()
        {

            string selectedfile = listBox1.SelectedItem.ToString();
            string docId = temp.documentTableSelectByDocName(selectedfile, user);
            string docfilingpath = temp.getFilingPath(docId);
            downloadPath = textBox1.Text;
            downloadPath = downloadPath + "\\" + selectedfile;
            dobj.saveDocumentInHardDrive(docfilingpath, @downloadPath);
            label9.Text = "Download Complete";
            label10.Text = "Path: " + downloadPath;
        }


    }
}
