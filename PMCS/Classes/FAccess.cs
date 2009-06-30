using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FAccess
    {
        private int aID;
        private string aName;
        private int aBelongsTo;
        private int aAccesses;
        private int aAccessBy;
        
        public int AAccessBy
        {
            get
            {
                return aAccessBy;
            }
            set
            {
                aAccessBy = value;
            }
        }
        public int AID
        {
            get
            {
                return aID;
            }
            set
            {
                aID = value;
            }
        }

        public string AName
        {
            get
            {
                return aName;
            }
            set
            {
                aName = value;
            }
        }

        public int ABelongsTo
        {
            get
            {
                return aBelongsTo;
            }
            set
            {
                aBelongsTo = value;
            }
        }

        public int AAccesses
        {
            get
            {
                return aAccesses;
            }
            set
            {
                aAccesses = value;
            }
        }

    }
}
