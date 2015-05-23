using System;
using System.Text;

namespace Kana.Ikimi.SocketFrame.Micro.Model {

    public class SocketCommand {

        private String _command;
        private String[] _parameters;
        private String _original;

        public String Command { get { return this._command; } }
        public String[] Parameters { get { return this._parameters.Clone() as String[]; } }
        public String Original { get { return this._original; } }

        public static SocketCommand Create(String commandString) {
            String[] commandSplit = commandString.Split(' ');
            String command = commandSplit[0].ToUpper();
            String[] parameters = new String[commandSplit.Length-1];
            Array.Copy(commandSplit, 1, parameters, 0, parameters.Length);
            return new SocketCommand() {
                _command = command,
                _parameters = parameters,
                _original = commandString
            };
        }

        public override String ToString() {
            var output = new StringBuilder();
            output.Append(this._command);
            foreach (String param in this._parameters) {
                output.Append(" ");
                output.Append(param);
            }
            return output.ToString();
        }

    }

}
