using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Record
{
    public partial class StudentF : Form
    {
        public StudentF()
        {
            InitializeComponent();
        }

      
        private void teacher_logoutbtn_Click(object sender, EventArgs e)
        {
            logOut();
        }

        private void teacher_document_showbtn_Click(object sender, EventArgs e)
        {
            showPersonalFiles(); 
        }

        private void teacher_upload_fileopen_btn_Click(object sender, EventArgs e)
        {
            setUpoloadInfo();
        }

        private void teacher_upload_savebtn_Click(object sender, EventArgs e)
        {
            docUpload();
        }

        private void teacher_share_savebtn_Click(object sender, EventArgs e)
        {
            docShare();
        }


        public void start(string user)
        {
            uobj.setuser(user);
            dobj.setUser(user);
            this.user = user;

            label15.Text = uobj.UserName();
            label16.Text = uobj.UserId();
            label17.Text = uobj.department();
            label18.Text = uobj.emailId();

            string picturePath = uobj.showProfilePicture();
            Image img = Image.FromFile(picturePath);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;
            string a = uobj.UserId();


        }

        private void Teacher_documentGroupFileShow_Click(object sender, EventArgs e)
        {
            showAllGroupFiles();
        }

        private void btn_studentCheckoutDownload_Click(object sender, EventArgs e)
        {
            downloadFile();
        }

        private void btn_studentCheckoutDownloadloc_Click(object sender, EventArgs e)
        {
            selectDownloadLocation();
        }

        private void btn_studentCheckoutOpenFile_Click(object sender, EventArgs e)
        {
            openFileUsingAnotherSoftware();
        }

        private void btn_studentDelete_Click(object sender, EventArgs e)
        {
            deletePersonalFile();
        }


        ///////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////
        ////////////////////////////////////////////////////////

        public void start(string user, string usertype)
        {
            this.usertype = usertype;
            uobj.setuser(user);
            dobj.setUser(user);
            this.user = user;

            label15.Text = uobj.UserName();
            label16.Text = uobj.UserId();
            label17.Text = uobj.department();
            label18.Text = uobj.emailId();


            string picturePath = uobj.showProfilePicture();
            Image img = Image.FromFile(picturePath);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.Image = img;

            string a = uobj.UserId();


            label10.Text = "";
            label11.Text = "";
            label22.Text = "";

            label23.Text = "";
            label24.Text = "";
            label14.Text = "";

            int b = dobj.getNoOfDoc(user);
            int c = temp.getNoOfGroup(user);
            if (b > 0)
            {
                shareSelectFileBoxRefresh();
                
                checkoutSelectFileBoxRefresh();
                deletePersonalFileBoxRefresh();

            }
            if (c > 0)
            {

             
                showAllGroupInfo();
            }
        }

        UserCl uobj = new UserCl();
        DocumentCl dobj = new DocumentCl();
        GroupCl gobj = new GroupCl();
        DatabaseCl temp = new DatabaseCl();

        string user, usertype;
        string docName, docPath, docType, downloadPath;
        int showGroupCount = 0;
        int showDocumentCount = 0;

        OpenFileDialog ofd = new OpenFileDialog();
        
        public void showAllGroupFiles()
        {
            listView2.Items.Clear();
            listView1.Visible = false;
            listView2.Visible = true;

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

                    listView2.Items.Add(item2);
                    serialNo++;

                }
                DatabaseCl.usergroupid = 0;
                DatabaseCl.docgroupid = 0;

            }
        }
        public void showPersonalFiles()
        {
            listView1.Items.Clear();
            listView2.Visible = false;
            listView1.Visible = true;
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
                listView1.Items.Add(item);

            }
            DatabaseCl.userdocid = 0;
        }

        public void setUpoloadInfo()
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                Teacher_upload_pathtxt.Text = ofd.FileName;
                Teacher_uploadfile_nametxt.Text = ofd.SafeFileName;

                docName = Teacher_uploadfile_nametxt.Text;
                docPath = Teacher_upload_pathtxt.Text;
                docType = teacher_upload_doctyoe_com.Text;

            }
        }
        public void docUpload()
        {
            int a = temp.getNoofdocument(user);
            int docExists = 0;
            for (int i = 0; i < a; i++)
            {
                string documentName = temp.userAndDocumentTableSelectAndReturnDocName(user);
                if (documentName.Equals(docName))
                {
                    label10.Text = "Document Already Exists";
                    docExists = 1;
                }
                else
                {
                    continue;
                }
            }
            if (docExists == 0)
            {
                double size = new FileInfo(docPath).Length;
                string path = @"X:\Project\DRMS-Files";
                path = path + "\\" + usertype + "\\" + user + "\\" + docName;
                dobj.saveDocumentInHardDrive(docPath, path);
                dobj.saveDocument(docName, path, docType, size);
                label10.Text = "Document is Saved";
                label11.Text = "Path: " + path;
                showDocumentCount++;
                shareSelectFileBoxRefresh();
                deletePersonalFileBoxRefresh();
                checkoutSelectFileBoxRefresh();
                teacher_upload_doctyoe_com.SelectedIndex = -1;
                Teacher_upload_pathtxt.Clear();
                Teacher_uploadfile_nametxt.Clear();
            }
            DatabaseCl.userdocidforsharelist = 0;
        }
        public void shareSelectFileBoxRefresh()
        {
            //initialization of sharetab's List Box

            int b = dobj.getNoOfDoc(user);
            List<string> items = new List<string>();
            for (int i = 0; i < b; i++)
            {
                //string value = temp.userAndDocumentTableSelectAndReturnDocName(user);
                //value = i + ". " + value;
                // items.Add(value);
                items.Add(temp.userAndDocumentTableSelectAndReturnDocName(user));
            }
            listBox1.DataSource = items;
            DatabaseCl.userdocidforsharelist = 0;

        }
        public void checkoutSelectFileBoxRefresh()
        {
            //initialization of sharetab's List Box

            int b = dobj.getNoOfDoc(user);
            List<string> items = new List<string>();
            for (int i = 0; i < b; i++)
            {
                items.Add(temp.userAndDocumentTableSelectAndReturnDocName(user));
            }
            listBox1.DataSource = items;
            DatabaseCl.userdocidforsharelist = 0;

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
                label22.Text = "File Format is not supported";

            DatabaseCl.userdocid = 0;
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
        public void downloadFile()
        {

            string selectedfile = listBox1.SelectedItem.ToString();
            string docId = temp.documentTableSelectByDocName(selectedfile, user);
            string docfilingpath = temp.getFilingPath(docId);
            downloadPath = textBox1.Text;
            downloadPath = downloadPath + "\\" + selectedfile;
            dobj.saveDocumentInHardDrive(docfilingpath, @downloadPath);
            label22.Text = "Download Complete";
            label23.Text = "Path: " + downloadPath;
        }
        public void docShare()
        {

                string selectedDocName = listBox3.SelectedItem.ToString();
                int index = listBox3.SelectedIndex;

                string docId = temp.userAndDocumentTableSelectByListBoxIndex(user, index);
                DatabaseCl.userdocidforsharelist = 0;
                string individualsId = Teacher_share_filenametxt.Text;
                dobj.shareDocument(individualsId, docId);
                label14.Text = "Shared Completed";

            DatabaseCl.usergroupid = 0;
            

        }
        public void showAllGroupInfo()
        {
            listView3.Items.Clear();
            int b = gobj.getNoOfGroup(user);
            string groupId;
            string[] info = new string[5];

            for (int i = 0; i < b; i++)
            {
                groupId = gobj.getGroupId(user);
                temp.GroupTableSelect(groupId);
                info[0] = (i + 1).ToString();
                info[1] = temp.getGroupName(groupId);
                info[2] = temp.getAdminId(groupId);
                info[3] = temp.getTotalMember(groupId).ToString();
                info[4] = temp.getTotalDocument(groupId).ToString();


                ListViewItem item = new ListViewItem(info[0]);

                item.SubItems.Add(info[1]);
                item.SubItems.Add(info[2]);
                item.SubItems.Add(info[3]);
                item.SubItems.Add(info[4]);

                listView3.Items.Add(item);

            }
            
            DatabaseCl.usergroupid = 0;
        }
        public void deletePersonalFile()
        {
            int index = listBox2.SelectedIndex;

            string selecteddocname = listBox2.SelectedItem.ToString();
            if (index > -1)
            {
                string docId = temp.userAndDocumentTableSelectByListBoxIndex(user, index);
                DatabaseCl.userdocidforsharelist = 0;
                
                int totalShareIndividual = temp.getTotalShareIndividual(docId);

                for (int i = 0; i < totalShareIndividual; i++)
                {
                    string userId = temp.deleteFromUserAndDocumentTableByDocId(docId);
                    temp.udateNoOfDocumentAfterDeletingAfile(userId);
                }
                temp.deleteFromDocumentTable(docId);
                label24.Text = "Deletion Successful";
                deletePersonalFileBoxRefresh();
            }
        }
        public void logOut()
        {
            this.Hide();
            Form1 ob = new Form1();
            ob.ShowDialog();
            this.Close();
        }
        public void deletePersonalFileBoxRefresh()
        {
            //initialization of sharetab's List Box

            int b = dobj.getNoOfDoc(user);
            List<string> items = new List<string>();
            for (int i = 0; i < b; i++)
            {
                items.Add(temp.userAndDocumentTableSelectAndReturnDocName(user));
            }
            listBox2.DataSource = items;
            DatabaseCl.userdocidforsharelist = 0;

        }
    }
}
