using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace VFT.ChartSingalR.Models
{
    public class MessageContext
    {
        [Key]
        public int Id { get; set; }
        public string MessageContent { get; set; }
        public string UserId { get; set; }
        public string Timestamp { get; set; }

        public int MessageId { get; set; }
        public virtual Message Message { get; set; }
    }
}
