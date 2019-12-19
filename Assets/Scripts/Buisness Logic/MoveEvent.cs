using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MoveEvent: EventArgs
    {
        public Move MyMove { get; set; }
        public Guid MySecurityHandle { get; set; }
    }

