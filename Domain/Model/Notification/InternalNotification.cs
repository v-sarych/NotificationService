using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Notification
{
    public class InternalNotification
    {
        public byte[] Data { get; private set; }

        public static InternalNotification FromByteArray(byte[] bytes)
            => new InternalNotification()
            {
                Data = bytes
            };
    }
}
