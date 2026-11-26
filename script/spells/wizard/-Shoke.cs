using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.script.spells
{
    public class Shoke :Spell
    {
        public Shoke()
            : base(new script.item.ShokeBook(1))
        {
        }

    }
}
