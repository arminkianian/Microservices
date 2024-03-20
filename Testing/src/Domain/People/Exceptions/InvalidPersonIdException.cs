using System.Runtime.Serialization;

namespace Domain.People.Exceptions
{
    public class InvalidPersonIdException : Zamin.Core.Domain.Exceptions.InvalidEntityStateException
    {
        public InvalidPersonIdException(string message, params string[] parameters) : base(message, parameters)
        {
        }
    }
}