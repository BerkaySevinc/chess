using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BekoS.Chess;

public interface IKeepEverPlayed
{
    public bool IsEverPlayed { get; set; }
}