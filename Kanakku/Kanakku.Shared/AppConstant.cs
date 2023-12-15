namespace Kanakku.Shared;

public static class SecureStorageKey
{
    public const string IS_LOGGED = "is_logged";
    public const string USER_ID = "user_id";
    public const string USERNAME = "username";
    public const string USER_DAILY_OPERATION_FILTER = "daily-operations-{0}-key";
}

public static class LookupMasterInternalName
{
    public const string STATE = "state";
    public const string DISTRICT = "district";
}

public static class DirectoryConstant
{
    public const string BINARY_RESOURCE_FORMAT = "{0}/Kanakku/Binary Resources";
    public const string EXPORT_DIRECTORY_FORMAT = "{0}/Kanakku/Binary Resources/Exports";
    public const string DB_BACKUP_FORMAT = "{0}/Kanakku/Backup/";
    public const string DFAULT_PRODUCT_IMAGE_PLACEHOLDER = "./assets/images/default-product-placeholder.png";
    public const string DFAULT_IMAGE_PLACEHOLDER = "./assets/images/default-product-placeholder.png";
    public static string EXPORT_DIRECTORY_PATH => String.Format(DirectoryConstant.EXPORT_DIRECTORY_FORMAT, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
    public static string DB_BACKUP_PATH => String.Format(DirectoryConstant.DB_BACKUP_FORMAT, Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));
}

public static class RouteConstant
{
    public static string EXPORT_PATH => "{0}static/exports/{1}";
}

public static class Constant
{
    public const long RESOURCE_MAX_LENGTH = 2_000_000;
    public const long FILE_PICKER_MAX_LENGTH = 100_000_000;
}

public static class AppRegex
{
    public const string NAME = "^[a-zA-Z ]+$";
    public const string PHONE_NUMBER = "^[+]?[0-9]*$";
    public const string PINCODE = "^[0-9]{6,}$";
    public const string EMAIL = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
}

public static class UIComp
{
    public const string TABLE_HEIGHT_DEFAULT = "60vh";
    public const string TABLE_HEIGHT_OPERATIONS = "60vh";
}

public static class AppSetting
{
    public const string DATE_FORMAT = "dd-MMM-yyyy";
    public const string DATE_TIME_FORMAT = "dd-MMM-yyyy HH:mm";
    public const string MONTH_YEAR_FORMAT = "MMM-yyyy";
}