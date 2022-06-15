using DevExpress.Mvvm;
using ICHelp.Behavior;
using ICHelp.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ICHelp.Services
{
    public class AssignmentService : BindableBase
    {
        private static AssignmentService instance;
        private Queue<Assignment> Assignments { get; set; } = new Queue<Assignment>();
        private StartChain StartChain = new StartChain();

        private string commandName;
        public string CommandName
        {
            get { return commandName; }
            set
            {
                commandName = value;
                RaisePropertiesChanged("CommandName");
            }
        }
        public AssignmentService()
        {
            AnyDeskId anyDeskId = new AnyDeskId();
            NewMessage newMessage = new NewMessage();
            //Inventorization inventorization = new Inventorization();
            //StartChain.SetNextAssignment(anyDeskId).SetNextAssignment(newMessage).SetNextAssignment(inventorization);

            CheckAssignmentAsync();
        }

        private void DoAssignmentsAsync(IAssignment assignment)
        {
            while (Assignments.TryDequeue(out Assignment result))
            {
                if (result != null)
                    CommandName = assignment.Execute(result.AssignmentType);
            }
        }
        private async Task CheckAssignmentAsync()
        {
            while (true)
            {
                if (CheckConnectionService.GetInstance().IsServerConnected)
                {
                    var client = new HttpClient();
                    string resultJson = await client.GetStringAsync($"{Server.Domain}:{Server.Port}/api/Assignment/CheckAssignmentTest");
                    var result = JsonConvert.DeserializeObject<Assignment[]>(resultJson);
                    if (result != null)
                    {
                        foreach (var item in result)
                            Assignments.Enqueue(item);
                        DoAssignmentsAsync(StartChain);
                    }
                }
                await Task.Delay(5000);
            }
        }

        public static AssignmentService GetInstance()
        {
            if (instance == null)
            {
                instance = new AssignmentService();
            }
            return instance;
        }
    }
}
