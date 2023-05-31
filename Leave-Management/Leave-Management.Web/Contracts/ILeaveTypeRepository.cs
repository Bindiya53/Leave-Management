using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Leave_Management.Web.Data;

namespace Leave_Management.Web.Contracts
{
    public interface ILeaveTypeRepository: IGenericRepository<LeaveType>
    {
        
    }
}