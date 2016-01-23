namespace TwitterClient.Contracts.Status
{
    /// <summary>
    /// Коллекция авторов твита. Обычно один.
    /// Заполняется, если это коллективный аккаунт.
    /// </summary>
    public class Contributor
    {
        public ulong Id { get; set; }
        public string ScreenName { get; set; }
    }
}