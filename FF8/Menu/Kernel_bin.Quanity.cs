﻿using System;

namespace FF8
{
    internal partial class Kernel_bin
    {
        [Flags]
        internal enum Quanity
        {
            //0% = 0x00,
            //6.25% = 0x01,
            //12.50% = 0x02,
            //25% = 0x04,
            //50% = 0x08,
            //100% = 0x10,

            _0f = 0x00,
            _0625f = 0x01,
            _1250f = 0x02,
            _25f = 0x04,
            _50f = 0x08,
            _1f = 0x10,
        }
    }
}