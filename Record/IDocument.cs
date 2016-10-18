using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Record
{
    interface IDocument
    {

          void setUser(string userName);
          void saveDocument(string docName, string docPath, string docType, double size);
          string convertSize(double size1);
          void shareDocument(string individualsId, string docId);
          void shareDocumentViaGroup(string groupId, string docId, string docRight);
          string[] getAllDocIds();
          string docIdGeneration();
          string scanIdGeneration();
          string filingLocationIdGeneration();
          string checkInTime();
          void checkOutTime();
          void setDocument(string docId);
          void saveDocumentInHardDrive(string sourcePath, string destinationPath);
          void deleteDocumentFromHardDrive(string destinationPath);
          int getNoOfDoc(string user);
       
    }
}
