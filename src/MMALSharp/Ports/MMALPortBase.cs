﻿// <copyright file="MMALPortBase.cs" company="Techyian">
// Copyright (c) Ian Auty. All rights reserved.
// Licensed under the MIT License. Please see LICENSE.txt for License info.
// </copyright>

using System;
using System.Drawing;
using System.Runtime.InteropServices;
using MMALSharp.Callbacks;
using MMALSharp.Common.Utility;
using MMALSharp.Native;
using MMALSharp.Components;
using Nito.AsyncEx;
using static MMALSharp.MMALCallerHelper;

namespace MMALSharp
{
    public enum PortType
    {
        Input,
        Output,
        Clock,
        Control,
        Unknown
    }

    /// <summary>
    /// Base class for port objects.
    /// </summary>
    public abstract unsafe class MMALPortBase : MMALObject
    {
        /// <summary>
        /// Specifies the type of port this is.
        /// </summary>
        public PortType PortType { get; set; }

        /// <summary>
        /// Managed reference to the component this port is associated with.
        /// </summary>
        public MMALComponentBase ComponentReference { get; set; }

        /// <summary>
        /// Managed reference to the downstream component this port is connected to.
        /// </summary>
        public MMALConnectionImpl ConnectedReference { get; set; }

        /// <summary>
        /// Managed reference to the pool of buffer headers associated with this port.
        /// </summary>
        public MMALPoolImpl BufferPool { get; set; }

        /// <summary>
        /// Managed name given to this object (user defined).
        /// </summary>
        public Guid Guid { get; set; }

        /// <summary>
        /// The MMALEncoding encoding type that this port will process data in. Helpful for retrieving encoding name/FourCC value.
        /// </summary>
        public MMALEncoding EncodingType { get; set; }

        /// <summary>
        /// The MMALEncoding pixel format that this port will process data in. Helpful for retrieving encoding name/FourCC value.
        /// </summary>
        public MMALEncoding PixelFormat { get; set; }

        /// <summary>
        /// Indicates whether ZeroCopy mode should be enabled on this port. When enabled, data is not copied to the ARM processor and is handled directly by the GPU. Useful when
        /// transferring large amounts of data or raw capture.
        /// See: https://www.raspberrypi.org/forums/viewtopic.php?t=170024
        /// </summary>
        public bool ZeroCopy { get; internal set; }

        #region Native properties

        /// <summary>
        /// Native name of port.
        /// </summary>
        public string Name => Marshal.PtrToStringAnsi((IntPtr)this.Ptr->Name);

        /// <summary>
        /// Indicates whether this port is enabled.
        /// </summary>
        public bool Enabled => this.Ptr->IsEnabled == 1;

        /// <summary>
        /// Specifies minimum number of buffer headers required for this port.
        /// </summary>
        public int BufferNumMin => this.Ptr->BufferNumMin;
        
        /// <summary>
        /// Specifies minimum size of buffer headers required for this port.
        /// </summary>
        public int BufferSizeMin => this.Ptr->BufferSizeMin;

        /// <summary>
        /// Specifies minimum alignment value for buffer headers required for this port.
        /// </summary>
        public int BufferAlignmentMin => this.Ptr->BufferAlignmentMin;

        /// <summary>
        /// Specifies recommended number of buffer headers for this port.
        /// </summary>
        public int BufferNumRecommended => this.Ptr->BufferNumRecommended;

        /// <summary>
        /// Specifies recommended size of buffer headers for this port.
        /// </summary>
        public int BufferSizeRecommended => this.Ptr->BufferSizeRecommended;

        /// <summary>
        /// Indicates the currently set number of buffer headers for this port.
        /// </summary>
        public int BufferNum
        {
            get => this.Ptr->BufferNum;
            internal set => this.Ptr->BufferNum = value;
        }

        /// <summary>
        /// Indicates the currently set size of buffer headers for this port.
        /// </summary>
        public int BufferSize
        {
            get => this.Ptr->BufferSize;
            internal set => this.Ptr->BufferSize = value;
        }

