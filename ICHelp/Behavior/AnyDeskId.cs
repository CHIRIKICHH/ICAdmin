using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Behavior
{
    internal class AnyDeskId : AbsAssignment
    {
        public override string Execute(AssignmentType command)
        {
            if (command == AssignmentType.GetCurrentAnyDeskId)
                return "AnyDesk ID = 12345678";
            else
                return base.Execute(command);
        }
    }
}
