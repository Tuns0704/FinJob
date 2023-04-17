namespace finjob_chatService.Models
{
    public class ChatMessage
    {
        public int Id { get; set; }
        public string SendBy { get; set; }
        public string SendTo { get; set; }
        public string Message { get; set; }
    }
}
