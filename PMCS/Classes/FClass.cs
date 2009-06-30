using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FClass
    {
        protected int cId;
        protected string cName;
        //protected string parentClass;
        List<string> parentClass = new List<string>();
        //protected int cBelongsTo;
        int cBelongsTo;
        protected bool cIsAbstract;
        protected bool cIsInterface;
        protected bool cStub;
        protected List<FMethod> cMethods = new List<FMethod>();
        protected List<FAttribute> cAttributes = new List<FAttribute>();
        protected List<FField> cFields = new List<FField>();
        protected List<FLibrary> nLibrary = new List<FLibrary>();
        protected bool cIsEnum;
        protected bool cIsStruct;


        public List<FLibrary> NLibrary
        {
            get
            {
                return nLibrary;
            }
            set
            {
                nLibrary = value;
            }
        }

        public int CId
        {
            get
            {
                return cId;
            }
            set
            {
                cId = value;
            }
        }
        public string CName
        {
            get
            {
                return cName;
            }
            set
            {
                cName = value;
            }
        }

        public List<string> ParentClass
        {
            get
            {
                return parentClass;
            }
            set
            {
                parentClass = value;
            }
        }
        public int CBelongsTo
        {
            get
            {
                return cBelongsTo;
            }
            set
            {
                cBelongsTo = value;
            }
        }
        public bool CIsAbstract
        {
            get
            {
                return cIsAbstract;
            }
            set
            {
                cIsAbstract = value;
            }
        }
        public bool CIsInterface
        {
            get
            {
                return cIsInterface;
            }
            set
            {
                cIsInterface = value;
            }
        }
        public bool CIsEnum
        {
            get
            {
                return cIsEnum;
            }
            set
            {
                cIsEnum = value;
            }
        }
        public bool CIsStruct
        {
            get
            {
                return cIsStruct;
            }
            set
            {
                cIsStruct = value;
            }
        }
        public bool CStub
        {
            get
            {
                return cStub;
            }
            set
            {
                cStub = value;
            }
        }
        public List<FMethod> CMethods
        {
            get
            {
                return cMethods;
            }
            set
            {
                cMethods = value;
            }
        }
        public List<FAttribute> CAttributes
        {
            get
            {
                return cAttributes;
            }
            set
            {
                cAttributes = value;
            }
        }
        public List<FField> cCFields
        {
            get
            {
                return cFields;
            }
            set
            {
                cFields = value;
            }
        }


        public int IndexOf(List<FNamespace> listOfNamespace, int indexOfNamespace, FClass fClass)
        {
            for (int i = 0; i < listOfNamespace[indexOfNamespace].NClasses.Count; i++)
            {
                if (listOfNamespace[indexOfNamespace].NClasses[i].cName == fClass.cName)
                {
                    return i;
                }
            }
            return -1;
        }

    }
}
