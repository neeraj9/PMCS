using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FNamespace
    {
        protected int nId;
        protected string nName;
        protected string parentNamespace;
        protected int nPackagedIn;
        protected bool nStub;
        protected List<FClass> nClasses = new List<FClass>();
        protected List<FInheritaceDefinition> fInheritance = new List<FInheritaceDefinition>();
        protected List<FAccess> fAccess = new List<FAccess>();
        protected List<FInvocation> fInvocation = new List<FInvocation>();

        public int NId
        {
            get
            {
                return nId;
            }
            set
            {
                nId = value;
            }
        }
        public string NName
        {
            get
            {
                return nName;
            }
            set
            {
                nName = value;
            }
        }
        public string ParnetNamespace
        {
            get
            {
                return parentNamespace;
            }
            set
            {
                parentNamespace = value;
            }
        }
        public int NPackagedIn
        {
            get
            {
                return nPackagedIn;
            }
            set
            {
                nPackagedIn = value;
            }
        }
        public bool NStub
        {
            get
            {
                return nStub;
            }
            set
            {
                nStub = value;
            }
        }
        public List<FClass> NClasses
        {
            get
            {
                return nClasses;
            }
            set
            {
                nClasses = value;
            }
        }
        public List<FInheritaceDefinition> FInheritance
        {

            get
            {
                return fInheritance;
            }
            set
            {
                fInheritance = value;
            }
        }
        public List<FAccess> FAccess
        {

            get
            {
                return fAccess;
            }
            set
            {
                fAccess = value;
            }
        }

        public List<FInvocation> FInvoc
        {

            get
            {
                return fInvocation;
            }
            set
            {
                fInvocation = value;
            }
        }


        public int IndexOf(List<FNamespace> listOfNamespace, FNamespace fNamespace)
        {
            for (int i = 0; i < listOfNamespace.Count; i++)
            {
                if (listOfNamespace[i].nName == fNamespace.NName)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}
