using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Behavior
{
    internal class Assignment
    {
        public int Id { get; set; }
        public AssignmentType AssignmentType { get; set; }
        public DateTime DateGenerateAssignment { get; set; }
    }
}
