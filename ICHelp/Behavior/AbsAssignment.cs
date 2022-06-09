using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Behavior
{
    internal abstract class AbsAssignment : IAssignment
    {
        private IAssignment nextAssignment;
        public AbsAssignment() => nextAssignment = null;
        public virtual string Execute(AssignmentType command)
        {
            if(nextAssignment != null)
                return nextAssignment.Execute(command);
            return "Такой команды нет!";
        }

        public IAssignment SetNextAssignment(IAssignment assignment)
        {
            nextAssignment = assignment;
            return assignment;
        }
    }
}
