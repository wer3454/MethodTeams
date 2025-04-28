using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodologyMain.Application.Exceptions
{
    public class MemberAlreadyInTeamException : Exception
    {
        public MemberAlreadyInTeamException()
        {
            
        }

        public MemberAlreadyInTeamException(string message)
            : base(message)
        {
            
        }
    }
}
