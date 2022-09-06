namespace Kanakku.Shared;

public static class SecureStorageKey
{
    public const string IS_LOGGED = "is_logged";
    public const string USER_ID = "user_id";
}

public static class LookupMasterInternalName
{
    public const string STATE = "state";
    public const string DISTRICT = "district";
}

public static class DirectoryConstant
{
    public const string BINARY_RESOURCE_FORMAT = "{0}/Kanakku/Binary Resources";
    public const string DFAULT_PRODUCT_IMAGE_PLACEHOLDER = "./assets/images/default-product-placeholder.png";
    public const string DFAULT_IMAGE_PLACEHOLDER = "./assets/images/default-product-placeholder.png";
}

public static class Constant
{
    public const long RESOURCE_MAX_LENGTH = 2_000_000;
    public const long FILE_PICKER_MAX_LENGTH = 100_000_000;
}

public static class AppRegex
{
    public const string NAME = "^[a-zA-Z ]+$";
    public const string PHONE_NUMBER = "^[+]?[0-9]+$";
    public const string PINCODE = "^[0-9]{6,}$";
    public const string EMAIL = "^[a-zA-Z0-9+_.-]+@[a-zA-Z0-9.-]+$";
}

public static class UIComp
{
    public const string TABLE_HEIGHT_DEFAULT = "60vh";
}