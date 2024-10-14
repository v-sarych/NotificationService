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

        public override string ToString() => Value.ToString();
    }
}
