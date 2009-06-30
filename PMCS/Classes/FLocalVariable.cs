using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FLocalVariable
    {
        protected int lId;
        protected string lName;
        protected int lBelongsTo;
        protected bool lStub;

        public int LId
        {
            get
            {
                return lId;
            }
            set
            {
                lId = value;
            }
        }
        public string LName
        {
            get
            {
                return lName;
            }
            set
            {
                lName = value;
            }
        }
        public int LBelongsTo
        {
            get
            {
                return lBelongsTo;
            }
            set
            {
                lBelongsTo = value;
            }
        }
        public bool LStub
        {
            get
            {
                return lStub;
            }
            set
            {
                lStub = value;
            }
        }

    }
}
