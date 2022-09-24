//variables for scripting

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karen.Types
{
    public struct kint32
    {
        public int value;
        public string name;
        public static kint32 Zero
        {
            get
            {
                kint32 k =new();
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToInt32((string)value);
        }
    }
    public struct kint64
    {
        public long value;
        public string name;
        public static kint64 Zero
        {
            get
            {
                kint64 k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToInt64((string)value);
        }
    }
    public struct kint16
    {
        public short value;
        public string name;
        public static kint16 Zero
        {
            get
            {
                kint16 k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToInt16((string)value);
        }
    }

    //unsigned
    public struct kuint32
    {
        public uint value;
        public string name;
        public static kuint32 Zero
        {
            get
            {
                kuint32 k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToUInt32((string)value);
        }
    }
    public struct kuint64
    {
        public ulong value;
        public string name;
        public static kuint64 Zero
        {
            get
            {
                kuint64 k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToUInt64((string)value);
        }
    }
    public struct kuint16
    {
        public ushort value;
        public string name;
        public static kuint16 Zero
        {
            get
            {
                kuint16 k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToUInt16((string)value);
        }
    }

    public struct kdouble
    {
        public double value;
        public string name;
        public static kdouble Zero
        {
            get
            {
                kdouble k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToDouble((string)value);
        }
    }
    public struct kdecimal
    {
        public decimal value;
        public string name;
        public static kdecimal Zero
        {
            get
            {
                kdecimal k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = 0;
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = Convert.ToDecimal((string)value);
        }
    }
    public struct kstring
    {
        public string value;
        public string name;
        public static kstring Empry
        {
            get
            {
                kstring k;
                k.name = $"{DateTime.UtcNow.Ticks}";
                k.value = "";
                return k;
            }
        }
        public void SetValue(object value, object name)
        {
            this.name = (string)name;
            this.value = (string)value;
        }
    }
}
