using System.Diagnostics;

namespace com.bjss.generator.Model
{
    [DebuggerDisplay("{Severity} = {Message}")]
    public class Error
    {
        internal Error(int line, int column, string message, Severity severity = Severity.Error)
            :this(new Location(line, column), message, severity)
        { }

        internal Error(Location location, string message, Severity severity = Severity.Error)
        {
            Location = location;
            Message = message;
            Severity = severity;
        }

        public Location Location { get; private set; }
        public string Message { get; private set; }
        public Severity Severity { get; private set; }
    }
}