        /// <summary>
        /// Accessor for the elementary stream.
        /// </summary>
        public MMAL_ES_FORMAT_T Format => *this.Ptr->Format;

        /// <summary>
        /// The Width/Height that this port will process data in.
        /// </summary>
        public Resolution Resolution
        {
            get => new Resolution(this.Ptr->Format->Es->Video.Width, this.Ptr->Format->Es->Video.Height);
            internal set
            {
                this.Ptr->Format->Es->Video.Width = value.Width;
                this.Ptr->Format->Es->Video.Height = value.Height;
            }
        }

        /// <summary>
        /// The region of interest that this port will process data in.
        /// </summary>
        public Rectangle Crop
        {
            get => new Rectangle(this.Ptr->Format->Es->Video.Crop.X, this.Ptr->Format->Es->Video.Crop.Y, this.Ptr->Format->Es->Video.Crop.Width, this.Ptr->Format->Es->Video.Crop.Height);
            internal set => this.Ptr->Format->Es->Video.Crop = new MMAL_RECT_T(value.X, value.Y, value.Width, value.Height);
        }

        /// <summary>
        /// The framerate we are processing data in.
        /// </summary>
        public MMAL_RATIONAL_T FrameRate
        {
            get => this.Ptr->Format->Es->Video.FrameRate;
            internal set => this.Ptr->Format->Es->Video.FrameRate = new MMAL_RATIONAL_T(value.Num, value.Den);
        }
        
        /// <summary>
        /// The Region of Interest width that this port will process data in.
        /// </summary>
        public int CropWidth => this.Ptr->Format->Es->Video.Crop.Width;

        /// <summary>
        /// The Region of Interest height that this port will process data in.
        /// </summary>
        public int CropHeight => this.Ptr->Format->Es->Video.Crop.Height;

        /// <summary>
        /// The encoding type that this port will process data in.
        /// </summary>
        public int NativeEncodingType
        {
            get => this.Ptr->Format->Encoding;
            internal set => this.Ptr->Format->Encoding = value;
        }

        /// <summary>
        /// The pixel format that this port will process data in.
        /// </summary>
        public int NativeEncodingSubformat
        {
            get => this.Ptr->Format->EncodingVariant;
            internal set => this.Ptr->Format->EncodingVariant = value;
        } 

        #endregion
        
        /// <summary>
        /// Asynchronous trigger which is set when processing has completed on this port.
        /// </summary>
        public AsyncCountdownEvent Trigger { get; set; }

        /// <summary>
        /// A callback handler for Input ports to populate native buffer header with user provided image data.
        /// </summary>
        public InputCallbackHandlerBase ManagedInputCallback { get; set; }

        /// <summary>
        /// A callback handler for Output ports we use to do further processing on buffer headers after they've been received by the native callback delegate.
        /// </summary>
        public OutputCallbackHandlerBase ManagedOutputCallback { get; set; }

        /// <summary>
        /// Native pointer that represents the component this port is associated with.
        /// </summary>
        internal MMAL_COMPONENT_T* Comp { get; set; }

        /// <summary>
        /// Native pointer that represents this port.
        /// </summary>
        internal MMAL_PORT_T* Ptr { get; set; }

        /// <summary>
        /// Monitor lock for input port callback method.
        /// </summary>
        internal static object InputLock = new object();

        /// <summary>
        /// Monitor lock for output port callback method.
        /// </summary>
        internal static object OutputLock = new object();

        /// <summary>
        /// Native pointer to the native callback function.
        /// </summary>
        internal IntPtr PtrCallback { get; set; }

        /// <summary>
        /// Delegate for native port callback.
        /// </summary>
        internal MMALPort.MMAL_PORT_BH_CB_T NativeCallback { get; set; }

