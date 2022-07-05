using System.ComponentModel.DataAnnotations;

namespace VFT.ChartSingalR.Models
{
    public class Message
    {
        [Key]
        public int Id { get; set; }
        public string UserRenderID { get; set; }
        public string UserReceiverID { get; set; }
        
        //mỗi quan hệ một nhiều
        public ICollection<MessageContext> MessageContexts { get; set; }
    }
}
