using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace StarComet.Content.src
{
    class Limit
    {
        public void CheckPlayerLimit(Player P, float Distance) 
        {
 
            if ((int)(P.Position.X / Distance) == 0 && (int)(P.Position.Y / Distance) == 0)
            {
                P.Limit = false;
                return;
            }
                P.Limit = true;

        }

    }
}
