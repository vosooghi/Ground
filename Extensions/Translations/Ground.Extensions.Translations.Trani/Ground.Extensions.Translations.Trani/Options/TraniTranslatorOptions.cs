namespace Ground.Extensions.Translations.Trani.Options
{
    public class TraniTranslatorOptions
    {
        public string ConnectionString { get; set; } = string.Empty;
        public bool AutoCreateSqlTable { get; set; } = true;
        public string TableName { get; set; } = "TraniTranslations";
        public string SchemaName { get; set; } = "dbo";
        public int ReloadDataIntervalInMinuts { get; set; }
        public DefaultTranslationOption[] DefaultTranslations { get; set; } = Array.Empty<DefaultTranslationOption>();

    }
}
