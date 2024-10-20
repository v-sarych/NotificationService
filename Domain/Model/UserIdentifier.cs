using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class UserIdentifier
    {
        public ulong Value{ get; private set; }
        public UserIdentifier(ulong value)
             => Value = value;

        public UserIdentifier(string value)
        => Value = Convert.ToUInt64(value);

        public override string ToString() => Value.ToString();
    }
}
