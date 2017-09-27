using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace kagv

{
    public enum DiagonalMovement
    {
        Always,
        Never,
        IfAtLeastOneWalkable,
        OnlyWhenNoObstacles
    }
}
