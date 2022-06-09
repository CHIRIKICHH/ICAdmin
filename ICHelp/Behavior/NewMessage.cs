using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Behavior
{
    internal class NewMessage : AbsAssignment
    {
        public override string Execute(AssignmentType command)
        {
            if (command == AssignmentType.NewMessage)
                return "У вас новое сообщение!";
            else
                return base.Execute(command);
        }
    }
}
