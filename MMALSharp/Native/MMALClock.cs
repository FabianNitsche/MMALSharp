﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MMALSharp.Native
{
    public static class MMALClock
    {
        public static int MMAL_CLOCK_EVENT_MAGIC = MMALUtil.MMAL_FOURCC("CKLM");
        public static int MMAL_CLOCK_EVENT_REFERENCE = MMALUtil.MMAL_FOURCC("CREF");
        public static int MMAL_CLOCK_EVENT_ACTIVE = MMALUtil.MMAL_FOURCC("CACT");
        public static int MMAL_CLOCK_EVENT_SCALE = MMALUtil.MMAL_FOURCC("CSCA");
        public static int MMAL_CLOCK_EVENT_TIME = MMALUtil.MMAL_FOURCC("CTIM");
        public static int MMAL_CLOCK_EVENT_UPDATE_THRESHOLD = MMALUtil.MMAL_FOURCC("CUTH");
        public static int MMAL_CLOCK_EVENT_DISCONT_THRESHOLD = MMALUtil.MMAL_FOURCC("CDTH");
        public static int MMAL_CLOCK_EVENT_REQUEST_THRESHOLD = MMALUtil.MMAL_FOURCC("CRTH");
        public static int MMAL_CLOCK_EVENT_INPUT_BUFFER_INFO = MMALUtil.MMAL_FOURCC("CIBI");
        public static int MMAL_CLOCK_EVENT_OUTPUT_BUFFER_INFO = MMALUtil.MMAL_FOURCC("COBI");
        public static int MMAL_CLOCK_EVENT_LATENCY = MMALUtil.MMAL_FOURCC("CLAT");
        public static int MMAL_CLOCK_EVENT_INVALID = 0;
                
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MMAL_CLOCK_UPDATE_THRESHOLD_T
    {
        public long thresholdLower, thresholdUpper;

        public MMAL_CLOCK_UPDATE_THRESHOLD_T(long thresholdLower, long thresholdUpper)
        {
            this.thresholdLower = thresholdLower;
            this.thresholdUpper = thresholdUpper;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MMAL_CLOCK_DISCONT_THRESHOLD_T
    {
        public long threshold, duration;

        public MMAL_CLOCK_DISCONT_THRESHOLD_T(long threshold, long duration)
        {
            this.threshold = threshold;
            this.duration = duration;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MMAL_CLOCK_REQUEST_THRESHOLD_T
    {
        public long threshold;
        public int thresholdEnable;

        public MMAL_CLOCK_REQUEST_THRESHOLD_T(long threshold, int thresholdEnable)
        {
            this.threshold = threshold;
            this.thresholdEnable = thresholdEnable;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MMAL_CLOCK_BUFFER_INFO_T
    {
        public long timestamp;
        public uint arrivalTime;

        public MMAL_CLOCK_BUFFER_INFO_T(long timestamp, uint arrivalTime)
        {
            this.timestamp = timestamp;
            this.arrivalTime = arrivalTime;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MMAL_CLOCK_LATENCY_T
    {
        public long target, attackPeriod, attackRate;

        public MMAL_CLOCK_LATENCY_T(long target, long attackPeriod, long attackRate)
        {
            this.target = target;
            this.attackPeriod = attackPeriod;
            this.attackRate = attackRate;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct MMAL_CLOCK_EVENT_DATA
    {
        public int enable;
        public MMAL_RATIONAL_T scale;
        public MMAL_CLOCK_UPDATE_THRESHOLD_T updateThreshold;
        public MMAL_CLOCK_DISCONT_THRESHOLD_T discontThreshold;
        public MMAL_CLOCK_REQUEST_THRESHOLD_T requestThreshold;
        public MMAL_CLOCK_BUFFER_INFO_T buffer;
        public MMAL_CLOCK_LATENCY_T latency;

        public MMAL_CLOCK_EVENT_DATA(int enable, MMAL_RATIONAL_T scale, MMAL_CLOCK_UPDATE_THRESHOLD_T updateThreshold,
                                     MMAL_CLOCK_DISCONT_THRESHOLD_T discontThreshold, MMAL_CLOCK_REQUEST_THRESHOLD_T requestThreshold,
                                     MMAL_CLOCK_BUFFER_INFO_T buffer, MMAL_CLOCK_LATENCY_T latency)
        {
            this.enable = enable;
            this.scale = scale;
            this.updateThreshold = updateThreshold;
            this.discontThreshold = discontThreshold;
            this.requestThreshold = requestThreshold;
            this.buffer = buffer;
            this.latency = latency;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MMAL_CLOCK_EVENT_T
    {
        public uint id, magic;
        public MMAL_BUFFER_HEADER_T* buffer;
        public uint padding0;
        public MMAL_CLOCK_EVENT_DATA data;
        public long padding1;

        public MMAL_CLOCK_EVENT_T(uint id, uint magic, MMAL_BUFFER_HEADER_T* buffer, uint padding0,
                                  MMAL_CLOCK_EVENT_DATA data, long padding1)
        {
            this.id = id;
            this.magic = magic;
            this.buffer = buffer;
            this.padding0 = padding0;
            this.data = data;
            this.padding1 = padding1;
        }
    }

}
