namespace Mc2.CrudTest.Presentation.Server.Core.Services
{
    public class ResultSet
    {
        public bool IsSucceed { get; set; }
        public string Message { get; set; }
    }

    public class ResultSet<T> : ResultSet
    {
        public T Data { get; set; }
    }
}
