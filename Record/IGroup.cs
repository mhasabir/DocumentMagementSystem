using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Record
{
    interface IGroup
    {
           string groupIdGeneration();
           void createGroup(string gName, string tempuser);
           int getNoOfGroup(string userId);
           string getGroupId(string userId);
    }
}
