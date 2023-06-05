using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Exceptions;
public class OperationFailureException : Exception
{
    public OperationFailureException(string message) : base(message)
    { }
}
