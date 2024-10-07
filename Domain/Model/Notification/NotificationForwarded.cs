using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Notification
{
    public class NotificationForwarded
    {
        public byte[] Data { get; set; }

        public static NotificationForwarded FromByteArray(byte[] bytes)
            => new NotificationForwarded()
            {
                Data = bytes
            };
    }
}
