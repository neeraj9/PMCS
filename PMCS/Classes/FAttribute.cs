using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FAttribute
    {
        protected int aId;
        protected string aName;
        protected int aBelongsTo;
        protected string aAccessControlQualifier;
        protected bool aHasClassScope;
        protected bool aStub;
        protected string aType;
        protected List<FFormalParameter> aParameter;
        public int AId
        {
            get
            {
                return aId;
            }
            set
            {
                aId = value;
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
        public string AType
        {
            get
            {
                return aType;
            }
            set
            {
                aType = value;
            }
        }
        public string AAccessControlQualifier
        {
            get
            {
                return aAccessControlQualifier;
            }
            set
            {
                aAccessControlQualifier = value;
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
        public bool AHasClassScope
        {
            get
            {
                return aHasClassScope;
            }
            set
            {
                aHasClassScope = value;
            }
        }
        public bool AStub
        {
            get
            {
                return aStub;
            }
            set
            {
                aStub = value;
            }
        }
        public List<FFormalParameter> AParameter
        {
            get
            {
                return aParameter;
            }
            set
            {
                aParameter = value;
            }
        }
    }
}