        /// <summary>
        /// Creates a new managed reference to a MMAL Component Port.
        /// </summary>
        /// <param name="ptr">The native pointer to the component port.</param>
        /// <param name="comp">The component this port is associated with.</param>
        /// <param name="type">The type of port this is.</param>
        protected MMALPortBase(MMAL_PORT_T* ptr, MMALComponentBase comp, PortType type, Guid guid)
        {
            this.Ptr = ptr;
            this.Comp = ptr->Component;
            this.ComponentReference = comp;
            this.PortType = type;
            this.Guid = guid;
        }

        /// <summary>
        /// Connects two components together by their input and output ports.
        /// </summary>
        /// <param name="destinationComponent">The component we want to connect to.</param>
        /// <param name="inputPort">The input port of the component we want to connect to.</param>
        /// <param name="useCallback">Flag to use connection callback (adversely affects performance).</param>
        /// <returns>The input port of the component we're connecting to - allows chain calling of this method.</returns>
        public MMALPortBase ConnectTo(MMALDownstreamComponent destinationComponent, int inputPort = 0, bool useCallback = false)
        {
            if (this.ConnectedReference != null)
            {
                MMALLog.Logger.Warn("A connection has already been established on this port");
                return destinationComponent.Inputs[inputPort];
            }

            var connection = MMALConnectionImpl.CreateConnection(this, destinationComponent.Inputs[inputPort], destinationComponent, useCallback);
            this.ConnectedReference = connection;
            destinationComponent.Inputs[inputPort].ConnectedReference = connection;

            return destinationComponent.Inputs[inputPort];
        }

        /// <summary>
        /// Connects two components together by their input and output ports.
        /// </summary>
        /// <param name="destinationComponent">The component we want to connect to.</param>
        /// <param name="inputPort">The input port of the component we want to connect to.</param>
        /// <param name="callback">An operation we would like to carry out after connecting these components together.</param>
        /// <returns>The input port of the component we're connecting to - allows chain calling of this method.</returns>
        public MMALPortBase ConnectTo(MMALDownstreamComponent destinationComponent, int inputPort, Func<MMALPortBase> callback)
        {
            this.ConnectTo(destinationComponent, inputPort);
            callback();
            return destinationComponent.Inputs[inputPort];
        }

        /// <summary>
        /// Represents the native callback method for an input port that's called by MMAL.
        /// </summary>
        /// <param name="port">Native port struct pointer.</param>
        /// <param name="buffer">Native buffer header pointer.</param>
        internal virtual void NativeInputPortCallback(MMAL_PORT_T* port, MMAL_BUFFER_HEADER_T* buffer)
        {
        }

        /// <summary>
        /// Represents the native callback method for an output port that's called by MMAL.
        /// </summary>
        /// <param name="port">Native port struct pointer.</param>
        /// <param name="buffer">Native buffer header pointer.</param>
        internal virtual void NativeOutputPortCallback(MMAL_PORT_T* port, MMAL_BUFFER_HEADER_T* buffer)
        {
        }

        /// <summary>
        /// Represents the native callback method for a control port that's called by MMAL.
        /// </summary>
        /// <param name="port">Native port struct pointer.</param>
        /// <param name="buffer">Native buffer header pointer.</param>
        internal virtual void NativeControlPortCallback(MMAL_PORT_T* port, MMAL_BUFFER_HEADER_T* buffer)
        {
        }

        /// <summary>
        /// Provides functionality to enable processing on a control port.
        /// </summary>
        internal virtual void EnableControlPort()
        {
        }
        
        /// <summary>
        /// Provides functionality to enable processing on an output port.
        /// </summary>
        /// <param name="sendBuffers">Indicates whether we want to send all the buffers in the port pool or simply create the pool.</param>
        internal virtual void EnableOutputPort(bool sendBuffers = true)
        {
            if (this.ManagedOutputCallback != null)
            {
                this.SendAllBuffers(sendBuffers);
            }
        }

        /// <summary>
        /// Provides functionality to enable processing on an input port.
        /// </summary>
        internal virtual void EnableInputPort()
        {
        }

