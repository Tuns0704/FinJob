using System.ComponentModel.DataAnnotations;

namespace finjob_backend.Models.DTO
{
    public class MessageDTO
    {
        [Required]
        public int ConversationId { get; set; }
        [Required]
        public string Sender { get; set; }
        [Required]
        public string Text { get; set; }
    }
}
