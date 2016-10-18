using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Record
{
    class GroupCl : IGroup
    {
        DatabaseCl temp = new DatabaseCl();

        public string groupIdGeneration()
        {
            string groupId;
            int gid, rowCount;
            rowCount = temp.groupTableCount();
            if (rowCount == 0)
            {
                gid = 1;
                groupId = "Group" + gid;
            }
            else
            {
                string lastGroupId = temp.lastRowGroupTable();
                groupId = lastGroupId.Replace("Group", "");
                gid = Convert.ToInt32(groupId);
                gid++;
                groupId = "Group" + gid;
            }
            return groupId;
        }

        public void createGroup(string gName,string tempuser)
        {   
            string gid=groupIdGeneration();
            temp.groupTableInsert(gid, gName, tempuser);
            temp.udateNoOfGroupIncrease(tempuser);
            temp.userAndGroupTableInsert(tempuser, gid);
           
        }

        public int getNoOfGroup(string userId)
        {  
            int b=temp.getNoOfGroup(userId);
            return b;

        }
        public string getGroupId(string userId)
        {
            return temp.userAndGroupTableSelect (userId);

        }

    }
}
