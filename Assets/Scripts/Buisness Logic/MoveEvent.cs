using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;

public class MoveEvent: EventArgs
    {
        public Move MyMove { get; set; }
        public GUID MySecurityHandle { get; set; }
    }

