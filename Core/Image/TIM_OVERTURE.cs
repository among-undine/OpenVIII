﻿using System.IO;
using System.Linq;

namespace OpenVIII
{
    /// <summary>
    /// TIM data for overture textures.
    /// </summary>
    /// <remarks>
    /// The reason for this is the lack of header, And allows compatablility with other objects that
    /// use TIMs.
    /// </remarks>
    public sealed class TIM_OVERTURE : TIM2
    {
        public TIM_OVERTURE(byte[] buffer, uint offset = 0) : base() => _Init(buffer, offset);

        public TIM_OVERTURE(BinaryReader br, uint offset = 0) : base() => _Init(br, offset);

        private TIM_OVERTURE()
        {
        }

        private new void _Init(byte[] buffer, uint offset)
        {
            this.buffer = buffer;
            using (BinaryReader br = new BinaryReader(new MemoryStream(buffer)))
            {
                Init(br, offset);
            }
        }

        private new void _Init(BinaryReader br, uint offset)
        {
            trimExcess = true;
            br.BaseStream.Seek(offset, SeekOrigin.Begin);
            buffer = br.ReadBytes((int)(br.BaseStream.Length - br.BaseStream.Position));
            using (BinaryReader br2 = new BinaryReader(new MemoryStream(buffer)))
                Init(br2, 0);
        }

        private new void Init(BinaryReader br, uint offset)
        {
            bpp = 16;
            timOffset = offset;
            ReadParameters(br);
        }

        private new void ReadParameters(BinaryReader br)
        {
            br.BaseStream.Seek(timOffset, SeekOrigin.Begin);
            texture.ImageOrgX = br.ReadUInt16();
            texture.ImageOrgY = br.ReadUInt16();
            texture.Width = br.ReadUInt16();
            texture.Height = br.ReadUInt16();
            texture.ImageSize = (uint)(br.BaseStream.Length - timOffset);
            texture.ImageDataSize = (int)(br.BaseStream.Length - br.BaseStream.Position);
            textureDataPointer = (uint)br.BaseStream.Position;
            br.BaseStream.Seek(timOffset, SeekOrigin.Begin);
            if (trimExcess)
                buffer = buffer.Skip((int)timOffset).Take((int)(texture.ImageDataSize + textureDataPointer - timOffset)).ToArray();
        }

        ///// <summary>
        ///// Splash is 640x400 16BPP typical TIM with palette of ggg bbbbb a rrrrr gg
        ///// </summary>
        ///// <param name="buffer">raw 16bpp image</param>
        ///// <returns>Texture2D</returns>
        ///// <remarks>
        ///// These files are just the image data with no header and no clut data. Tim class doesn't
        ///// handle this.
        ///// </remarks>
        //public static Texture2D Overture(byte[] buffer)
        //{
        //    using (MemoryStream ms = new MemoryStream(buffer))
        //    using (BinaryReader br = new BinaryReader(ms))
        //    {
        //        //var ImageOrgX = BitConverter.ToUInt16(buffer, 0x00);
        //        //var ImageOrgY = BitConverter.ToUInt16(buffer, 0x02);

        //        ms.Seek(0x04, SeekOrigin.Begin);
        //        ushort Width = br.ReadUInt16();
        //        ushort Height = br.ReadUInt16();
        //        Texture2D splashTex = new Texture2D(Memory.graphics.GraphicsDevice, Width, Height, false, SurfaceFormat.Color);
        //        lock (splashTex)
        //        {
        //            Color[] rgbBuffer = new Color[Width * Height];
        //            for (int i = 0; i < rgbBuffer.Length && ms.Position + 2 < ms.Length; i++)
        //            {
        //                rgbBuffer[i] = ABGR1555toRGBA32bit(br.ReadUInt16(), true);
        //            }
        //            splashTex.SetData(rgbBuffer);
        //        }
        //        return splashTex;
        //    }
        //}
    }
}