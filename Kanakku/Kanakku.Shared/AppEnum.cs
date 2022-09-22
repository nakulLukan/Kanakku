namespace Kanakku.Shared;

public enum ErrorType : short
{
    Warning = 1,
    Error = 2
}

public enum DailyOperationLayout : short
{
    ListView,
    Tabular,
}

public enum SizeGroup : int
{
    General = 1,
}

public enum DateFilter: short
{
    ThisMonth = 1,
    ThisWeek,
    Today,
    LastMonth,
    LastWeek,
    Yesterday
}