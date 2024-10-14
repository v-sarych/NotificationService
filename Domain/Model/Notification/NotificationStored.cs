using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Notification
{
    public class NotificationStored
    {
        public UserIdentifier UserId { get; set; }
        public byte[] Payload { get; set; }
        public long DateOfCreation {  get; private set; }

        public NotificationStored SetNowDateOfCreation()
        {
            DateOfCreation = ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeSeconds();
            return this;
        }
    }
}
