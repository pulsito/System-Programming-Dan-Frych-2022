using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Reproductor
{
    class VolumeEffect : ISampleProvider
    {
        private float volume;
        public float Volume
        {
            get
            {
                return volume;
            }
            set
            {
                if (value < 0)
                {
                    volume = 0;
                }
                else if (value > 1)
                {
                    volume = 1;
                }
                else
                {
                    volume = value;
                }
            }
        }

        private ISampleProvider source;

        public VolumeEffect(ISampleProvider source)
        {
            this.source = source;
            volume = 1;
        }

       
        public WaveFormat WaveFormat {
            get
            {
                return source.WaveFormat;
            }
        }

       
        public int Read(float[] buffer, int offset, int count)
        {
            var read = source.Read(buffer, offset, count);
            
          
            for (int i = 0; i < read; i++)
            {
                
                buffer[offset + i] *= volume;
            }

            return read;
        }
    }
}
