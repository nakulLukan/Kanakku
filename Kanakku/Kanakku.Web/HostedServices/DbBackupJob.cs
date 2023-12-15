using Coravel.Invocable;
using Kanakku.Shared;
using Npgsql;
using System.Diagnostics;

namespace Kanakku.Web.HostedServices
{
    public class DbBackupJob : IInvocable
    {
        public async Task Invoke()
        {
            TakePgDump();
        }

        private void TakePgDump()
        {
            // Replace with your connection string
            string connectionString = "Host=localhost;Username=nakul;Password=password;Database=kanakku;Port=5433";

            // Replace with the path where you want to save the backup file
            string backupPath = DirectoryConstant.DB_BACKUP_PATH + "/backup.sql";

            // Database name to backup
            string databaseName = "kanakku";

            try
            {
                using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    // Use the pg_dump command to perform the backup
                    using (Process pgDumpProcess = new Process())
                    {
                        pgDumpProcess.StartInfo.FileName = "pg_dump";
                        pgDumpProcess.StartInfo.Arguments = $"-h {connection.Host} -U {connection.UserName} -d {databaseName} -f \"{backupPath}\"";
                        pgDumpProcess.StartInfo.UseShellExecute = false;
                        pgDumpProcess.StartInfo.RedirectStandardOutput = true;
                        pgDumpProcess.StartInfo.RedirectStandardError = true;

                        pgDumpProcess.Start();

                        string output = pgDumpProcess.StandardOutput.ReadToEnd();
                        string error = pgDumpProcess.StandardError.ReadToEnd();

                        pgDumpProcess.WaitForExit();

                        if (pgDumpProcess.ExitCode == 0)
                        {
                            Console.WriteLine("Backup completed successfully.");
                        }
                        else
                        {
                            Console.WriteLine($"Error: {error}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }
}
