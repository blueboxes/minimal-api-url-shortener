class TableRow : ITableEntity {
    public string TargetUrl { get; set; }
    public string PartitionKey { get; set; }
    public string RowKey { get; set; }
    public DateTimeOffset? Timestamp { get; set; }
    public ETag ETag { get; set; }

    public TableRow()
    {
        TargetUrl = string.Empty;
        PartitionKey = string.Empty;
        RowKey = string.Empty;
    }
}
