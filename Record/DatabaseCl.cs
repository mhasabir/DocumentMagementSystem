using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
namespace Record
{
    class DatabaseCl : IDatabase
    {
        UserTable utemp = new UserTable();
        DocumentTable dtemp = new DocumentTable();
        UserAndDocumentTable udtemp = new UserAndDocumentTable();
        GroupTable gtemp = new GroupTable();
        UserAndGroupTable ugtemp = new UserAndGroupTable();
        DocumentAndGroupTable dgtemp = new DocumentAndGroupTable();

        string user,doc;
        public static int userdocid =0;
        public  static int usergroupid = 0;
        public static int usergroupidtofindgroupmember = 0;
        public static int userdocidforsharelist = 0;
        public static int docgroupid=0;

      LinqToSqlDataContext context = new LinqToSqlDataContext();
      LinqToSql2DataContext context2 = new LinqToSql2DataContext();
        //Enter into UserTable
     
        public string userTableSelect(string tempdata)
        {
            try
            {
                user = tempdata;
                
                utemp = context.UserTables.SingleOrDefault(x => x.user_id == tempdata);         
                return utemp.user_id;
            }
            catch(Exception)
            {
                return "User Not Found";
            }
        }
        public string getUserId()
        {
            return utemp.user_id;
        }
        public string getPassword()
        {
            return utemp.password;
        }
        public string getUsername()
        {
            return utemp.user_name;
        }
        public int getNoofdocument(string user)
        {
            try
            {
                userTableSelect(user);
                return utemp.no_of_document;
            }
            catch(Exception)
            { return 0; }
        }
        public string getDepartment()
        {
            return utemp.department;
        }
        public string getEmail()
        {
            return utemp.email;
        }
        public string getUsertype()
        {
            return utemp.user_type;
        }
        public string getUsertype(string tempdata)
        {
            user = tempdata;

            utemp = context.UserTables.SingleOrDefault(x => x.user_id == tempdata);         
            return utemp.user_type;
        }
        public string showImage()
        {
            return utemp.user_image_path;
        }
        public int userCount()
        {
            int count;
            //LinqToSql2DataContext context = new LinqToSql2DataContext();
            count=context.UserTables.Count();
            return count;
        }

        //Enter into userAndDocumentTable

        public string userAndDocumentTableSelect(string tempuser)
        {
            try
           {  
                //LinqToSqlDataContext context = new LinqToSqlDataContext();
                udtemp = context2.UserAndDocumentTables.FirstOrDefault(x => x.user_id == tempuser && x.user_doc_id > userdocid);
                userdocid = udtemp.user_doc_id;
                return udtemp.doc_id;
            }
            catch (Exception)
            {
               string message = "Not Found";
               return message;
            }
        }


        public string userAndDocumentTableSelectAndReturnDocName(string tempuser)
        {
            try
            {
                udtemp = context2.UserAndDocumentTables.FirstOrDefault(x => x.user_id == tempuser && x.user_doc_id > userdocidforsharelist);
                userdocidforsharelist = udtemp.user_doc_id;
                dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == udtemp.doc_id);
                return dtemp.doc_name;
            }

