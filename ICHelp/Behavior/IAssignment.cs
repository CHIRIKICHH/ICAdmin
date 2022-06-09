using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Behavior
{
    internal interface IAssignment
    {
        IAssignment SetNextAssignment(IAssignment assignment);
        string Execute(AssignmentType command);
    }
}
