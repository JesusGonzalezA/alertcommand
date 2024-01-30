namespace mteams.Configuration;

public class DocumentDBConfiguration
{
    public string DocumentDatabaseName { get; set; }
    public DBCollectionNameList CosmosCollectionNameList { get; set; }
}

public class DBCollectionNameList
{
    public string ConversationsCollectionName { get; set; }
}
