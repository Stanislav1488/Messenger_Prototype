using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger_Prototype.Model
{
    public interface IProfileUser
    {
        string Name { get; set; }
        string Login { get; set; }
        string Status { get; set; }
    }
}

