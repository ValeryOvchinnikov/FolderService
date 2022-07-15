using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FolderService
{
    public class LogCategory
    {
        private LogCategory(string value) { Value = value; }

        public string Value { get; private set; }

        public static LogCategory Create { get { return new LogCategory("CREATED"); } }
        public static LogCategory Rename { get { return new LogCategory("RENAMED TO"); } }
        public static LogCategory Update { get { return new LogCategory("UPDATED"); } }
        public static LogCategory Delete { get { return new LogCategory("DELETED"); } }
    }
}
