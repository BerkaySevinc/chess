using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess;

public interface IMove
{
    public Board Board { get; }

    public Square From { get; }
    public Square To { get; }
}