            catch (Exception)
            {
                string message = "Not Found";
                return message;
            }
         }
       public string userAndDocumentTableSelectByListBoxIndex(string tempuser,int index)
           {
               for (int i = 0; i <= index; i++)
               {
                   udtemp = context2.UserAndDocumentTables.FirstOrDefault(x => x.user_id == tempuser && x.user_doc_id > userdocidforsharelist);
                   userdocidforsharelist = udtemp.user_doc_id;
                   dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == udtemp.doc_id);
               }
                return dtemp.doc_id;
            }

        public string docRight(string userId,string docId)
        {
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            udtemp = context2.UserAndDocumentTables.SingleOrDefault(x => x.user_id == userId && x.doc_id == docId);
            return udtemp.doc_right;
        }
        public int userAndDocumentCount()
        {
            int count;
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            count = context2.UserAndDocumentTables.Count();
            return count;
        }

        //Enter into DocumentTable
        
        public string documentTableSelect(string docId)
        {         
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.doc_id;
        }
        public string documentTableSelectByDocName(string docName,string user)
        {
            utemp = context.UserTables.SingleOrDefault(x => x.user_id == user);
            int noofdoc = utemp.no_of_document;
            userdocid = 0;
            for (int i = 0; i < noofdoc; i++)
            {
                udtemp = context2.UserAndDocumentTables.First(x => x.user_id == user && x.user_doc_id>userdocid);
                userdocid = udtemp.user_doc_id;
                string docId = udtemp.doc_id;
                dtemp=context.DocumentTables.SingleOrDefault(x=>x.doc_id==docId);
                string name=dtemp.doc_name;
                if (name.Equals(docName))
                {
                    break;
                }
                else
                    continue;

            }
            return dtemp.doc_id;
        }

        public string documentTableSelectByDocNameForGroupDoc(string docName, string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            int noofdoc = gtemp.total_document;
            docgroupid = 0;
          
            for (int i = 0; i < noofdoc; i++)
            {
                dgtemp = context2.DocumentAndGroupTables.First(x => x.group_id == gId && x.doc_group_id > docgroupid);
                docgroupid = dgtemp.doc_group_id;
                string docId = dgtemp.doc_id;
                dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
                string name = dtemp.doc_name;
                if (name.Equals(docName))
                {
                    break;
                }
                else
                    continue;

            }
            return dtemp.doc_id;
        }
        public void setDocument(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
           
        }

      /*  public string documentTableSelectForShareDoc(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.doc_id;
        }*/


        public string getDocType(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.doc_type;
        }
        public string getFilingPath(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.filing_path;
        }
        public string getFilingPath()
        {
            return dtemp.filing_path;
        }
        public string getFilingPathByDocId(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.filing_path;
        }
        public string getDocName()
        {
            return dtemp.doc_name;
        }
        public string getDocName(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.doc_name;
        }
        public int getTotalShareInGroup(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.total_share_in_group;
        }
        public int getTotalShareIndividual(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.total_share_individual;
        }
        public string getDocCheckInTime(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.checkin_time;
        }
        public string getDocCheckOutTime(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.checkout_time;
        }
        public string getDocSize(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return dtemp.size;
        }
        public int DocumentCount()
        {
            int count;
           // LinqToSqlDataContext context = new LinqToSqlDataContext();
            count = context.DocumentTables.Count();
            return count;
        }

        public DocumentTable getDocument(string docId)
        {
            DocumentTable ob = new DocumentTable();
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            ob = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            return ob;
        
        
        }
   



        //Insert into DocumentTable

        public void documentTableInsert(string docName, string docId, string docType, string scanId, string filingPath, string fileId, string checkInTime,string docSize)
        {
            DocumentTable ob = new DocumentTable();
            ob.doc_name = docName;
            ob.doc_id = docId;
            ob.doc_type = docType;
            ob.file_id = fileId;
            ob.filing_path = filingPath;
            ob.scan_id = scanId;
            ob.checkin_time = checkInTime;
            ob.size = docSize;
            ob.total_share_in_group = 0;
            ob.total_share_individual = 1;
         
            context.DocumentTables.InsertOnSubmit(ob);
            context.SubmitChanges();
         
        }

        public void checkOutTimeInsert(string checkOutTime)
        {
            dtemp.checkout_time = checkOutTime;
           // LinqToSqlDataContext context = new LinqToSqlDataContext();
            context.DocumentTables.InsertOnSubmit(dtemp);
            context.SubmitChanges();
        }

        //Insert into UserAndDocumentTable

        public void userAndDocumentTableInsert(string userId,string docId,string docRight)
        {
            UserAndDocumentTable ob = new UserAndDocumentTable();
            ob.doc_id = docId;
            ob.user_id = userId;
            ob.doc_right = docRight;
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            context2.UserAndDocumentTables.InsertOnSubmit(ob);
            context2.SubmitChanges();
        }

        //user table update
        public void udateNoOfDocument(string userId)
        {
            try
            {
              //  LinqToSql2DataContext context = new LinqToSql2DataContext();
                utemp = context.UserTables.SingleOrDefault(x => x.user_id == userId);
                utemp.no_of_document ++;
                context.SubmitChanges();
            }
            catch (Exception)
            {}
        
        }
        public void udateNoOfDocumentAfterDeletingAfile(string userId)
        {

                utemp = context.UserTables.SingleOrDefault(x => x.user_id == userId);
                utemp.no_of_document--;
                context.SubmitChanges();


        }
        public void udateNoOfGroupIncrease(string tempuser)
        {            
               // LinqToSql2DataContext context = new LinqToSql2DataContext();
                utemp = context.UserTables.SingleOrDefault(x => x.user_id == tempuser);
                utemp.no_of_group = utemp.no_of_group+1;
                context.SubmitChanges();
                       
        }
      

        public void udateNoOfGroupDecrease(string tempuser)
        {                          
                utemp = context.UserTables.SingleOrDefault(x => x.user_id == tempuser);
                utemp.no_of_group = utemp.no_of_group-1;
                context.SubmitChanges();
                       
        }
   // Update DocumentTable

        public void selectDocumentTableForUpdate(string docId)
        {
            doc = dtemp.doc_id;
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            
            // return temp.doc_id;
        }
        public void documentTableUpdate(string docName,string docType,string filingPath)
        {
            dtemp.doc_name = docName;
            dtemp.doc_type = docType;
            dtemp.filing_path = filingPath;
            
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == doc);
            context.SubmitChanges();
            
        }
        public void totalShareIndividualUpdate(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id==docId);
            dtemp.total_share_individual = dtemp.total_share_individual + 1;
            context.SubmitChanges();
        }

        public void totalShareInGroupUpdate(string docId)
        {
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            dtemp.total_share_in_group = dtemp.total_share_in_group + 1;
            context.SubmitChanges();
        }
        public void checkOutTimeUpdate(string checkOutTime)
        {
            dtemp.checkout_time = checkOutTime;
            //LinqToSql2DataContext context = new LinqToSql2DataContext();
            utemp = context.UserTables.SingleOrDefault(x => x.user_id == doc);
            context.SubmitChanges();
        }

  //Deleting from UserAndDocumentTable

        public void deleteFromUserAndDocumentTable(string userId, string docId)
        {
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            udtemp = context2.UserAndDocumentTables.FirstOrDefault(x => x.user_id == userId && x.doc_id==docId);
            context2.UserAndDocumentTables.DeleteOnSubmit(udtemp);
            context2.SubmitChanges();
        }
        public string deleteFromUserAndDocumentTableByDocId(string docId)
        {
            udtemp = context2.UserAndDocumentTables.First(x => x.doc_id == docId);
            string userId = udtemp.user_id;
            context2.UserAndDocumentTables.DeleteOnSubmit(udtemp);
            context2.SubmitChanges();
            return userId;
        }

   //Deleting from DocumentTable

        public void deleteFromDocumentTable(string docId)
        {

            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id == docId);
            context.DocumentTables.DeleteOnSubmit(dtemp);
            context.SubmitChanges();
        }

        //Deleting from DocumentAndGroupTable

        public void deleteFromDocumentAndGroupTableAndNoOfDocDecreaseInGroupTable(string docId,string gId)
        {
                dgtemp = context2.DocumentAndGroupTables.SingleOrDefault(x => x.doc_id == docId && x.group_id==gId);
                context2.DocumentAndGroupTables.DeleteOnSubmit(dgtemp);
                context2.SubmitChanges();
                gtemp = context.GroupTables.Single(x => x.group_id == gId);
                gtemp.total_document--;
                context.SubmitChanges();
        }
        public void deleteFromDocumentAndGroupTableByDocId(string docId)
        {       
                dgtemp = context2.DocumentAndGroupTables.First(x => x.doc_id == docId);
                string gId = dgtemp.group_id;
                context2.DocumentAndGroupTables.DeleteOnSubmit(dgtemp);
                context2.SubmitChanges();
                gtemp = context.GroupTables.Single(x => x.group_id == gId);
                gtemp.total_document=gtemp.total_document-1;
                context.SubmitChanges();
                
        }

        //Insert into GroupTable

        public void groupTableInsert(string groupId, string groupName, string adminId)
        {
            GroupTable ob = new GroupTable();
            ob.group_id = groupId;
            ob.group_name = groupName;
            ob.admin_id = adminId;
            ob.total_member = 1;
            ob.total_document = 0;
            //LinqToSql2DataContext context = new LinqToSql2DataContext();
            context.GroupTables.InsertOnSubmit(ob);
            context.SubmitChanges();
        }
        public int groupTableCount()
        {
            int count;
           // LinqToSql2DataContext context = new LinqToSql2DataContext();
            count = context.GroupTables.Count();
            return count;
        }
        public void GroupTableSelect(string gId)
        {
            //LinqToSql2DataContext context = new LinqToSql2DataContext();
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            
        }
        public string GroupTableSelectByGroupIdAndReturnAdminId(string gId)
        {
           
            gtemp = context.GroupTables.FirstOrDefault(x => x.group_id == gId);
            return gtemp.admin_id;
           
        }
        public string UserAndGroupTableSelectAndReturnGroupNames(string tempuser)
        {
           
            ugtemp = context2.UserAndGroupTables.FirstOrDefault(x => x.user_id == tempuser && x.user_group_id > usergroupid);
            usergroupid = ugtemp.user_group_id;
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == ugtemp.group_id);
            return gtemp.group_name;
        }

        public string GroupTableSelectForGroupId(string gName,string user)
        {
            utemp = context.UserTables.SingleOrDefault(x=>x.user_id==user);
            int a = utemp.no_of_group;
            usergroupid = 0;
            for (int i = 0; i < a; i++)
            {
                ugtemp = context2.UserAndGroupTables.First(x => x.user_id == user &&  x.user_group_id > usergroupid);
                usergroupid = ugtemp.user_group_id;
                string groupId = ugtemp.group_id;
                gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == groupId);
                string groupName = gtemp.group_name;
                if (groupName.Equals(gName))
                {
                    break;
                }
                else
                    continue;
            }
           
            return gtemp.group_id;
        }

        public string getGroupName(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            return gtemp.group_name;
        }
        public string getAdminId(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            return gtemp.admin_id;
        }
        public int getTotalMember(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            return gtemp.total_member;
        }
        public int getTotalDocument(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            return gtemp.total_document;
        }
        /*public int getTotalMember(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            return gtemp.total_member;
        }*/

        public void updateTotalDocument(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            gtemp.total_document++;
            context.SubmitChanges();
        }
        public void updateAdminId(string adminId,string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            gtemp.admin_id = adminId;
            context.SubmitChanges();
          
        }
        public void updateTotalMemberIncrease(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            gtemp.total_member++;
            context.SubmitChanges();
        }
        public void updateTotalMemberDecrease(string gId)
        {
            gtemp = context.GroupTables.SingleOrDefault(x => x.group_id == gId);
            gtemp.total_member--;
            context.SubmitChanges();
        }
        public int userAndGroupTableCount()
        {
            int count;
           // LinqToSqlDataContext context = new LinqToSqlDataContext();
            count = context2.UserAndGroupTables.Count();
            return count;
        }
        public int documentAndGroupTableCount()
        {
            int count;
         //   LinqToSqlDataContext context = new LinqToSqlDataContext();
            count = context2.DocumentAndGroupTables.Count();
            return count;
        }
        public void userAndGroupTableInsert(string gAdmin,string gId)
        {
            UserAndGroupTable obj = new UserAndGroupTable();
            obj.group_id = gId;
            obj.user_id = gAdmin;
            context2.UserAndGroupTables.InsertOnSubmit(obj);
            context2.SubmitChanges();
        

            
        }
        public string userAndGroupTableSelect(string tempuser)
        {
            try
            {
               // LinqToSqlDataContext context = new LinqToSqlDataContext();
                ugtemp = context2.UserAndGroupTables.FirstOrDefault(x => x.user_id == tempuser && x.user_group_id > usergroupid);
                usergroupid = ugtemp.user_group_id;
                return ugtemp.group_id;
            }
            catch(Exception)
            { return "You Are Not Connected To Any Group"; }
           
        }
        public void deleteFromuserAndGroupTable(string memberId,string  groupId)
        {
            ugtemp = context2.UserAndGroupTables.SingleOrDefault(x => x.group_id == groupId && x.user_id == memberId);
            context2.UserAndGroupTables.DeleteOnSubmit(ugtemp);
            context2.SubmitChanges();
        }

        public void deleteFromuserAndGroupTable(string groupId)
        {
            ugtemp = context2.UserAndGroupTables.FirstOrDefault(x => x.group_id == groupId);
            context2.UserAndGroupTables.DeleteOnSubmit(ugtemp);
            context2.SubmitChanges();
        }
        public void deleteFromdocumentAndGroupTable(string groupId)
        {
            dgtemp = context2.DocumentAndGroupTables.FirstOrDefault(x => x.group_id == groupId);
            context2.DocumentAndGroupTables.DeleteOnSubmit(dgtemp);
            context2.SubmitChanges();
        }

        public Boolean userAndGroupTableCheckByGidAndUId(string adminId,string gId)
        {
            Boolean a = false;
            try
            {
                ugtemp = context2.UserAndGroupTables.Single(x => x.user_id == adminId && x.group_id == gId);
                a = true;
                return a;
            }
            catch (Exception)
            { return a; }

        }

        public string documentAndGroupTableSelect(string gId)
        {
            try
            {
                // LinqToSqlDataContext context = new LinqToSqlDataContext();
                dgtemp = context2.DocumentAndGroupTables.First(x => x.group_id == gId && x.doc_group_id> docgroupid);
                docgroupid = dgtemp.doc_group_id;
                return dgtemp.doc_id;
            }
            catch (Exception)
            { return "You Are Not Connected To Any Group"; }

        }

           public string documentAndGroupTableSelectAndReturnDocName(string gId)
           {
                dgtemp = context2.DocumentAndGroupTables.First(x => x.group_id == gId && x.doc_group_id> docgroupid);
                docgroupid = dgtemp.doc_group_id;
                string docId = dgtemp.doc_id;
                dtemp = context.DocumentTables.SingleOrDefault(x => x.doc_id==docId);
                return dtemp.doc_name;
            }

        public string userAndGroupTableSelectByGroupId(string gId)
        {
            ugtemp = context2.UserAndGroupTables.FirstOrDefault(x => x.group_id == gId && x.user_group_id > usergroupidtofindgroupmember);
            usergroupidtofindgroupmember = ugtemp.user_group_id;
            return ugtemp.user_id;
        }

        public int getNoOfGroup(string userId)
        {
            try
            {
           // LinqToSql2DataContext context = new LinqToSql2DataContext();
            utemp = context.UserTables.SingleOrDefault (x => x.user_id == userId);
            return utemp.no_of_group;

          
              
               
            }
            catch (Exception)
            { return 0; }
        }
        public void documentAndGroupTableInsert(string dId, string gId,string docRight)
        {
            DocumentAndGroupTable ob = new DocumentAndGroupTable();
            ob.group_id = gId;
            ob.doc_id = dId;
            ob.doc_right = docRight;
            //LinqToSqlDataContext context = new LinqToSqlDataContext();
            context2.DocumentAndGroupTables.InsertOnSubmit(ob);
            context2.SubmitChanges();
        }

        //////Selecting first row of a table

        public string firstRowGroupTable()
        {
            gtemp = context.GroupTables.First();
            return gtemp.group_id;

        }
        public string firstRowDocumentTable()
        {
            dtemp = context.DocumentTables.First();
            return dtemp.doc_id;

        }

        public int firstRowUserAndGroupTable()
        {
            ugtemp = context2.UserAndGroupTables.First();
            return ugtemp.user_group_id;

        }
        public int firstRowUserAndDocumentTable()
        {
            udtemp = context2.UserAndDocumentTables.First();
            return udtemp.user_doc_id;

        }
        public int firstRowDocumentAndGroupTable()
        {
            dgtemp = context2.DocumentAndGroupTables.First();
            return dgtemp.doc_group_id;

        }
        //////Selecting last row of a table
        public string lastRowGroupTable()
        {  
            gtemp = context.GroupTables.AsEnumerable().Last();
            return gtemp.group_id;

        }

        public string lastRowDocumentTable()
        {
            dtemp = context.DocumentTables.AsEnumerable().Last();
            return dtemp.doc_id;

        }

        public int LastRowUserAndGroupTable()
        {
            ugtemp = context2.UserAndGroupTables.AsEnumerable().Last();
            return ugtemp.user_group_id;

        }
        public int LastRowUserAndDocumentTable()
        {
            udtemp = context2.UserAndDocumentTables.AsEnumerable().Last();
            return udtemp.user_doc_id;

        }
        public int LastRowDocumentAndGroupTable()
        {
            dgtemp = context2.DocumentAndGroupTables.AsEnumerable().Last();
            return dgtemp.doc_group_id;

        }


    }
}
