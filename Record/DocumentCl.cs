using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Record
{
    class DocumentCl:IDocument
    {
        DatabaseCl temp = new DatabaseCl();
        string user;
        public void setUser(string userName)
        {
            user = userName;
            
        }
        public void saveDocument(string docName,string docPath,string docType,double size)
        {
            temp.udateNoOfDocument(user);
            string cInTime, docId, scanId, filingId,docRight,docSize;
            docSize=convertSize(size);
            docRight = "owner";
            cInTime = checkInTime();
            docId = docIdGeneration();
            scanId = scanIdGeneration();
            filingId = filingLocationIdGeneration();
            temp.documentTableInsert(docName, docId, docType, scanId, docPath, filingId, cInTime, docSize);
            temp.userAndDocumentTableInsert(user, docId,docRight);
            
        }
        public string convertSize(double size1)
        {  
            double size;
            string sizeinstring;
            if(size1>1048576)
            {
                 size = size1 / 1048576;
                 size =Math.Round(size,2);
                 sizeinstring=size.ToString();
                 sizeinstring=sizeinstring+"Mb";
                 return sizeinstring;
            }
            else if (size1 > 1024)
            {
                size = size1 / 1024;
                size = Math.Round(size, 1);
                sizeinstring = size.ToString();
                sizeinstring = sizeinstring + "Kb";
                return sizeinstring;
            }
            else
            {
                size = size1;
                size = Math.Round(size, 1);
                sizeinstring = size.ToString();
                sizeinstring = sizeinstring + "Bytes";
                return sizeinstring;
            
            }
        }
        public void shareDocument(string individualsId, string docId)
        {           
            string docRight = "Shared";
            temp.userAndDocumentTableInsert(individualsId,docId,docRight);
            temp.udateNoOfDocument(individualsId);
            temp.totalShareIndividualUpdate(docId);
        }
        public void shareDocumentViaGroup(string groupId, string docId,string docRight)
        {
            //temp.GroupTableSelect(groupId);
            temp.updateTotalDocument(groupId); 
            temp.documentAndGroupTableInsert(docId, groupId, docRight);
            temp.totalShareInGroupUpdate(docId);     
       
        }
        public string[] getAllDocIds()
        {
            int a = getNoOfDoc(user);
            string[] docIds=new string[a] ;
            for (int i = 0; i < a; i++)
            {
                docIds[i] = temp.userAndDocumentTableSelect(user);

            }
            return docIds;
        }
    
        public string docIdGeneration()
        {
            int did,rowCount;
            string scanId, docId;
            rowCount = temp.DocumentCount();
            scanId = scanIdGeneration();
            if (rowCount==0)
            {
                did = 10000;            
                docId = "d" + did + scanId;
            }
            else
            {
                string lastdocId = temp.lastRowDocumentTable();
                string[] m = lastdocId.Split('s');
                docId = m[0].Replace("d", "");
                did = Convert.ToInt32(docId);
                did++;
                docId = "d" + did + scanId;
            }
            return docId;
            
        }
        public string scanIdGeneration()
        {
            int sid,rowCount;
            string scanId;
            rowCount = temp.DocumentCount();

            if (rowCount == 0)
            {
                sid = 20000;             
                scanId = "s" + sid;
            }
            else
            {
                string lastdocId = temp.lastRowDocumentTable();
                string[] m = lastdocId.Split('s');
                sid = Convert.ToInt32(m[1]);
                sid++;
                scanId = "s" + sid;
            }
            return scanId;
        }
        public string filingLocationIdGeneration()
        {
            string filingId ="f"+user;
            return filingId;
        }
        public string checkInTime()
        {    string cInTime;
            DateTime dateTime = DateTime.Now;
            cInTime = dateTime.ToString();
            return cInTime;
        }
        public void checkOutTime()
        {
            string cOutTime;
            DateTime dateTime = DateTime.Now;
            cOutTime = dateTime.ToString();
            temp.checkOutTimeInsert(cOutTime);
        }
        public void setDocument(string docId)
        {
            temp.setDocument(docId);
        }

        public void saveDocumentInHardDrive(string sourcePath,string destinationPath)
        {
            File.Copy(@sourcePath,@destinationPath,true);
        }
        public void deleteDocumentFromHardDrive(string destinationPath)
        {
            if (File.Exists(@destinationPath))
            {

                try
                {
                    File.Delete(@destinationPath);
                   
                }
                catch(Exception)
                {   
                }

            }
        }

       
        public int getNoOfDoc(string user)
        {
          return  temp.getNoofdocument(user); 
        }
        
       
    }
}
