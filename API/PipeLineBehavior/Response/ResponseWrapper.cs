namespace TicketManagement.API.PipeLineBehavior.Response
{
    public class ResponseWrapper<T>
    {
        public bool Success { get; set; }
        public T Data { get; set; }
        public IEnumerable<object> Errors { get; set; }
    }
}
