﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MMALSharp.Native;

namespace MMALSharp.Callbacks
{
    public interface ICallbackHandler
    {
        void Callback(MMALBufferImpl buffer);
    }
}
