using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Linq;

namespace PMCS.Classes
{
    class InputSource
    {
        private List<FNamespace> listOfNamespaces = new List<FNamespace>();
        string sourcePath = "";
        protected int elementID;
        protected int fileCount;
        Parser p;

        public int ElementID
        {
            get
            {
                return elementID;
            }
            set
            {
                elementID = value;
            }
        }

        public int FileCount
        {
            get
            {
                return fileCount;
            }
            set
            {
                fileCount = value;
            }
        }
        public List<FNamespace> ListOfNamespaces
        {
            get
            {
                return listOfNamespaces;
            }
            set
            {
                listOfNamespaces = value;
            }
        }
        public string SourcePath
        {
            get
            {
                return sourcePath;
            }
        }

        public int GetParentClassID(string className, int indexOfNamespace, int indexOfClass)
        {
            for (int i = 0; i < ListOfNamespaces.Count; i++)
            {
                for (int j = 0; j < ListOfNamespaces[i].NClasses.Count; j++)
                {
                    if (ListOfNamespaces[i].NClasses[j].CName == className)
                    {
                        return ListOfNamespaces[i].NClasses[j].CId;
                    }
                }
            }
            return -1;
        }

        public void ProcessInheritance()
        {
            for (int i = 0; i < ListOfNamespaces.Count; i++)
            {
                for (int j = 0; j < ListOfNamespaces[i].NClasses.Count; j++)
                {
                    if ((ListOfNamespaces[i].NClasses[j].ParentClass != null) && (ListOfNamespaces[i].NClasses[j].ParentClass.Count > 0))
                    {
                        for (int k = 0; k < ListOfNamespaces[i].NClasses[j].ParentClass.Count; k++)
                        {
                            int parentIndex = GetParentClassID(ListOfNamespaces[i].NClasses[j].ParentClass[k], i, j);
                            if (parentIndex != -1)//pe vesi se clasat e sisstemit psh ato skan index e kan -1
                            {
                                FInheritaceDefinition inheritance = new FInheritaceDefinition();
                                inheritance.IId = ElementID;
                                inheritance.IStub = false;
                                inheritance.SubClass = ListOfNamespaces[i].NClasses[j].CId;
                                inheritance.SuperClass = parentIndex;
                                ListOfNamespaces[i].FInheritance.Add(inheritance);
                                ElementID++;
                            }
                        }
                    }
                }
            }
        }

        public int GetParentClassIndex(string className, int indexOfNamespace, int indexOfClass)
        {
            for (int i = 0; i < ListOfNamespaces.Count; i++)
            {
                for (int j = 0; j < ListOfNamespaces[i].NClasses.Count; j++)
                {
                    if (ListOfNamespaces[i].NClasses[j].CName == className)
                    {
                        return j;
                    }
                }
            }
            return -1;
        }

