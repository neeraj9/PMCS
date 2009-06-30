using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FInheritaceDefinition
    {
        protected int iId;
        protected int superClass;
        protected int subClass;
        protected bool iStub;

        public int IId
        {
            get
            {
                return iId;
            }
            set
            {
                iId = value;
            }
        }
        public int SuperClass
        {
            get
            {
                return superClass;
            }
            set
            {
                superClass = value;
            }
        }
        public int SubClass
        {
            get
            {
                return subClass;
            }
            set
            {
                subClass = value;
            }
        }
        public bool IStub
        {
            get
            {
                return iStub;
            }
            set
            {
                iStub = value;
            }
        }
    }
}

