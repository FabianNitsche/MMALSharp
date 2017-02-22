﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMALSharp.Native
{
    
    public static class MMALEncodings
    {
        public static int MMAL_ENCODING_H264 = MMALUtil.MMAL_FOURCC("H264");
        public static int MMAL_ENCODING_MVC = MMALUtil.MMAL_FOURCC("MVC ");
        public static int MMAL_ENCODING_H263 = MMALUtil.MMAL_FOURCC("H263");
        public static int MMAL_ENCODING_MP4V = MMALUtil.MMAL_FOURCC("MP4V");
        public static int MMAL_ENCODING_MP2V = MMALUtil.MMAL_FOURCC("MP2V");
        public static int MMAL_ENCODING_MP1V = MMALUtil.MMAL_FOURCC("MP1V");
        public static int MMAL_ENCODING_WMV3 = MMALUtil.MMAL_FOURCC("WMV3");
        public static int MMAL_ENCODING_WMV2 = MMALUtil.MMAL_FOURCC("WMV2");
        public static int MMAL_ENCODING_WMV1 = MMALUtil.MMAL_FOURCC("WMV1");
        public static int MMAL_ENCODING_WVC1 = MMALUtil.MMAL_FOURCC("WVC1");
        public static int MMAL_ENCODING_VP8 = MMALUtil.MMAL_FOURCC("VP8 ");
        public static int MMAL_ENCODING_VP7 = MMALUtil.MMAL_FOURCC("VP7 ");
        public static int MMAL_ENCODING_VP6 = MMALUtil.MMAL_FOURCC("VP6 ");
        public static int MMAL_ENCODING_THEORA = MMALUtil.MMAL_FOURCC("THEO");
        public static int MMAL_ENCODING_SPARK = MMALUtil.MMAL_FOURCC("SPRK");
        public static int MMAL_ENCODING_MJPEG = MMALUtil.MMAL_FOURCC("MJPG");
        public static int MMAL_ENCODING_JPEG = MMALUtil.MMAL_FOURCC("JPEG");
        public static int MMAL_ENCODING_GIF = MMALUtil.MMAL_FOURCC("GIF ");
        public static int MMAL_ENCODING_PNG = MMALUtil.MMAL_FOURCC("PNG ");
        public static int MMAL_ENCODING_PPM = MMALUtil.MMAL_FOURCC("PPM ");
        public static int MMAL_ENCODING_TGA = MMALUtil.MMAL_FOURCC("TGA ");
        public static int MMAL_ENCODING_BMP = MMALUtil.MMAL_FOURCC("BMP ");
        public static int MMAL_ENCODING_I420 = MMALUtil.MMAL_FOURCC("I420");
        public static int MMAL_ENCODING_I420_SLICE = MMALUtil.MMAL_FOURCC("S420");
        public static int MMAL_ENCODING_YV12 = MMALUtil.MMAL_FOURCC("YV12");
        public static int MMAL_ENCODING_I422 = MMALUtil.MMAL_FOURCC("I422");
        public static int MMAL_ENCODING_I422_SLICE = MMALUtil.MMAL_FOURCC("S422");
        public static int MMAL_ENCODING_YUYV = MMALUtil.MMAL_FOURCC("YUYV");
        public static int MMAL_ENCODING_YVYU = MMALUtil.MMAL_FOURCC("YVYU");
        public static int MMAL_ENCODING_UYVY = MMALUtil.MMAL_FOURCC("UYVY");
        public static int MMAL_ENCODING_VYUY = MMALUtil.MMAL_FOURCC("VYUY");
        public static int MMAL_ENCODING_NV12 = MMALUtil.MMAL_FOURCC("NV12");
        public static int MMAL_ENCODING_NV21 = MMALUtil.MMAL_FOURCC("NV21");
        public static int MMAL_ENCODING_ARGB = MMALUtil.MMAL_FOURCC("ARGB");
        public static int MMAL_ENCODING_RGBA = MMALUtil.MMAL_FOURCC("RGBA");
        public static int MMAL_ENCODING_ABGR = MMALUtil.MMAL_FOURCC("ABGR");
        public static int MMAL_ENCODING_BGRA = MMALUtil.MMAL_FOURCC("BGRA");
        public static int MMAL_ENCODING_RGB16 = MMALUtil.MMAL_FOURCC("RGB2");
        public static int MMAL_ENCODING_RGB24 = MMALUtil.MMAL_FOURCC("RGB3");
        public static int MMAL_ENCODING_RGB32 = MMALUtil.MMAL_FOURCC("RGB4");
        public static int MMAL_ENCODING_BGR16 = MMALUtil.MMAL_FOURCC("BGR2");
        public static int MMAL_ENCODING_BGR24 = MMALUtil.MMAL_FOURCC("BGR3");
        public static int MMAL_ENCODING_BGR32 = MMALUtil.MMAL_FOURCC("BGR4");
        public static int MMAL_ENCODING_BAYER_SBGGR10P = MMALUtil.MMAL_FOURCC("pBAA");
        public static int MMAL_ENCODING_BAYER_SBGGR8 = MMALUtil.MMAL_FOURCC("BA81");
        public static int MMAL_ENCODING_BAYER_SBGGR12P = MMALUtil.MMAL_FOURCC("BY12");
        public static int MMAL_ENCODING_BAYER_SBGGR16 = MMALUtil.MMAL_FOURCC("BYR2");
        public static int MMAL_ENCODING_BAYER_SBGGR10DPCM8 = MMALUtil.MMAL_FOURCC("bBA8");
        public static int MMAL_ENCODING_YUVUV128 = MMALUtil.MMAL_FOURCC("SAND");
        public static int MMAL_ENCODING_OPAQUE = MMALUtil.MMAL_FOURCC("OPQV");
        public static int MMAL_ENCODING_EGL_IMAGE = MMALUtil.MMAL_FOURCC("EGLI");
        public static int MMAL_ENCODING_PCM_UNSIGNED_BE = MMALUtil.MMAL_FOURCC("PCMU");
        public static int MMAL_ENCODING_PCM_UNSIGNED_LE = MMALUtil.MMAL_FOURCC("pcmu");
        public static int MMAL_ENCODING_PCM_SIGNED_BE = MMALUtil.MMAL_FOURCC("PCMS");
        public static int MMAL_ENCODING_PCM_SIGNED_LE = MMALUtil.MMAL_FOURCC("pcms");
        public static int MMAL_ENCODING_PCM_FLOAT_BE = MMALUtil.MMAL_FOURCC("PCMF");
        public static int MMAL_ENCODING_PCM_FLOAT_LE = MMALUtil.MMAL_FOURCC("pcmf");
        public static int MMAL_ENCODING_PCM_UNSIGNED = MMAL_ENCODING_PCM_UNSIGNED_LE;
        public static int MMAL_ENCODING_PCM_SIGNED = MMAL_ENCODING_PCM_SIGNED_LE;
        public static int MMAL_ENCODING_PCM_FLOAT = MMAL_ENCODING_PCM_FLOAT_LE;
        public static int MMAL_ENCODING_MP4A = MMALUtil.MMAL_FOURCC("MP4A");
        public static int MMAL_ENCODING_MPGA = MMALUtil.MMAL_FOURCC("MPGA");
        public static int MMAL_ENCODING_ALAW = MMALUtil.MMAL_FOURCC("ALAW");
        public static int MMAL_ENCODING_MULAW = MMALUtil.MMAL_FOURCC("ULAW");
        public static int MMAL_ENCODING_ADPCM_MS = MMALUtil.MMAL_FOURCC("MS\x00\x02");
        public static int MMAL_ENCODING_ADPCM_IMA_MS = MMALUtil.MMAL_FOURCC("MS\x00\x01");
        public static int MMAL_ENCODING_ADPCM_SWF = MMALUtil.MMAL_FOURCC("ASWF");
        public static int MMAL_ENCODING_WMA1 = MMALUtil.MMAL_FOURCC("WMA1");
        public static int MMAL_ENCODING_WMA2 = MMALUtil.MMAL_FOURCC("WMA2");
        public static int MMAL_ENCODING_WMAP = MMALUtil.MMAL_FOURCC("WMAP");
        public static int MMAL_ENCODING_WMAL = MMALUtil.MMAL_FOURCC("WMAL");
        public static int MMAL_ENCODING_WMAV = MMALUtil.MMAL_FOURCC("WMAV");
        public static int MMAL_ENCODING_AMRNB = MMALUtil.MMAL_FOURCC("AMRN");
        public static int MMAL_ENCODING_AMRWB = MMALUtil.MMAL_FOURCC("AMRW");
        public static int MMAL_ENCODING_AMRWBP = MMALUtil.MMAL_FOURCC("AMRP");
        public static int MMAL_ENCODING_AC3 = MMALUtil.MMAL_FOURCC("AC3 ");
        public static int MMAL_ENCODING_EAC3 = MMALUtil.MMAL_FOURCC("EAC3");
        public static int MMAL_ENCODING_DTS = MMALUtil.MMAL_FOURCC("DTS ");
        public static int MMAL_ENCODING_MLP = MMALUtil.MMAL_FOURCC("MLP ");
        public static int MMAL_ENCODING_FLAC = MMALUtil.MMAL_FOURCC("FLAC");
        public static int MMAL_ENCODING_VORBIS = MMALUtil.MMAL_FOURCC("VORB");
        public static int MMAL_ENCODING_SPEEX = MMALUtil.MMAL_FOURCC("SPX ");
        public static int MMAL_ENCODING_ATRAC3 = MMALUtil.MMAL_FOURCC("ATR3");
        public static int MMAL_ENCODING_ATRACX = MMALUtil.MMAL_FOURCC("ATRX");
        public static int MMAL_ENCODING_ATRACL = MMALUtil.MMAL_FOURCC("ATRL");
        public static int MMAL_ENCODING_MIDI = MMALUtil.MMAL_FOURCC("MIDI");
        public static int MMAL_ENCODING_EVRC = MMALUtil.MMAL_FOURCC("EVRC");
        public static int MMAL_ENCODING_NELLYMOSER = MMALUtil.MMAL_FOURCC("NELY");
        public static int MMAL_ENCODING_QCELP = MMALUtil.MMAL_FOURCC("QCEL");
        public static int MMAL_ENCODING_MP4V_DIVX_DRM = MMALUtil.MMAL_FOURCC("M4VD");
        public static int MMAL_ENCODING_VARIANT_H264_DEFAULT = 0;
        public static int MMAL_ENCODING_VARIANT_H264_AVC1 = MMALUtil.MMAL_FOURCC("AVC1");
        public static int MMAL_ENCODING_VARIANT_H264_RAW = MMALUtil.MMAL_FOURCC("RAW ");
        public static int MMAL_ENCODING_VARIANT_MP4A_DEFAULT = 0;
        public static int MMAL_ENCODING_VARIANT_MP4A_ADTS = MMALUtil.MMAL_FOURCC("ADTS");
        public static int MMAL_COLOR_SPACE_UNKNOWN = 0;
        public static int MMAL_COLOR_SPACE_ITUR_BT601 = MMALUtil.MMAL_FOURCC("Y601");
        public static int MMAL_COLOR_SPACE_ITUR_BT709 = MMALUtil.MMAL_FOURCC("Y709");
        public static int MMAL_COLOR_SPACE_JPEG_JFIF = MMALUtil.MMAL_FOURCC("YJFI");
        public static int MMAL_COLOR_SPACE_FCC = MMALUtil.MMAL_FOURCC("YFCC");
        public static int MMAL_COLOR_SPACE_SMPTE240M = MMALUtil.MMAL_FOURCC("Y240");
        public static int MMAL_COLOR_SPACE_BT470_2_M = MMALUtil.MMAL_FOURCC("Y__M");
        public static int MMAL_COLOR_SPACE_BT470_2_BG = MMALUtil.MMAL_FOURCC("Y_BG");
        public static int MMAL_COLOR_SPACE_JFIF_Y16_255 = MMALUtil.MMAL_FOURCC("YY16");
    }
}
