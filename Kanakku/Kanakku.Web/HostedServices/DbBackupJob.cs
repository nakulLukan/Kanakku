using Coravel.Invocable;
using Kanakku.Shared;
using Npgsql;
using System.Diagnostics;

namespace Kanakku.Web.HostedServices
{
    public class DbBackupJob : IInvocable
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<DbBackupJob> _logger;
        
        public DbBackupJob(IConfiguration configuration, ILogger<DbBackupJob> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task Invoke()
        {
            try
            {
                //TakePgDump();
                CopyBackupFileToServer(DirectoryConstant.DB_BACKUP_PATH + "/backup.sql");
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "Backup Job Task Failed");
            }
        }

        private void CopyBackupFileToServer(string backupFilePath)
        {
            string sharedFolderName = @"\\nakul\d";
            string relativeBackDirectoryPath = "kanakku/db-backups/";
            var serverSharedDirectory = new DirectoryInfo(sharedFolderName);
            if (!serverSharedDirectory.Exists)
            {
                _logger.LogError("The application does not have access to shared folder: {0}", sharedFolderName);
                return;    
            }

            try
            {
                var acl = serverSharedDirectory.GetAccessControl();
                
            }
            catch (UnauthorizedAccessException uae)
            {
                _logger.LogError(uae, "Does not have permission to use shared folder {0}", sharedFolderName);
            }
        }

        private string TakePgDump()
        {
            string connectionString = _configuration.GetConnectionString("DbConnection");

            string backupPath = DirectoryConstant.DB_BACKUP_PATH + "/backup.sql";
            if (!Directory.Exists(DirectoryConstant.DB_BACKUP_PATH))
            {
                Directory.CreateDirectory(DirectoryConstant.DB_BACKUP_PATH);
            }
            // Database name to backup
            string databaseName = "kanakku";

            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();

                // Use the pg_dump command to perform the backup
                using Process pgDumpProcess = new Process();
                pgDumpProcess.StartInfo.FileName = "pg_dump";
                pgDumpProcess.StartInfo.Arguments = $"-h {connection.Host} -U {connection.UserName} -d {databaseName} -f \"{backupPath}\" --no-password";
                pgDumpProcess.StartInfo.UseShellExecute = false;
                pgDumpProcess.StartInfo.RedirectStandardOutput = true;
                pgDumpProcess.StartInfo.RedirectStandardError = true;

                pgDumpProcess.Start();

                string output = pgDumpProcess.StandardOutput.ReadToEnd();
                string error = pgDumpProcess.StandardError.ReadToEnd();

                pgDumpProcess.WaitForExit();

                if (pgDumpProcess.ExitCode == 0)
                {
                    _logger.LogInformation("Backup completed successfully.");
                }
                else
                {
                    throw new InvalidOperationException(error);
                }

                return backupPath;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to backup database");
                Console.WriteLine($"Error: {ex.Message}");
                return string.Empty;
            }
        }
    }
}
