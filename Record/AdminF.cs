using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Record
{
    public partial class AdminF : Form
    {
        public AdminF()
        {
            InitializeComponent();
              
        }

        private void Student_logout_Click(object sender, EventArgs e)
        {
            logOut();
        }

        private void Student_upload_show_Click(object sender, EventArgs e)
        {
            showPersonalFiles();           
        }
        
        private void Student_upload_savebtn_Click(object sender, EventArgs e)
        {
            docUpload();
        }
             
        private void shareButton_Click(object sender, EventArgs e)
        {
            docShare();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            groupCration();
        }
        
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            shareSelectFileBoxSelectedIndexChanged();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            shareGroupBoxSelectedIndexChanged();
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            memberAddition();  
        }
      
        private void button5_Click(object sender, EventArgs e)
        {
            showAllGroupFiles();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            changeAdmin();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            selectDownloadLocation(); 
        }
        

        private void button11_Click(object sender, EventArgs e)
        {
            openFileUsingAnotherSoftware();
        }
       

        private void button12_Click(object sender, EventArgs e)
        {
            deletePersonalFile();  
        }
        
        private void button10_Click(object sender, EventArgs e)
        {
            downloadFile();
        }

        
        private void listBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            groupMemberInfo();
        }

        private void listBox8_SelectedIndexChanged(object sender, EventArgs e)
        {
            showEachGroupFile();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            memberDeletion(); 
        }

        

        private void button13_Click(object sender, EventArgs e)
        {
            groupDeletion();
        }

        private void Student_file_openbtn_Click(object sender, EventArgs e)
        {
            setUpoloadInfo();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            deleteGroupFile();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            panel5.Visible = false;
            panel6.Visible = false;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel5.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            panel6.Visible = true;
            panel5.Visible = true;


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

        public void start(string user,string usertype)
        {
            this.usertype=usertype;
            uobj.setuser(user);
            dobj.setUser(user);
            this.user = user;

            label11.Text = uobj.UserName();
            label12.Text = uobj.UserId();
            label13.Text = uobj.department();
            label14.Text = uobj.emailId();


            string picturePath = uobj.showProfilePicture();
            Image img = Image.FromFile(picturePath);
            this.student_pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            student_pictureBox.Image = img;

            string a = uobj.UserId();

            label18.Text = "";
            label17.Text = "";
            label15.Text = "";
            label20.Text = "";
            label22.Text = "";
            label25.Text = "";
            label34.Text = "";
            label38.Text = "";
            label41.Text = "";
            label42.Text = "";
            label43.Text = "";
            label44.Text = "";
            int b = dobj.getNoOfDoc(user);
            int c = temp.getNoOfGroup(user);
            if (b > 0)
            {
                shareSelectFileBoxRefresh();
                shareGroupBoxRefresh();
                checkoutSelectFileBoxRefresh();
                deletePersonalFileBoxRefresh();

            }
            if (c > 0)
            {

                addMemberSelectGroupBoxRefresh();
                MembersSelectGroupBoxRefresh();
                changeAdminSelectGroupBoxRefresh();
                deleteSelectGroupBoxRefresh();
                showAllGroupInfo();
            }
        }

        public void refresh()
        {
            int b = dobj.getNoOfDoc(user);
            int c = temp.getNoOfGroup(user);
            if (b > 0)
            {
                shareSelectFileBoxRefresh();
                shareGroupBoxRefresh();
                checkoutSelectFileBoxRefresh();
                deletePersonalFileBoxRefresh();

            }
            if (c > 0)
            {

                addMemberSelectGroupBoxRefresh();
                MembersSelectGroupBoxRefresh();
                changeAdminSelectGroupBoxRefresh();
                deleteSelectGroupBoxRefresh();
                showAllGroupInfo();
            }
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
        public void deletePersonalFileBoxRefresh()
        {
            //initialization of sharetab's List Box

            int b = dobj.getNoOfDoc(user);
            List<string> items = new List<string>();
            for (int i = 0; i < b; i++)
            {
                items.Add(temp.userAndDocumentTableSelectAndReturnDocName(user));
            }
            listBox7.DataSource = items;
            DatabaseCl.userdocidforsharelist = 0;

        }
        public void shareGroupBoxRefresh()
        {
            //initialization of sharetab's ComboBox

            int c = gobj.getNoOfGroup(user);
            List<string> items1 = new List<string>();
            items1.Add("Individual");
            for (int i = 0; i < c; i++)
            {
                items1.Add(temp.UserAndGroupTableSelectAndReturnGroupNames(user));
            }

            listBox2.DataSource = items1;

            DatabaseCl.usergroupid = 0;

            if (listBox2.Items.Count > 0)
                listBox2.SelectedIndex = 0;

        }
        public void addMemberSelectGroupBoxRefresh()
        {
            //initialization of sharetab's ComboBox

            int c = gobj.getNoOfGroup(user);
            List<string> items1 = new List<string>();

            for (int i = 0; i < c; i++)
            {
                items1.Add(temp.UserAndGroupTableSelectAndReturnGroupNames(user));
            }
            listBox3.DataSource = items1;

            DatabaseCl.usergroupid = 0;

        }

        public void deleteSelectGroupFileBoxRefresh()
        {
            showEachGroupFile();      
        }
        public void MembersSelectGroupBoxRefresh()
        {
            //initialization of sharetab's ComboBox

            int c = gobj.getNoOfGroup(user);
            List<string> items1 = new List<string>();

            for (int i = 0; i < c; i++)
            {
                items1.Add(temp.UserAndGroupTableSelectAndReturnGroupNames(user));
            }

            listBox4.DataSource = items1;
            string a = items1[0];
            showGroupMembers(a);
            DatabaseCl.usergroupid = 0;

        }
        public void changeAdminSelectGroupBoxRefresh()
        {
            //initialization of sharetab's ComboBox

            int c = gobj.getNoOfGroup(user);
            List<string> items1 = new List<string>();

            for (int i = 0; i < c; i++)
            {
                string groupId = temp.userAndGroupTableSelect(user);
                string adminId = temp.GroupTableSelectByGroupIdAndReturnAdminId(groupId);
                if (adminId.Equals(user))
                {
                    items1.Add(temp.getGroupName(groupId));
                }
                else
                    continue;

            }
            listBox5.DataSource = items1;

            DatabaseCl.usergroupid = 0;

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
            listBox6.DataSource = items;
            DatabaseCl.userdocidforsharelist = 0;

        }

        public void deleteSelectGroupBoxRefresh()
        {
            //initialization of sharetab's ComboBox

            int c = gobj.getNoOfGroup(user);
            List<string> items1 = new List<string>();

            for (int i = 0; i < c; i++)
            {
                string groupId = temp.userAndGroupTableSelect(user);
                string adminId = temp.GroupTableSelectByGroupIdAndReturnAdminId(groupId);
                if (adminId.Equals(user))
                {
                    items1.Add(temp.getGroupName(groupId));
                }
                else
                    continue;

            }
            listBox8.DataSource = items1;

            DatabaseCl.usergroupid = 0;

        }
        public void showAllGroupInfo()
        {
            listView1.Items.Clear();
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

                listView1.Items.Add(item);

            }


            labelCGM.Text = "";
            DatabaseCl.usergroupid = 0;
        }

        public void showAllGroupFiles()
        {
            listView3.Items.Clear();
            listView4.Visible = false;
            listView3.Visible = true;

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

                    listView3.Items.Add(item2);
                    serialNo++;

                }
                DatabaseCl.usergroupid = 0;
                DatabaseCl.docgroupid = 0;

            }
        }

        public void groupMemberInfo()
        {
            listView2.Items.Clear();
            string selectedGroupName = listBox4.SelectedItem.ToString();
            string groupId = temp.GroupTableSelectForGroupId(selectedGroupName,user);
            int a = temp.getTotalMember(groupId);
            string[] infos = new string[5];
            for (int i = 0; i < a; i++)
            {
                string userId = temp.userAndGroupTableSelectByGroupId(groupId);
                uobj.setuser(userId);
                infos[4] = (i + 1).ToString();
                infos[0] = userId;
                infos[1] = uobj.UserName();
                infos[2] = uobj.userType();
                infos[3] = uobj.department();

                ListViewItem item1 = new ListViewItem(infos[0]);

                item1.SubItems.Add(infos[1]);
                item1.SubItems.Add(infos[2]);
                item1.SubItems.Add(infos[3]);
                item1.SubItems.Add(infos[4]);

                listView2.Items.Add(item1);

            }
            DatabaseCl.usergroupidtofindgroupmember = 0;
            DatabaseCl.usergroupid = 0;

        }
        public void showGroupMembers(string gName)
        {
            listView2.Items.Clear();
            string selectedGroupName = gName;
            string groupId = temp.GroupTableSelectForGroupId(selectedGroupName,user);
            int a = temp.getTotalMember(groupId);
            string[] infos = new string[5];
            for (int i = 0; i < a; i++)
            {
                string userId = temp.userAndGroupTableSelectByGroupId(groupId);
                uobj.setuser(userId);
                infos[0] = userId;
                infos[1] = uobj.UserName();
                infos[2] = uobj.userType();
                infos[3] = uobj.department();
                infos[4] = (i + 1).ToString();

                ListViewItem item1 = new ListViewItem(infos[0]);

                item1.SubItems.Add(infos[1]);
                item1.SubItems.Add(infos[2]);
                item1.SubItems.Add(infos[3]);
                item1.SubItems.Add(infos[4]);

                listView2.Items.Add(item1);

            }
            DatabaseCl.usergroupidtofindgroupmember = 0;
            DatabaseCl.usergroupid = 0;

        }
        public void showEachGroupFile()
        {
            string selectedGroupName = listBox8.SelectedItem.ToString();
            int noofgroups = temp.getNoOfGroup(user);
            if (noofgroups > 0)
            {
                string groupId = temp.GroupTableSelectForGroupId(selectedGroupName, user);

                int a = temp.getTotalDocument(groupId);
                string info;
                List<string> items = new List<string>();
                for (int i = 0; i < a; i++)
                {
                    string docId = temp.documentAndGroupTableSelect(groupId);

                    //dobj.setDocument(docId);
                    info = temp.getDocName(docId);
                    items.Add(info);
                }
                listBox9.DataSource = items;
               
                DatabaseCl.usergroupid = 0;
                DatabaseCl.docgroupid = 0;

            }
           
        }

        public void deleteGroupFile()
        {
            label41.Text = "";
            int index = listBox8.SelectedIndex;
            int index2 = listBox9.SelectedIndex;
            if (index > -1 && index2 > -1)
            {
                string selectedGroupName = listBox8.SelectedItem.ToString();
                string groupId = temp.GroupTableSelectForGroupId(selectedGroupName, user);

                string selectedFileName = listBox9.SelectedItem.ToString();
                string docId = temp.documentTableSelectByDocNameForGroupDoc(selectedFileName, groupId);

                temp.deleteFromDocumentAndGroupTableAndNoOfDocDecreaseInGroupTable(docId, groupId);

                label41.Text = "Deletion Successful";
                //showAllGroupInfo();
                showEachGroupFile();
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
        public void showPersonalFiles()
        {
            listView4.Items.Clear();
            listView3.Visible = false;
            listView4.Visible = true;
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
                listView4.Items.Add(item);

            }
            DatabaseCl.userdocid = 0;
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
                    label15.Text = "Document Already Exists";
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
                label15.Text = "Document is Saved";
                label43.Text ="Path: " +path;
                showDocumentCount++;
                shareSelectFileBoxRefresh();
                deletePersonalFileBoxRefresh();
                checkoutSelectFileBoxRefresh();
                Student_doctype_combox.SelectedIndex = -1;
                textBox1.Clear();
                textBox2.Clear();
            }
            DatabaseCl.userdocidforsharelist = 0;
        }

        public void setUpoloadInfo()
        {
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = ofd.FileName;
                textBox1.Text = ofd.SafeFileName;

                docName = textBox1.Text;
                docPath = textBox2.Text;
                docType = Student_doctype_combox.Text;

            }
        }

        public void docShare()
        {

            if (listBox2.SelectedItem.ToString().Equals("Individual"))
            {
                string selectedDocName = listBox1.SelectedItem.ToString();
                int index = listBox1.SelectedIndex;

                string docId = temp.userAndDocumentTableSelectByListBoxIndex(user, index);
                DatabaseCl.userdocidforsharelist = 0;
                string individualsId = textBox3.Text;
                dobj.shareDocument(individualsId, docId);
                label18.Text = "Shared Completed";
            }
            else
            {
                int index = listBox1.SelectedIndex;
                string docId = temp.userAndDocumentTableSelectByListBoxIndex(user, index);
                DatabaseCl.userdocidforsharelist = 0;
                string selectedGroupName = listBox2.SelectedItem.ToString();
                string groupId = temp.GroupTableSelectForGroupId(selectedGroupName, user);
                dobj.shareDocumentViaGroup(groupId, docId, user);
                deleteSelectGroupFileBoxRefresh();
                showAllGroupFiles();

                label18.Text = "Shared Completed";
            }
            DatabaseCl.usergroupid = 0;
            showAllGroupInfo();

        }

        public void groupCration()
        {
            string groupName = textBox5.Text;
            int groupExists = 0;
            int a = temp.getNoOfGroup(user);
            for (int i = 0; i < a; i++)
            {
                string gname = temp.UserAndGroupTableSelectAndReturnGroupNames(user);
                if (gname.Equals(groupName))
                {
                    label25.Text = "Group Already Exists";
                    groupExists = 1;
                }
                else
                    continue;
            }
            if (groupExists == 0)
            {
                gobj.createGroup(groupName, user);
                label25.Text = "Group Creation Successful  " + showGroupCount;
                textBox5.Clear();
                textBox6.Clear();
                showGroupCount++;
                shareGroupBoxRefresh();
                addMemberSelectGroupBoxRefresh();
                MembersSelectGroupBoxRefresh();
                changeAdminSelectGroupBoxRefresh();
                deleteSelectGroupBoxRefresh();
                showAllGroupInfo();

            }
            DatabaseCl.usergroupid = 0;
        }

        public void shareSelectFileBoxSelectedIndexChanged()
        {
            string selectedDocName = listBox1.SelectedItem.ToString();
            label18.Text = "Selected File: " + selectedDocName;
        }
        public void shareGroupBoxSelectedIndexChanged()
        {
            if (listBox2.SelectedItem.ToString().Equals("Individual"))
            {
                textBox3.Visible = true;
                label17.Visible = true;
                label17.Text = "Enter User Id";
            }
            else
            {
                textBox3.Visible = false;
                label17.Visible = false;
            }
        }

        public void memberAddition()
        {
            string memberId = textBox4.Text;
            string selectedGroupName = listBox3.SelectedItem.ToString();
            string groupId = temp.GroupTableSelectForGroupId(selectedGroupName, user);
            temp.updateTotalMemberIncrease(groupId);
            temp.userAndGroupTableInsert(memberId, groupId);
            temp.udateNoOfGroupIncrease(memberId);

            label20.Text = "Member Addition Successful";
            showAllGroupInfo();
            MembersSelectGroupBoxRefresh();
            textBox4.Clear();
        }
        public void changeAdmin()
        {
            string groupAdmin = textBox6.Text;
            string groupName = listBox5.SelectedItem.ToString();
            string groupId = temp.GroupTableSelectForGroupId(groupName, user);
            Boolean a = false;
            a = temp.userAndGroupTableCheckByGidAndUId(groupAdmin, groupId);
            if (a == true)
            {
                temp.updateAdminId(groupAdmin, groupId);
                changeAdminSelectGroupBoxRefresh();
                label22.Text = "Change Admin Successful";
                showAllGroupInfo();
            }
            else
                label22.Text = "Admin id Is Not a Member of this Group";


            textBox6.Clear();
        }

        public void selectDownloadLocation()
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            DialogResult result = fbd.ShowDialog();
            string targetPath;
            int index = listBox6.SelectedIndex;
            string docId = temp.userAndDocumentTableSelectByListBoxIndex(user, index);
            DatabaseCl.userdocidforsharelist = 0;
            docId = temp.documentTableSelect(docId);
            string sourcePath = temp.getFilingPath(docId);

            if (result == DialogResult.OK)
            {
                targetPath = fbd.SelectedPath;
                downloadPath = targetPath;
                textBox7.Text = targetPath;
                //  File.Copy(sourcePath, targetPath, true);
            }
        }

        public void openFileUsingAnotherSoftware()
        {

            string selectedDocName = listBox6.SelectedItem.ToString();
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
                label32.Text = "File Format is not supported";

            DatabaseCl.userdocid = 0;
        }

        public void deletePersonalFile()
        {
            int index = listBox7.SelectedIndex;

            string selecteddocname = listBox7.SelectedItem.ToString();
            if (index > -1)
            {
                string docId = temp.userAndDocumentTableSelectByListBoxIndex(user, index);
                DatabaseCl.userdocidforsharelist = 0;
                int totalshareingroup = temp.getTotalShareInGroup(docId);

                for (int i = 0; i < totalshareingroup; i++)
                {
                    temp.deleteFromDocumentAndGroupTableByDocId(docId);
                }
                int totalShareIndividual = temp.getTotalShareIndividual(docId);

                for (int i = 0; i < totalShareIndividual; i++)
                {
                    string userId = temp.deleteFromUserAndDocumentTableByDocId(docId);
                    temp.udateNoOfDocumentAfterDeletingAfile(userId);
                }
                temp.deleteFromDocumentTable(docId);
                label34.Text = "Deletion Successful";
                deletePersonalFileBoxRefresh();
            }
        }

        public void downloadFile()
        {

            string selectedfile = listBox6.SelectedItem.ToString();
            string docId = temp.documentTableSelectByDocName(selectedfile, user);
            string docfilingpath = temp.getFilingPath(docId);
            downloadPath = textBox7.Text;
            downloadPath = downloadPath + "\\" + selectedfile;
            dobj.saveDocumentInHardDrive(docfilingpath, @downloadPath);
            label42.Text = "Download Complete";
            label44.Text="Path: " + downloadPath;
        }

        public void memberDeletion()
        {
            string memberId = textBox4.Text;
            string selectedGroupName = listBox3.SelectedItem.ToString();
            string groupId = temp.GroupTableSelectForGroupId(selectedGroupName, user);
            temp.updateTotalMemberDecrease(groupId);
            temp.deleteFromuserAndGroupTable(memberId, groupId);
            temp.udateNoOfGroupDecrease(memberId);

            label20.Text = "Member Deletion Successful";
            showAllGroupInfo();
            MembersSelectGroupBoxRefresh();
            textBox4.Clear();
        }

        public void groupDeletion()
        {
            string selectedGroupName = listBox3.SelectedItem.ToString();
            string groupId = temp.GroupTableSelectForGroupId(selectedGroupName, user);
            int totalmember = temp.getTotalMember(groupId);
            int totaldocument = temp.getTotalDocument(groupId);
            for (int i = 0; i < totalmember; i++)
            {
                temp.deleteFromuserAndGroupTable(groupId);
            }
            for (int i = 0; i < totaldocument; i++)
            {
                temp.deleteFromdocumentAndGroupTable(groupId);
            }

            label22.Text = "Group Deletion Successful   " + showGroupCount;
            textBox5.Clear();
            textBox6.Clear();
            showGroupCount++;
            shareGroupBoxRefresh();
            addMemberSelectGroupBoxRefresh();
            MembersSelectGroupBoxRefresh();
            changeAdminSelectGroupBoxRefresh();
            deleteSelectGroupBoxRefresh();
            showAllGroupInfo();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            string userid = textBox8.Text;
            string usertype=temp.getUsertype(userid);
            if (usertype.Equals("Student"))
            {
                
                StudentF objSF = new StudentF();
                objSF.start(userid, usertype);
                objSF.ShowDialog();
               
            }
            else if (usertype.Equals("Teacher"))
            {
              
                TeacherF objTF = new TeacherF();
                objTF.start(userid, usertype);
                objTF.ShowDialog();
                
            }
            else if (usertype.Equals("Staff"))
            {
                StaffF objSTF = new StaffF();
                objSTF.start(userid, usertype);
                objSTF.ShowDialog();
                
            }

        }
        /*     public void showEachGroupFile()
             {
                 int c = gobj.getNoOfGroup(user);
                 string infos;
                 string[] groupIds = new string[c];
                 for (int i = 0; i < c; i++)
                 {
                     groupIds[i] = temp.userAndGroupTableSelect(user);
                 }
                 int index = listBox8.SelectedIndex;
                 //if (index > -1)
                 //{
                     int a = temp.getTotalDocument(groupIds[index]);
                     List<string> items = new List<string>();
                     for (int i = 0; i < a; i++)
                     {
                         string docId = temp.documentAndGroupTableSelect(groupIds[index]);

                         dobj.setDocument(docId);
                         infos = temp.getDocName(docId);
                         items.Add(infos);
                     }
                     listBox9.DataSource = items;

                 //}
                     DatabaseCl.usergroupid = 0;
                     DatabaseCl.docgroupid = 0;          
             }*/
    }
}
