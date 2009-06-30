using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FField
    {
        protected int fID;
        protected string fType;
        protected string fName;
        protected string fAccessControlQualifier;
        protected int varType;

        public string FAccessControlQualifier
        {
            get
            {
                return fAccessControlQualifier;

            }
            set
            {
                fAccessControlQualifier = value;
            }
        }
        public int VarType
        {
            get
            {
                return varType;
            }
            set
            {
                varType = value;
            }
        }

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
        public string FType
        {
            get
            {
                return fType;
            }
            set
            {
                fType = value;
            }
        }
    }
}
