using DbUp.Engine;
using DbUp.Engine.Transactions;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ideas.TestData
{
    /// <summary>
    /// Retrieves scripts from given directory with the directory path included in script name
    /// </summary>
    public class DirectoryScriptsProvider : IScriptProvider
    {
        private readonly string _directoryPath;

        ///<summary>
        ///</summary>
        ///<param name="directoryPath">Path to SQL upgrade scripts</param>
        public DirectoryScriptsProvider(string directoryPath)
        {
            _directoryPath = directoryPath;
        }


        /// <summary>
        /// Gets all scripts that should be executed.
        /// </summary>
        public IEnumerable<SqlScript> GetScripts(IConnectionManager connectionManager)
        {
            var files = Directory.GetFiles(_directoryPath, "*.sql").AsEnumerable();

            return files.Select(x => SqlScript.FromStream(x, new FileStream(x, FileMode.Open))).ToList();
        }
    }
}
