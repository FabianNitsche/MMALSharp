﻿// <copyright file="MMALDownstreamHandlerComponent.cs" company="Techyian">
// Copyright (c) Ian Auty. All rights reserved.
// Licensed under the MIT License. Please see LICENSE.txt for License info.
// </copyright>

using MMALSharp.Handlers;

namespace MMALSharp.Components
{
    public abstract class MMALDownstreamHandlerComponent : MMALDownstreamComponent
    {
        protected MMALDownstreamHandlerComponent(string name, params ICaptureHandler[] handlers)
            : base(name)
        {
            if (handlers != null)
            {
                for (var i = 0; i < this.Outputs.Count; i++)
                {
                    this.Outputs[i].Handler = handlers[i];
                }    
            }
        }
    }
}
