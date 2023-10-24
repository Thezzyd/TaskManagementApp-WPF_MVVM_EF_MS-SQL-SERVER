
namespace TaskManagementApp.Services
{
    public class ServiceResopnse<T>
    {
        public T Data { get; set; }
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
    }
}
