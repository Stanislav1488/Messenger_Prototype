using Messenger_Prototype.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Prototype.Model
{
    public class User : BaseViewModel, IProfileUser
    {
        private string status;
        public string Login { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Status
        {
            get { return status; }
            set
            {
                status = value;
                OnPropertyChanged(nameof(Status));
            }
        }
    }
}
