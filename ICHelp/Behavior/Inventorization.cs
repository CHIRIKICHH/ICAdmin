using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Behavior
{
    internal class Inventorization : AbsAssignment
    {
        public override string Execute(AssignmentType command)
        {
            if (command == AssignmentType.GetCurrentInventarizationData)
                return "Инвенторизация!";
            else
                return base.Execute(command);
        }
    }
}
