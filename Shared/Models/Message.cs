using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace RealTimeChat.Shared.Models
{
    public class Message
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Key]
        public long MessageId { get; set; }
        public string BodyText { get; set; }
        public DateTime SentAt { get; set; }
        public DateTime ReadAt { get; set; }
    }
}
