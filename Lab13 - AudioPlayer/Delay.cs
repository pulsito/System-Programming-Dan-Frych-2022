using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio.Wave;

namespace Reproductor
{
    class Delay : ISampleProvider
    {
        
        private int buffersize;
        private int durationBufferSeconds;
        private int quantitySamplesElapsed = 0;
        private int quantitySamplesDeleted = 0;
        private int QuantitySamplesOffset = 0;

        private int offsetMiliseconds;
        public int OffsetMiliseconds
        {
            get
            {
                return offsetMiliseconds;
            }
            set
            {
                offsetMiliseconds = value;
                QuantitySamplesOffset = (int)(((float)OffsetMiliseconds / 1000.0f) * (float)source.WaveFormat.SampleRate);
            }
        }

        public bool Active
        {
            get; set;
        }

        public float Gain
        {
            get;
            set;
        }

        private ISampleProvider source;

        private List<float> bufferDelay = new List<float>();
        
        public Delay(ISampleProvider source)
        {
            Active = false;
            this.source = source;
            durationBufferSeconds = 10;

            
            buffersize = source.WaveFormat.SampleRate * durationBufferSeconds;
        }

       
        public WaveFormat WaveFormat
        {
            get
            {
                return source.WaveFormat;
            }
        }

        public int Read(float[] buffer, int offset, int count)
        {
           
            var read = source.Read(buffer, offset, count);

           
            float tiempoTranscurridoSegundos = (float)quantitySamplesElapsed / (float)source.WaveFormat.SampleRate;
            float millisecondsElapsed = tiempoTranscurridoSegundos * 1000.0f; 

          
            for (int i = 0; i < read; i++)
            {
                bufferDelay.Add(buffer[i + offset]);
            }

          
            if (bufferDelay.Count > buffersize)
            {
                int diferencia = bufferDelay.Count - buffersize;
                bufferDelay.RemoveRange(0, diferencia);
                quantitySamplesDeleted += diferencia;
            }

           
            if(bufferDelay.Count > buffersize)
            {
                int diferencia = bufferDelay.Count - buffersize;
                bufferDelay.RemoveRange(0, diferencia);
                quantitySamplesDeleted += diferencia;
            }

            if (Active)
            {
          
                if (millisecondsElapsed > offsetMiliseconds)
                {
                    for (int i = 0; i < read; i++)
                    {
                        buffer[offset + i] += bufferDelay[quantitySamplesElapsed - quantitySamplesDeleted + i - QuantitySamplesOffset] * Gain;
                    }
                }
            }

            quantitySamplesElapsed += read;
            return read;
        }
    }
}