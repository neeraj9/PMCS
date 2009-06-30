using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FMethod
    {
        private int mId;
        private string mName;
        private string mAccessControlQualifier;
        private int mBelongsTo;
        private bool mHasClassScope;
        private string mKind;
        private string mSignature;
        private bool mStub;
        private List<FAccess> mAccess = new List<FAccess>();
        private List<FFormalParameter> mParameter = new List<FFormalParameter>();
        private List<FLocalVariable> mVariable = new List<FLocalVariable>();
        private List<FInvocation> mInvocation = new List<FInvocation>();

        private int loc;

        public int Loc
        {
            get
            {
                return loc;
            }
            set
            {
                loc = value;
            }
        }
        // private List<
        public int MId
        {
            get
            {
                return mId;
            }
            set
            {
                mId = value;
            }
        }
        public string MName
        {
            get
            {
                return mName;
            }
            set
            {
                mName = value;
            }
        }
        public string mMccessControlQualifier
        {
            get
            {
                return mAccessControlQualifier;
            }
            set
            {
                mAccessControlQualifier = value;
            }
        }
        public int MBelongsTo
        {
            get
            {
                return mBelongsTo;
            }
            set
            {
                mBelongsTo = value;
            }
        }
        public bool MHasClassScope
        {
            get
            {
                return mHasClassScope;
            }
            set
            {
                mHasClassScope = value;
            }
        }
        public string MKind
        {
            get
            {
                return mKind;
            }
            set
            {
                mKind = value;
            }
        }
        public string MSignature
        {
            get
            {
                return mSignature;
            }
            set
            {
                mSignature = value;
            }
        }
        public bool MStub
        {
            get
            {
                return mStub;
            }
            set
            {
                mStub = value;
            }
        }
        public List<FFormalParameter> MParameter
        {
            get
            {
                return mParameter;
            }
            set
            {
                mParameter = value;
            }
        }
        public List<FLocalVariable> MVariable
        {
            get
            {
                return mVariable;
            }
            set
            {
                mVariable = value;
            }
        }

        public List<FAccess> MAccess
        {
            get
            {
                return mAccess;
            }
            set
            {
                mAccess = value;
            }
        }
        public List<FInvocation> MInvocation
        {
            get
            {
                return mInvocation;
            }
            set
            {
                mInvocation = value;
            }
        }
    }
}
