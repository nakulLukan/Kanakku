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
        private const string BACKUP_FILE_STAMP_FORMAT = "yyyyMMdd";

        readonly string sharedFolderName;
        readonly string relativeBackDirectoryPath;
        string destinationPath => sharedFolderName + relativeBackDirectoryPath;
        string destinationFileName { get => destinationPath + DateTime.Now.ToString(BACKUP_FILE_STAMP_FORMAT) + "_backup.sql"; }

        public DbBackupJob(IConfiguration configuration, ILogger<DbBackupJob> logger)
        {
            _configuration = configuration;
            _logger = logger;
            sharedFolderName = configuration["ServerDetails:SharedFolderName"];
            relativeBackDirectoryPath = configuration["ServerDetails:DbBackupRelativePath"];
        }

        public async Task Invoke()
        {
            try
            {
                if (File.Exists(destinationFileName))
                {
                    _logger.LogInformation("Backup file already exists for the day. Skipping backup process.");
                    return;
                }
                var backupFilePath = TakePgDump();
                CopyBackupFileToServer(backupFilePath);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Backup Job Task Failed");
            }

            await Task.CompletedTask;
        }

        private void CopyBackupFileToServer(string backupFilePath)
        {
            var serverSharedDirectory = new DirectoryInfo(sharedFolderName);
            if (!serverSharedDirectory.Exists)
            {
                _logger.LogError("The application does not have access to shared folder: {0}", sharedFolderName);
                return;
            }

            try
            {
                var acl = serverSharedDirectory.GetAccessControl();
                if (!Directory.Exists(destinationPath))
                {
                    Directory.CreateDirectory(destinationPath);
                }
                var destinationFileName = destinationPath + DateTime.Now.ToString(BACKUP_FILE_STAMP_FORMAT) + "_backup.sql";
                File.Copy(backupFilePath, destinationFileName, true);
                _logger.LogInformation("Backup file moved to shared folder {0}", destinationFileName);
                
                // Delete backup files
                DeleteOldBackupFiles();
            }
            catch (UnauthorizedAccessException uae)
            {
                _logger.LogError(uae, "Does not have permission to use shared folder {0}", sharedFolderName);
            }
        }

        private void DeleteOldBackupFiles()
        {
            // Delete backup files before 1 month
            var backupFiles = Directory.GetFiles(destinationPath);
            foreach (var backupFile in backupFiles)
            {
                var dateStamp = Path.GetFileName(backupFile).Split("_")[0];
                if (DateTime.ParseExact(dateStamp, BACKUP_FILE_STAMP_FORMAT, null) < DateTime.Now.AddDays(-30))
                {
                    File.Delete(backupFile);
                    logger.LogInformation("Backup file {0} deleted", Path.GetFileName(backupFile));
                }
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

            try
            {
                using NpgsqlConnection connection = new NpgsqlConnection(connectionString);
                connection.Open();

                // Use the pg_dump command to perform the backup
                using Process pgDumpProcess = new Process();
                pgDumpProcess.StartInfo.FileName = "pg_dump";
                pgDumpProcess.StartInfo.Arguments = $"-h {connection.Host} -U {connection.UserName} -d {connection.Database} -f \"{backupPath}\" --no-password";
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
