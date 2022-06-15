using ICHelp.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Behavior
{
    internal class AnyDeskId : AbsAssignment
    {
        public override string Execute(AssignmentType command)
        {
            if (command == AssignmentType.GetCurrentAnyDeskId)
                return "";
            else
                return base.Execute(command);
        }

    }
}
