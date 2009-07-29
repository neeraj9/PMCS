using System;
using System.Text;

namespace PMCS.Classes
{
    class CachingStringBuilder
    {
        private readonly StringBuilder builder;
        private String toString;

        public CachingStringBuilder()
        {
            builder = new StringBuilder(200000);
        }

        public int Length
        {
            get { return builder.Length; }
        }

        public void Remove(int startIndex, int length)
        {
            builder.Remove(startIndex, length);
            toString = null;
        }

        public void Append(string value)
        {
            builder.Append(value);
            toString = null;
        }

        public void Append(char value)
        {
            builder.Append(value);
            toString = null;
        }

        public void Replace(char oldChar, char newChar, int startIndex, int count)
        {
            builder.Replace(oldChar, newChar, startIndex, count);
            toString = null;
        }

        public override string ToString()
        {
            if (toString == null)
                toString = builder.ToString();
            return toString;
        }
    }
}
