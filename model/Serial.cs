using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LKCamelot.model
{

    public struct Serial : IComparable, IComparable<Serial>
    {
        private int m_Serial;

        private static Serial m_LastMobile = 0x10000;
        private static Serial m_LastItem = 0x40000000;
        private static readonly object AllocationLock = new object();

        public static Serial LastMobile { get { return m_LastMobile; } }
        public static Serial LastItem { get { return m_LastItem; } }

        public static readonly Serial MinusOne = new Serial(-1);
        public static readonly Serial Zero = new Serial(0);

        public static Serial NewMobile
        {
            get
            {
                lock (AllocationLock)
                {
                    var registry = GetRegistry();
                    while (true)
                    {
                        if (m_LastMobile.Value >= 0x3FFFFFFF)
                            throw new InvalidOperationException("Mobile serial space is exhausted.");
                        Serial candidate = m_LastMobile.Value + 1;
                        if (registry.TryAdd(candidate, candidate))
                        {
                            m_LastMobile = candidate;
                            return candidate;
                        }
                        m_LastMobile = candidate;
                    }
                }
            }
        }
        public static Serial NewItem
        {
            get
            {
                lock (AllocationLock)
                {
                    var registry = GetRegistry();
                    while (true)
                    {
                        if (m_LastItem.Value >= int.MaxValue)
                            throw new InvalidOperationException("Item serial space is exhausted.");
                        Serial candidate = m_LastItem.Value + 1;
                        if (registry.TryAdd(candidate, candidate))
                        {
                            m_LastItem = candidate;
                            return candidate;
                        }
                        m_LastItem = candidate;
                    }
                }
            }
        }

        // Explicit serials from saved data must also be reserved before new IDs are issued.
        public static void Reserve(Serial serial)
        {
            if (!serial.IsValid)
                return;

            lock (AllocationLock)
            {
                GetRegistry().TryAdd(serial, serial);
                if (serial.IsMobile && serial.Value > m_LastMobile.Value)
                    m_LastMobile = serial;
                else if (serial.IsItem && serial.Value > m_LastItem.Value)
                    m_LastItem = serial;
            }
        }

        private static ConcurrentDictionary<Serial, Serial> GetRegistry()
        {
            if (World.m_MObjects == null)
                World.m_MObjects = new ConcurrentDictionary<Serial, Serial>();
            return World.m_MObjects;
        }
        private Serial(int serial)
        {
            m_Serial = serial;
        }

        public int Value
        {
            get
            {
                return m_Serial;
            }
        }

        public bool IsMobile
        {
            get
            {
                return (m_Serial > 0 && m_Serial < 0x40000000);
            }
        }

        public bool IsItem
        {
            get
            {
                return (m_Serial >= 0x40000000 && m_Serial <= 0x7FFFFFFF);
            }
        }

        public bool IsValid
        {
            get
            {
                return (m_Serial > 0);
            }
        }

        public override int GetHashCode()
        {
            return m_Serial;
        }

        public int CompareTo(Serial other)
        {
            return m_Serial.CompareTo(other.m_Serial);
        }

        public int CompareTo(object other)
        {
            if (other is Serial)
                return this.CompareTo((Serial)other);
            else if (other == null)
                return -1;

            throw new ArgumentException();
        }

        public override bool Equals(object o)
        {
            if (o == null || !(o is Serial)) return false;

            return ((Serial)o).m_Serial == m_Serial;
        }

        public static bool operator ==(Serial l, Serial r)
        {
            return l.m_Serial == r.m_Serial;
        }

        public static bool operator !=(Serial l, Serial r)
        {
            return l.m_Serial != r.m_Serial;
        }

        public static bool operator >(Serial l, Serial r)
        {
            return l.m_Serial > r.m_Serial;
        }

        public static bool operator <(Serial l, Serial r)
        {
            return l.m_Serial < r.m_Serial;
        }

        public static bool operator >=(Serial l, Serial r)
        {
            return l.m_Serial >= r.m_Serial;
        }

        public static bool operator <=(Serial l, Serial r)
        {
            return l.m_Serial <= r.m_Serial;
        }

        /*public static Serial operator ++ ( Serial l )
        {
            return new Serial( l + 1 );
        }*/

        public override string ToString()
        {
            return String.Format("0x{0:X8}", m_Serial);
        }

        public static implicit operator int(Serial a)
        {
            return a.m_Serial;
        }

        public static implicit operator Serial(int a)
        {
            return new Serial(a);
        }
    }
}