        /// <summary>
        /// Disable processing on a port. Disabling a port will stop all processing on this port and return all (non-processed)
        /// buffer headers to the client. If this is a connected output port, the input port to which it is connected shall also be disabled.
        /// Any buffer pool shall be released.
        /// </summary>
        internal void DisablePort()
        {
            if (this.Enabled)
            {
                MMALLog.Logger.Debug("Disabling port");

                if (this.BufferPool != null)
                {
                    var length = this.BufferPool.HeadersNum;

                    MMALLog.Logger.Debug($"Releasing {length} buffers from queue.");

                    for (int i = 0; i < length; i++)
                    {
                        MMALLog.Logger.Debug("Releasing active buffer");
                        var buffer = this.BufferPool.Queue.GetBuffer();
                        
                        if (buffer != null)
                        {
                            buffer.Release();
                        }
                        else
                        {
                            MMALLog.Logger.Warn("Retrieved buffer invalid. Waiting for disable...");
                        }
                    }
                }

                MMALCheck(MMALPort.mmal_port_disable(this.Ptr), "Unable to disable port.");
            }
        }

        /// <summary>
        /// Commit format changes on this port.
        /// </summary>
        /// <exception cref="MMALException"/>
        internal void Commit()
        {
            MMALLog.Logger.Debug("Committing port format changes");
            MMALCheck(MMALPort.mmal_port_format_commit(this.Ptr), "Unable to commit port changes.");
        }

        /// <summary>
        /// Shallow copy a format structure. It is worth noting that the extradata buffer will not be copied in the new format.
        /// </summary>
        /// <param name="destination">The destination port we're copying to.</param>
        internal void ShallowCopy(MMALPortBase destination)
        {
            MMALLog.Logger.Debug("Shallow copy port format");
            MMALFormat.mmal_format_copy(destination.Ptr->Format, this.Ptr->Format);
        }

        /// <summary>
        /// Shallow copy a format structure. It is worth noting that the extradata buffer will not be copied in the new format.
        /// </summary>
        /// <param name="eventFormatSource">The source event format we're copying from.</param>
        internal void ShallowCopy(MMALEventFormat eventFormatSource)
        {
            MMALLog.Logger.Debug("Shallow copy event format");
            MMALFormat.mmal_format_copy(this.Ptr->Format, eventFormatSource.Ptr);
        }

        /// <summary>
        /// Fully copy a format structure, including the extradata buffer.
        /// </summary>
        /// <param name="destination">The destination port we're copying to.</param>
        internal void FullCopy(MMALPortBase destination)
        {
            MMALLog.Logger.Debug("Full copy port format");
            MMALFormat.mmal_format_full_copy(destination.Ptr->Format, this.Ptr->Format);
        }

        /// <summary>
        /// Fully copy a format structure, including the extradata buffer.
        /// </summary>
        /// <param name="eventFormatSource">The source event format we're copying from.</param>
        internal void FullCopy(MMALEventFormat eventFormatSource)
        {
            MMALLog.Logger.Debug("Full copy event format");
            MMALFormat.mmal_format_full_copy(this.Ptr->Format, eventFormatSource.Ptr);
        }

        /// <summary>
        /// Ask a port to release all the buffer headers it currently has. This is an asynchronous operation and the
        /// flush call will return before all the buffer headers are returned to the client.
        /// </summary>
        internal void Flush()
        {            
            MMALLog.Logger.Debug("Flushing port buffers");
            MMALCheck(MMALPort.mmal_port_flush(this.Ptr), "Unable to flush port.");
        }

        /// <summary>
        /// Send a buffer header to a port.
        /// </summary>
        /// <param name="buffer">A managed buffer object.</param>
        internal void SendBuffer(MMALBufferImpl buffer)
        {
            if (this.Enabled)
            {
                if (MMALCameraConfig.Debug)
                {
                    MMALLog.Logger.Debug("Sending buffer");
                }

                MMALCheck(MMALPort.mmal_port_send_buffer(this.Ptr, buffer.Ptr), "Unable to send buffer header.");
            }
        }