        public int GetAccessesID(int namespaceIndex, int controlClassIndex, int fieldIndex, string accessName)
        {
            if (listOfNamespaces[namespaceIndex].NClasses.Count > controlClassIndex)
            {
                if (listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields != null)
                {
                    for (int i = 0; i < listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields.Count; i++)
                    {
                        if (listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields[i].FName != null)
                        {
                            if (listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields[i].FName.CompareTo(accessName) == 0)
                            {
                                return listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields[i].FID;
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public void ProcessAccess()
        {
            for (int i = 0; i < ListOfNamespaces.Count; i++)
            {
                for (int j = 0; j < ListOfNamespaces[i].NClasses.Count; j++)
                {
                    for (int t = 0; t < ListOfNamespaces[i].NClasses[j].CMethods.Count; t++)
                    {
                        for (int z = 0; z < ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess.Count; z++)
                        {
                            int parentIndex = GetAccessesID(i, j, t, ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess[z].AName);
                            if (parentIndex != -1)
                            {
                                ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess[z].AAccesses = parentIndex;
                                ListOfNamespaces[i].FAccess.Add(ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess[z]);
                            }
                            else
                            {
                                if (ListOfNamespaces[i].NClasses[j].ParentClass.Count > 0)
                                {
                                    for (int p = 0; p < ListOfNamespaces[i].NClasses[j].ParentClass.Count; p++)
                                    {
                                        int parentClass = GetParentClassIndex(ListOfNamespaces[i].NClasses[j].ParentClass[p], ListOfNamespaces[i].NId, ListOfNamespaces[i].NClasses[j].CId);
                                        if (parentClass > -1)
                                        {
                                            parentIndex = GetAccessesID(i, parentClass, t, ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess[z].AName);
                                            if (parentIndex != -1)
                                            {
                                                ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess[z].AAccesses = parentIndex;
                                                if (ControlAccess(ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess[z].AID) == 0)
                                                {
                                                    ListOfNamespaces[i].FAccess.Add(ListOfNamespaces[i].NClasses[j].CMethods[t].MAccess[z]);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        public int ControlAccess(int accessID)
        {//mos me na i qit ka 2 accessa

            for (int i = 0; i < ListOfNamespaces.Count; i++)
            {
                for (int j = 0; j < ListOfNamespaces[i].FAccess.Count; j++)
                {
                    if (accessID == ListOfNamespaces[i].FAccess[j].AID)
                    {
                        return 1;
                    }
                }
            }
            return 0;
        }


        public int GetInvocationIDClass(int namespaceIndex, string className, string methodName)
        {
            for (int i = 0; i < listOfNamespaces[namespaceIndex].NClasses.Count; i++)
            {
                if (listOfNamespaces[namespaceIndex].NClasses[i].CName.CompareTo(className) == 0)
                {
                    for (int m = 0; m < listOfNamespaces[namespaceIndex].NClasses[i].CMethods.Count; m++)
                    {
                        if (listOfNamespaces[namespaceIndex].NClasses[i].CMethods[m].MName != null)
                        {
                            if (listOfNamespaces[namespaceIndex].NClasses[i].CMethods[m].MName.CompareTo(methodName) == 0)
                            {
                                return listOfNamespaces[namespaceIndex].NClasses[i].CMethods[m].MId;
                            }
                        }
                    }
                }
            }
            return -1;
        }
        public int GetInvocationID(int namespaceIndex, int controlClassIndex, string accessName, string methodName)
        {
            for (int i = 0; i < listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields.Count; i++)
            {
                if (listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields[i].FName != null)
                {
                    if (listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields[i].FName.CompareTo(accessName) == 0)
                    {
                        return GetInvocationIDClass(namespaceIndex, listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].cCFields[i].FType, methodName);
                    }
                }
            }
            return -1;
        }

        public int GetInvocationMID(int namespaceIndex,  int controlClassIndex, string methodName)
        {
            if (listOfNamespaces[namespaceIndex].NClasses.Count > controlClassIndex)
            {
                for (int i = 0; i < listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].CMethods.Count; i++)
                {
                    if (listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].CMethods[i].MName != null)
                    {
                        if (listOfNamespaces[namespaceIndex].NClasses[controlClassIndex].CMethods[i].MName.CompareTo(methodName) == 0)
                        {
                            return ListOfNamespaces[namespaceIndex].NClasses[controlClassIndex].CMethods[i].MId;
                        }
                    }
                }
            }
            return -1;
        }

        public string GetMethodSignature(int id)
        {
            for (int i = 0; i < ListOfNamespaces.Count; i++)
            {
                for (int j = 0; j < ListOfNamespaces[i].NClasses.Count; j++)
                {
                    for (int t = 0; t < ListOfNamespaces[i].NClasses[j].CMethods.Count; t++)
                    {
                        if (ListOfNamespaces[i].NClasses[j].CMethods[t].MId == id)
                        {
                            return ListOfNamespaces[i].NClasses[j].CMethods[t].MSignature;
                        }
                    }
                }
            }
            return "";
        }
        public void ProccessInvocation()
        {
            for (int i = 0; i < ListOfNamespaces.Count; i++)
            {
                for (int j = 0; j < ListOfNamespaces[i].NClasses.Count; j++)
                {
                    for (int t = 0; t < ListOfNamespaces[i].NClasses[j].CMethods.Count; t++)
                    {
                        for (int z = 0; z < ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation.Count; z++)
                        {
                            int index;
                            if (ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FParent != "")
                            {
                                index = GetInvocationID(i, j, ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FParent, ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FName);
                                if (index > -1)
                                {
                                    ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FCandidate = index;
                                    ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FName = GetMethodSignature(index);
                                    ListOfNamespaces[i].FInvoc.Add(ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z]);
                                }
                            }
                            else
                            {
                                index = GetInvocationMID(i, j, ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FName);
                                if (index > -1)
                                {
                                    ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FCandidate = index;
                                    ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FName = GetMethodSignature(index);
                                    ListOfNamespaces[i].FInvoc.Add(ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z]);
                                }
                                else
                                {
                                    if (ListOfNamespaces[i].NClasses[j].ParentClass.Count > 0)
                                    {
                                        for (int p = 0; p < ListOfNamespaces[i].NClasses[j].ParentClass.Count; p++)
                                        {
                                            int parentClass = GetParentClassIndex(ListOfNamespaces[i].NClasses[j].ParentClass[p], ListOfNamespaces[i].NId, ListOfNamespaces[i].NClasses[j].CId);
                                            if (parentClass > -1)
                                            {
                                                index = GetInvocationMID(i,  parentClass, ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FName);
                                                if (index != -1)
                                                {
                                                    ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FCandidate = index;
                                                    ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z].FName = GetMethodSignature(index);
                                                    ListOfNamespaces[i].FInvoc.Add(ListOfNamespaces[i].NClasses[j].CMethods[t].MInvocation[z]);
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        
        public void ReadFilesOfProject(string path, Action<string> progressAction)
        {
            sourcePath = path;
            //an array which will get files from the specified source, and it will read only .cs files
            string[] fileEntries = Directory.GetFiles(SourcePath);
            foreach (string fileName in fileEntries)
            {
                if ((fileName.EndsWith(".cs")) && (!Path.GetFileName(fileName).StartsWith("._")))
                {
                    p.ReadFromFile(fileName);
                    progressAction(fileName);
                }
            }

            // Recurse into subdirectories of this directory.
            string[] subdirEntries = Directory.GetDirectories(SourcePath);
            foreach (string subdir in subdirEntries) //lexohen edhe nefolderat
            {
                ReadFilesOfProject(subdir, progressAction);
            }
        }
        public void ReadProject(string path, Action<string> progressAction)
        {
            p = new Parser(this); 
            // e japim inputsourcin si paramter hyres se me ListOfNamspace na po perdorim gjithmon en Parse
            //e inicializojm ktu e jon eforeach te ReadProject se na hup klasa e parsume ma heret
            //sa here qe lexon file te ri aiia jep naspacit si file te ri edhe i kemi namspacat e ri 


            ReadFilesOfProject(path, progressAction);
            ProcessInheritance();
            ProccessInvocation();
            ProcessAccess();
            
        }

        public void ProjectFileCount(string path)
        {
            string[] fileEntries = Directory.GetFiles(path);
            foreach (string fileName in fileEntries)
            {
                if (fileName.EndsWith(".cs") == true)
                {
                    FileCount++;
                }
            }

            // Recurse into subdirectories of this directory.
            string[] subdirEntries = Directory.GetDirectories(path);
            foreach (string subdir in subdirEntries) //lexohen edhe nefolderat
            {
                ProjectFileCount(subdir);
            }
        }
    }
}