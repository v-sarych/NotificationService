using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Notification
{
    public class Notification
    {
        public ulong UserId { get; set; }
        public byte[] Payload { get; set; }
        public long DateOfCreation {  get; set; }
    }
}