        internal void SendAllBuffers(bool sendBuffers = true)
        {
            this.InitialiseBufferPool();
            
            if (sendBuffers)
            {
                var length = this.BufferPool.Queue.QueueLength();

                for (int i = 0; i < length; i++)
                {
                    var buffer = this.BufferPool.Queue.GetBuffer();

                    MMALLog.Logger.Debug($"Sending buffer to output port: Length {buffer.Length}");

                    this.SendBuffer(buffer);
                }
            }            
        }

        internal void SendAllBuffers(MMALPoolImpl pool)
        {
            var length = pool.Queue.QueueLength();

            for (int i = 0; i < length; i++)
            {
                var buffer = pool.Queue.GetBuffer();

                MMALLog.Logger.Debug($"Sending buffer to output port: Length {buffer.Length}");

                this.SendBuffer(buffer);
            }
        }

        /// <summary>
        /// Destroy a pool of MMAL_BUFFER_HEADER_T associated with a specific port. This will also deallocate all of the memory
        /// which was allocated when creating or resizing the pool.
        /// </summary>
        internal void DestroyPortPool()
        {
            if (this.BufferPool != null)
            {
                this.DisablePort();
                MMALUtil.mmal_port_pool_destroy(this.Ptr, this.BufferPool.Ptr);
            }
        }

        /// <summary>
        /// Releases an input port buffer and reads further data from user provided image data if not reached end of file.
        /// </summary>
        /// <param name="bufferImpl">A managed buffer object.</param>
        internal void ReleaseInputBuffer(MMALBufferImpl bufferImpl)
        {            
            bufferImpl.Release();
            
            if (this.Enabled && this.BufferPool != null)
            {
                MMALBufferImpl newBuffer;
                while (true)
                {
                    newBuffer = this.BufferPool.Queue.GetBuffer();
                    if (newBuffer != null)
                    {
                        break;
                    }
                }
                
                // Populate the new input buffer with user provided image data.
                var result = this.ManagedInputCallback.Callback(newBuffer);
                newBuffer.ReadIntoBuffer(result.BufferFeed, result.DataLength, result.EOF);

                try
                {
                    if (this.Trigger != null && this.Trigger.CurrentCount > 0 && result.EOF)
                    {
                        MMALLog.Logger.Debug("Received EOF. Releasing.");

                        this.Trigger.Signal();
                        newBuffer.Release();                        
                        newBuffer = null;
                    }

                    if (newBuffer != null)
                    {
                        this.SendBuffer(newBuffer);
                    }
                    else
                    {
                        MMALLog.Logger.Warn("Buffer null. Continuing.");
                    }
                }
                catch (Exception ex)
                {
                    MMALLog.Logger.Warn($"Buffer handling failed. {ex.Message}");
                    throw;
                }
            }
        }
        
        /// <summary>
        /// Release an output port buffer, get a new one from the queue and send it for processing.
        /// </summary>
        /// <param name="bufferImpl">A managed buffer object.</param>
        internal void ReleaseOutputBuffer(MMALBufferImpl bufferImpl)
        {
            bufferImpl.Release();
            bufferImpl.Dispose();
            try
            {
                if (!this.Enabled)
                {
                    MMALLog.Logger.Warn("Port not enabled.");
                }

                if (this.BufferPool == null)
                {
                    MMALLog.Logger.Warn("Buffer pool null.");
                }

                if (this.Enabled && this.BufferPool != null)
                {
                    var newBuffer = MMALQueueImpl.GetBuffer(this.BufferPool.Queue.Ptr);

                    if (newBuffer != null)
                    {
                        this.SendBuffer(newBuffer);
                    }
                    else
                    {
                        MMALLog.Logger.Warn("Buffer null. Continuing.");
                    }
                }
            }
            catch (Exception e)
            {
                MMALLog.Logger.Warn($"Unable to send buffer header. {e.Message}");
            }
        }

        internal void InitialiseBufferPool()
        {
            MMALLog.Logger.Debug($"Initialising buffer pool.");
            this.BufferPool = new MMALPoolImpl(this);
        }
    }
}
