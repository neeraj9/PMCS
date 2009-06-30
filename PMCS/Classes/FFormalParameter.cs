using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FFormalParameter
    {
        protected int fId;
        protected string fName;
        protected int fBelongsTo;
        protected bool fStub;
        protected int position;

        public int FId
        {
            get
            {
                return fId;
            }
            set
            {
                fId = value;
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
        public int FBelongsTo
        {
            get
            {
                return fBelongsTo;
            }
            set
            {
                fBelongsTo = value;
            }
        }
        public bool FStub
        {
            get
            {
                return fStub;
            }
            set
            {
                fStub = value;
            }
        }
        public int Position
        {
            get
            {
                return position;
            }
            set
            {
                position = value;
            }
        }

    }
}
