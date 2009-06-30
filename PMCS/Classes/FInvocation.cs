using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{

    class FInvocation
    {
        private int fID;
        private string fName;
        private string fParent;
        private int fInvokedBy;//id of method that make invocation
        private int fCandidate;//id of method that is invoced

        public int FID
        {
            get
            {
                return fID;
            }
            set
            {
                fID = value;
            }
        }

        public string FName
        {
            get
            {
                return fName;
            }
            set
            {
                fName = value;
            }
        }

        public string FParent
        {
            get
            {
                return fParent;
            }
            set
            {
                fParent = value;
            }
        }

        public int FInvokedBy
        {
            get
            {
                return fInvokedBy;
            }
            set
            {
                fInvokedBy = value;
            }
        }

        public int FCandidate
        {
            get
            {
                return fCandidate;
            }
            set
            {
                fCandidate = value;
            }
        }
    }
}