using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class FLibrary
    {
        protected int lId;
        protected string lName;

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
        public bool Contains(List<FLibrary> listOfLibraries, FLibrary library)
        {
            for (int i = 0; i < listOfLibraries.Count; i++)
            {
                if (listOfLibraries[i].LName == library.LName)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
