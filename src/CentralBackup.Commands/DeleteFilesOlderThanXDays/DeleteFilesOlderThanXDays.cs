using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CentralBackup.Core.Interfaces.Commands;

namespace CentralBackup.Commands.DeleteFilesOlderThanXDays
{
    public class DeleteFilesOlderThanXDays : ICommand
    {
        public void Execute(IDictionary<string, string> configuration)
        {
            var directoryInfo = new DirectoryInfo(configuration["Path"]);
            var files = directoryInfo.GetFiles()
                .Where(p => p.CreationTime < DateTime.Now.AddDays(int.Parse(configuration["Days"]) * -1));

            foreach (var file in files)
            {
                file.Delete();
            }
        }
    }
}