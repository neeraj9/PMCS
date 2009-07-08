using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace PMCS.Classes
{
    class Parser
    {
        private List<FLibrary> listOfLibrary = new List<FLibrary>();
        private List<FClass> listOfClasses = new List<FClass>();
        private StreamReader SR;
        public InputSource inputSource = new InputSource();
        public StringBuilder parseLine = new StringBuilder();
        public List<int> statusOfNamepsace = new List<int>();
        public List<int> statusOfClass = new List<int>();
        public int startIndex;
        public int lastIndex;
        //with this function we are calculating the min value of one array
        public int Min(params int[] val)
        {
            int min = val[0];
            for (int i = 1; i < val.Length; i++)
            {
                if (val[i] > -1)
                {
                    if ((min == -1) || (min > val[i]))
                    {
                        min = val[i];
                    }
                }
            }
            if (min == -1)
                return 0;
            else
                return min;
        }
        public Parser()
        {
        }
        public Parser(InputSource source)
        {
            inputSource = source;
            inputSource.ElementID = 2;
        }
        public void ParseNamespace()
        {
            FNamespace fmNamespace = new FNamespace();
            lastIndex = parseLine.ToString().IndexOf("{");
            fmNamespace.NId = inputSource.ElementID;
            fmNamespace.NName = parseLine.ToString().Substring(startIndex + 10, lastIndex - (startIndex + 10)).Trim();
            String[] namespaces = parseLine.ToString().Substring(startIndex + 10, lastIndex - (startIndex + 10)).Trim().Split('.');
            if (namespaces.Length > 1)
            {
                fmNamespace.ParnetNamespace = namespaces[namespaces.Length - 2];
            }
            if (statusOfNamepsace.Count > 0)
            {
                fmNamespace.ParnetNamespace = inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NName;
                fmNamespace.NName = inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NName + "." + parseLine.ToString().Substring(startIndex + 10, lastIndex - (startIndex + 10)).Trim();
        
            }

            int index = fmNamespace.IndexOf(inputSource.ListOfNamespaces, fmNamespace);
            if (index == -1)
            {
                inputSource.ListOfNamespaces.Add(fmNamespace);
                statusOfNamepsace.Add(inputSource.ListOfNamespaces.Count - 1);
                inputSource.ElementID++;
            }
            else
            {
                statusOfNamepsace.Add(index);
            }
            parseLine.Remove(0, lastIndex + 1);
        }
        public void ParseUsing()
        {
            lastIndex = parseLine.ToString().IndexOf(";");
            FLibrary library = new FLibrary();
            library.LId = inputSource.ElementID;
            library.LName = parseLine.ToString().Substring(startIndex + 6, lastIndex - (startIndex + 6)).Trim();
            if (!library.Contains(listOfLibrary, library))
            {
                listOfLibrary.Add(library);
                inputSource.ElementID++;
            }
            parseLine.Remove(0, lastIndex + 1);
        }
        public void ParseClass()
        {
            FClass fclass = new FClass();
            lastIndex = parseLine.ToString().IndexOf("{");
            fclass.CId = inputSource.ElementID;

            if (statusOfClass.Count > 0)
            {

                fclass.ParentClass.Add(inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses[statusOfClass[statusOfClass.Count - 1]].CName);

            }

            int inherit = parseLine.ToString().IndexOf(":");
            List<string> name = new List<string>();
            if ((inherit > -1) && (inherit < lastIndex))
            {
                string[] parent = parseLine.ToString().Substring(inherit + 1, lastIndex - (inherit + 1)).Split(',');
                if (parent.Length > 0)
                {
                    for (int i = 0; i < parent.Length; i++)
                    {
                        fclass.ParentClass.Add(parent[i].Trim());
                    }
                }
                fclass.CName = parseLine.ToString().Substring(startIndex + 5, inherit - (startIndex + 5)).Trim();
            }
            else
            {
                fclass.CName = parseLine.ToString().Substring(startIndex + 5, lastIndex - (startIndex + 5)).Trim();
            }

            if ((parseLine.ToString().IndexOf("abstarct") > -1) && (parseLine.ToString().IndexOf("abstract") < lastIndex))
            {
                fclass.CIsAbstract = true;
            }
            fclass.CIsInterface = false;
            fclass.CIsEnum = false;
            fclass.CIsStruct = false;

            fclass.CStub = false;

            fclass.CBelongsTo = inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NId;


            int index = fclass.IndexOf(inputSource.ListOfNamespaces, statusOfNamepsace[statusOfNamepsace.Count - 1], fclass);
            if (index == -1)
            {
                inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses.Add(fclass);
                statusOfClass.Add(inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses.Count - 1);
                inputSource.ElementID++;
            }
            else
            {
                statusOfClass.Add(index);
            }
            parseLine.Remove(0, lastIndex + 1);
        }
        public void ParseStruct()
        {
            FClass fclass = new FClass();
            lastIndex = parseLine.ToString().IndexOf("{");
            fclass.CId = inputSource.ElementID;

            if (statusOfClass.Count > 0)
            {

                fclass.ParentClass.Add(inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses[statusOfClass[statusOfClass.Count - 1]].CName);

            }
            fclass.CName = parseLine.ToString().Substring(startIndex + 7, lastIndex - (startIndex + 7)).Trim();

            if ((parseLine.ToString().IndexOf("abstarct") > -1) && (parseLine.ToString().IndexOf("abstract") < lastIndex))
            {
                fclass.CIsAbstract = true;
            }
            fclass.CIsInterface = false;
            fclass.CIsEnum = false;
            fclass.CIsStruct = true;

            fclass.CStub = false;
            fclass.CBelongsTo = inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NId;

            parseLine.Remove(0, lastIndex + 1);
            inputSource.ElementID++;
            inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses.Add(fclass);
            statusOfClass.Add(inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses.Count - 1);
        }
        public void ParseDelegate()
        {
            int lastIndex = parseLine.ToString().IndexOf(";");
            parseLine.Remove(0, lastIndex + 1);
        }
        public void ParseEnum()
        {
            int enumStatus = 1;
            lastIndex = parseLine.ToString().IndexOf("{") + 1;
            while (enumStatus > 0)
            {
                if (parseLine.ToString()[lastIndex] == '{')
                {
                    enumStatus++;
                }
                else
                {
                    if (parseLine.ToString()[lastIndex] == '}')
                    {
                        enumStatus--;
                    }
                }
                lastIndex++;
            }
            parseLine.Remove(0, lastIndex + 1);
        }
        public void ParseInterface()
        {
            FClass fclass = new FClass();
            int last = parseLine.ToString().IndexOf("{");
            int start = parseLine.ToString().IndexOf("interface");
            fclass.CName = parseLine.ToString().Substring(start + 10, last - start - 10).Trim();
            fclass.CIsInterface = true;
            fclass.CId = inputSource.ElementID;
            parseLine.Remove(0, last + 1);
            int index = fclass.IndexOf(inputSource.ListOfNamespaces, statusOfNamepsace[statusOfNamepsace.Count - 1], fclass);//kontrollon klasa a eshte ne listen e namspacave e rujtun ma heret dhe e mer indexin e asaj klase
            //[para se me e fut kalsen po vesim a eziston edhe nese po po ja marrim indexin poziten e klases edhe qka po parsojm po vendosim ne ate klase
            if (index == -1)
            {
                inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses.Add(fclass);
                statusOfClass.Add(inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses.Count - 1);
                inputSource.ElementID++;
            }
            else
            {
                statusOfClass.Add(index);
            }
            ParseInterface pI = new ParseInterface(this);
            pI.ProcessInterfaceLine();
        }
        public void ParseField(int index)
        {
            List<string> name = new List<string>();
            string type = "";
            string accessControlQualifier = "";
            string f = parseLine.ToString().Substring(0, index).Trim();
            string temp = "";
            int i = 0;
            while (i < f.Length)
            {
                if ((f[i] != '\r') && (f[i] != '\n') && (f[i] != '\t'))
                {
                    temp += f[i];
                }
                i++;
            }
            //po punojm me temp e jo f se ne deklarim te fieldsave munet me pas tab enter edhe vetem per me e kontrollu kete sen
            int n = temp.IndexOf("new") ;
            if (n> -1)
            {
                temp = temp.Substring(0, n);
            }

            String[] fields = temp.Split(',');
            String[] attribut;
            String[] field;

            

            for (i = 0; i < fields.Length; i++)
            {
                attribut = fields[i].Split('=');
                field = attribut[0].Split(' ');
                if (i == 0)
                {
                    for (int j = field.Length -1; j > -1; j--)
                    {
                        if ((field[j] != "") && (field[j] != "\r\n") && (field[j] != "\n") && (field[j] != "\t"))
                        {
                            if ((name.Count == 0) || (field[j].Contains(",") == true))
                            {
                                name.Add(field[j]);
                            }
                            else
                            {
                                if (type == "")
                                {
                                    type = field[j];
                                }
                                else
                                {
                                    if (accessControlQualifier == "")
                                    {
                                        if ((field[j] == "protected") || (field[j] == "public") || (field[j] == "private") || (field[j] == "internal"))
                                        {
                                            accessControlQualifier = field[j];
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int j = 0; j < field.Length; j++)
                    {
                        if ((field[j] != "") && (field[j] != "\r\n") && (field[j] != "\n") && (field[j] != "\t"))
                        {
                            name.Add(field[j]);
                        }
                    }
                }
            }          
            for (int j = 0; j < name.Count; j++)
            {
                FField fField = new FField();
                fField.FID = inputSource.ElementID;
                fField.FAccessControlQualifier = "";
                fField.FName = name[j];
                fField.FType = type;
                fField.FAccessControlQualifier = accessControlQualifier;
                fField.VarType = 1;
                inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses[statusOfClass[statusOfClass.Count - 1]].cCFields.Add(fField);
                inputSource.ElementID++;
            }
            
            parseLine.Remove(0, startIndex + 1);



    }
        public void lastIndexOfVoid()
        {
            int voidStatus = 1;
            int i, j;
            lastIndex = parseLine.ToString().IndexOf("{");
            parseLine.Remove(0, lastIndex + 1);
            while (voidStatus > 0)
            {
                i = parseLine.ToString().IndexOf("{");
                j = parseLine.ToString().IndexOf("}");
                int min = Min(i, j);
                if (min == i)
                {
                    voidStatus++;
                    parseLine.Remove(0, i + 1);
                }
                else
                {
                    if (min == j)
                    {
                        voidStatus--;
                        parseLine.Remove(0, j + 1);
                    }
                }
            }
        }
        public void ParseProperty()
        {
            FAttribute fAttribute = new FAttribute();
            String[] attribute = parseLine.ToString().Substring(0, startIndex).Trim().Split(' ');
            fAttribute.AId = inputSource.ElementID;
            fAttribute.ABelongsTo = inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses[statusOfClass[statusOfClass.Count - 1]].CId;
            fAttribute.AHasClassScope = false;
            fAttribute.AStub = false;

            for (int i = attribute.Length - 1; i > 0; i--)
            {
                if ((attribute[i] != "") && (attribute[i] != "\r\n") && (attribute[i] != "\n") && (attribute[i] != "\t"))
                {
                    if ((fAttribute.AName == null) || (fAttribute.AName == ""))
                    {
                        fAttribute.AName = attribute[i];
                    }
                    else
                    {
                        if ((fAttribute.AType == null) || (fAttribute.AType == ""))
                        {
                            fAttribute.AType = attribute[i];
                        }
                        else
                        {
                            if ((attribute[i] == "protected") || (attribute[i] == "public") || (attribute[i] == "private") || (attribute[i] == "internal") || (attribute[i] == "abstract"))
                            {
                                fAttribute.AAccessControlQualifier = attribute[i];
                            }
                        }
                    }
                }
            }

            inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses[statusOfClass[statusOfClass.Count - 1]].CAttributes.Add(fAttribute);
            inputSource.ElementID++;
            //parseLine.Remove(0, lastIndexOfVoid());
            lastIndexOfVoid();

        }
        public void CheckMethod(int index)
        {
            int voidStatus = 1;
            StringBuilder temp = new StringBuilder();
            temp.Append(parseLine.ToString().Substring(0, index));
            parseLine.Remove(0, index);
            int t = parseLine.ToString().IndexOf("this");
            int b = parseLine.ToString().IndexOf("base");
            int i = parseLine.ToString().IndexOf("(");
            int j; //parseLine.ToString().IndexOf(")");
            int min = Min(t, b);
            if ((min > 0) && (min < i))
            {
                parseLine.Remove(0, i + 1);
                while (voidStatus > 0)
                {
                    i = parseLine.ToString().IndexOf("(");
                    j = parseLine.ToString().IndexOf(")");
                    if (i > -1 && i < j)
                    {
                        voidStatus++;
                        //temp.Append(parseLine.ToString().Substring(0, i + 1));
                        parseLine.Remove(0, i + 1);
                    }
                    else
                    {
                        if (j > -1)
                        {
                            voidStatus--;
                            //temp.Append(parseLine.ToString().Substring(0, j + 1));
                            parseLine.Remove(0, j + 1);
                        }
                    }
                }
            }
            else
            {
                parseLine.Remove(0, parseLine.ToString().IndexOf("{"));
            }
            temp.Append(parseLine.ToString());
            parseLine.Remove(0, parseLine.Length);
            parseLine.Append(temp.ToString());           
        }
        public int LOC()
        {
            int voidStatus = 1;
            StringBuilder temp = new StringBuilder();
            temp.Append(parseLine.ToString().Substring(0, parseLine.ToString().IndexOf("{") + 1));
            int a = parseLine.ToString().IndexOf("{");
            parseLine.Remove(0, parseLine.ToString().IndexOf("{") + 1);

            while (voidStatus > 0)
            {
                int i = parseLine.ToString().IndexOf("{");
                int j = parseLine.ToString().IndexOf("}");
                if (i > -1 && i < j)
                {
                    voidStatus++;
                    a += i;
                    temp.Append(parseLine.ToString().Substring(0, i + 1));
                    parseLine.Remove(0, i + 1);
                }
                else
                {
                    if (j > -1)
                    {
                        voidStatus--;
                        a += j;
                        temp.Append(parseLine.ToString().Substring(0, j + 1));
                        parseLine.Remove(0, j + 1);
                    }
                }
            }
            String[] x = temp.ToString().Trim().Split('\n');
            temp.Append(parseLine.ToString());
            parseLine.Remove(0, parseLine.Length);
            parseLine.Append(temp.ToString());
            return x.Length;
        }
        public void ParseMethod(int index, int startIndex, bool isAbstratc)
        {

            int controlIndex = parseLine.ToString().IndexOf(":");
            if ((controlIndex > -1) && (controlIndex < startIndex))
            {
                CheckMethod(controlIndex);
            }
            FMethod fMethod = new FMethod();

            lastIndex = parseLine.ToString().IndexOf("(");
            String[] method = parseLine.ToString().Substring(0, lastIndex).Trim().Split(' ');

            fMethod.MId = inputSource.ElementID;
            fMethod.MBelongsTo = inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses[statusOfClass[statusOfClass.Count - 1]].CId;

            fMethod.MHasClassScope = false;
            fMethod.MKind = "";//abstrakt
            //fMethod.MParameter;
            fMethod.MStub = false;
            fMethod.Loc = LOC();
            //fMethod.MVariable
            for (int i = method.Length - 1; i > -1; i--)
            {
                if ((method[i] != "") && (method[i] != "\r\n") && (method[i] != "\n") && (method[i] != "\t"))
                {
                    if ((fMethod.MName == null) || (fMethod.MName == ""))
                    {
                        fMethod.MName = method[i];
                    }
                    else
                    {
                        //if ((fMethod.mMccessControlQualifier == null) || (fMethod.mMccessControlQualifier == ""))
                        //{
                        if ((method[i] == "protected") || (method[i] == "public") || (method[i] == "private") || (method[i] == "internal") || (method[i] == "abstract"))
                        {
                            fMethod.mMccessControlQualifier = method[i];
                        }

                        //}
                    }
                }
            }

            fMethod.MSignature = fMethod.MName + parseLine.ToString().Substring(lastIndex, index + 1 - lastIndex).Trim();
            inputSource.ListOfNamespaces[statusOfNamepsace[statusOfNamepsace.Count - 1]].NClasses[statusOfClass[statusOfClass.Count - 1]].CMethods.Add(fMethod);
            inputSource.ElementID++;


            if (isAbstratc == false)
            {
                parseLine.Remove(0, parseLine.ToString().IndexOf("{") + 1);
                ParseMethod parseMethod = new ParseMethod(this);
                //si this kjo mer klasen 
                parseMethod.ParseMehodLine();
            }
            else
            {
                parseLine.Remove(0, parseLine.ToString().IndexOf(";") + 1);
            }
        }
        public int indexOfClassKeyword(int index, string keyword)
        {
            index = indexOfKeyword(index, keyword);
            if (index != -1)
            {
                // check that class is not used in a generics qualifier
                int i = index;
                do
                {
                    i -= 1;
                } while ((i >= 0) && (parseLine.ToString()[i] == ' '));
                if (parseLine.ToString()[i] == ':')
                    return -1;
            }
            return index;
        }
        public int indexOfKeyword(int index, string keyword)
        {
            char c;
          
            if (index > -1)
            {
                if (index > 0)
                {
                    c = parseLine.ToString()[index - 1];
                    if ((c != ' ') && (c != '\n') && (c != '\t') && (c != '\r') && (c != '}') && (c != '{') && (c != '(') && (c != ')'))
                    {
                        return -1;
                    }
                }
                c = parseLine.ToString()[index + keyword.Length];
                if ((c != ' ') && (c != '\n') && (c != '\t') && (c != '\r') && (c != '}') && (c != '{') && (c != '(') && (c != ')'))
                {
                    return -1;
                }
            }
            return index;
        }
        public void ParseAttribut()
        {
            //lastIndex = parseLine.ToString().IndexOf("]");
            //parseLine.Remove(0, lastIndex + 1);

            int enumStatus = 1;
            lastIndex = parseLine.ToString().IndexOf("[") + 1;
            while (enumStatus > 0)
            {
                if (parseLine.ToString()[lastIndex] == '[')
                {
                    enumStatus++;
                }
                else
                {
                    if (parseLine.ToString()[lastIndex] == ']')
                    {
                        enumStatus--;
                    }
                }
                lastIndex++;
            }
            parseLine.Remove(0, lastIndex + 1);
        }
        public int checkAttribut(int index)
        {
            if (index != -1)
            {
                string t = parseLine.ToString().Substring(0, index);
                for (int i = 0; i < t.Length; i++)
                {
                    if ((t[i] != '\n') && (t[i] != '\t') && (t[i] != '\r') && (t[i] != ' '))
                    {
                        return -1;
                    }
                }   
            }
            return index;
        }
        public void ProcessLine()
        {
            while (parseLine.ToString().Trim() != "")
            {
                int indexOfUsing = indexOfKeyword(parseLine.ToString().IndexOf("using"), "using");
                int indexOfNamespace = indexOfKeyword(parseLine.ToString().IndexOf("namespace"), "namespace");
                int indexOfClass = indexOfClassKeyword(parseLine.ToString().IndexOf("class"), "class");
                int indexOfInterface = indexOfKeyword(parseLine.ToString().IndexOf("interface"), "interface");
                int indexOfStuct = indexOfKeyword(parseLine.ToString().IndexOf("struct"), "struct");
                int indexOfEnum = indexOfKeyword(parseLine.ToString().IndexOf("enum"), "enum");
                int indexOfDelegate = indexOfKeyword(parseLine.ToString().IndexOf("delegate"), "delegate");
                int indexOfAttribut = parseLine.ToString().IndexOf("[");
                int indexOfParameter = parseLine.ToString().IndexOf("(");
                if (indexOfAttribut > indexOfParameter)
                {
                    indexOfAttribut = -1;
                }

                if (indexOfDelegate > indexOfParameter)
                {
                    indexOfDelegate = -1;
                }
                indexOfAttribut = checkAttribut(indexOfAttribut);

                int indexOfVoid = parseLine.ToString().IndexOf("{");
                int indexOfField = parseLine.ToString().IndexOf(";");
                int indexOfClose = parseLine.ToString().IndexOf("}");
                startIndex = Min(indexOfUsing, indexOfNamespace, indexOfClass, indexOfInterface, indexOfStuct,
                                 indexOfEnum, indexOfDelegate, indexOfVoid, indexOfField, indexOfClose, indexOfAttribut);

                if (startIndex == indexOfUsing)
                {
                    ParseUsing();
                }
                else
                {
                    if (startIndex == indexOfNamespace)
                    {
                        ParseNamespace();
                    }
                    else
                    {
                        if (startIndex == indexOfClass)
                        {
                            ParseClass();
                        }
                        else
                        {
                            if (startIndex == indexOfInterface)
                            {
                                ParseInterface();
                            }
                            else
                            {
                                if (startIndex == indexOfStuct)
                                {
                                    ParseStruct();
                                }
                                else
                                {
                                    if (startIndex == indexOfEnum)
                                    {
                                        ParseEnum();
                                    }
                                    else
                                    {
                                        if (startIndex == indexOfDelegate)
                                        {
                                            ParseDelegate();
                                        }
                                        else
                                        {
                                            if (startIndex == indexOfVoid)
                                            {
                                                int method = parseLine.ToString().IndexOf(")");
                                                if ((method > -1) && (method < indexOfVoid))
                                                {
                                                    ParseMethod(method, startIndex, false);
                                                }
                                                else
                                                {
                                                    ParseProperty();
                                                }
                                            }
                                            else
                                            {
                                                if (startIndex == indexOfField)
                                                {

                                                    int ab = indexOfKeyword(parseLine.ToString().IndexOf("abstract"), "abstract");
                                                    int ex = indexOfKeyword(parseLine.ToString().IndexOf("extern"), "extern");
                                                    int m = Min(ab, ex);
                                                    if ((m > 0) && (m < indexOfField))
                                                    {
                                                        int method = parseLine.ToString().IndexOf(")");
                                                        if ((method > -1) && (method < indexOfField))
                                                        {
                                                            ParseMethod(method, startIndex, true);
                                                        }
                                                        else
                                                        {
                                                            ParseField(startIndex);
                                                        }

                                                    }
                                                    else
                                                    {
                                                        ParseField(startIndex);
                                                    }
                                                }
                                                else
                                                {
                                                    if (startIndex == indexOfClose)
                                                    {
                                                        parseLine.Remove(0, startIndex + 1);
                                                        if (statusOfClass.Count >= statusOfNamepsace.Count)
                                                        {
                                                            if (statusOfClass.Count != 0)
                                                            {
                                                                statusOfClass.RemoveAt(statusOfClass.Count - 1);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (statusOfNamepsace.Count != 0)
                                                            {
                                                                statusOfNamepsace.RemoveAt(statusOfNamepsace.Count - 1);
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (startIndex == indexOfAttribut)
                                                        {
                                                            ParseAttribut();
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
            }
        }
        public void ReadFromFile(string filename)
        {
            parseLine.Remove(0, parseLine.Length);
            SR = File.OpenText(filename);
            bool etStatus = false;
            char c;
            while (SR.Peek() > -1)
            {
                c = (char)SR.Read();
                if (c.ToString().CompareTo("'") == 0)
                {
                    parseLine.Append(c);
                    while (((char)SR.Peek()) != '\'')
                    {
                        if ((char)SR.Read() == '\\')
                        {
                            SR.Read();
                        }
                    }
                    c = (char)SR.Read();
                }
                if (c == '"')
                {
                    parseLine.Append(c);
                    while ((char)SR.Peek() != '"')
                    {
                        if ((char)SR.Read() == '\\')
                        {
                            if ((etStatus == true) && ((char)SR.Peek() == '"'))
                            {

                            }
                            else
                            {
                                SR.Read();
                            }
                        }
                    }
                    c = (char)SR.Read();
                    etStatus = false;
                }
                if ((c == '/') && ((char)SR.Peek() == '/'))
                {
                    while (((char)SR.Peek() != '\n') && (SR.Peek() > -1))
                    {
                        c = (char)SR.Read();
                    }
                    c = ' ';
                }

                if ((char)SR.Peek() == '@')
                {
                    //munen stringjet me kon string s = @"C:\fisne" per mos me e bo sring s = "C:\\fisne"

                    parseLine.Append(" ");
                    c =  (char)SR.Read();
                    etStatus = true;
                }

                if ((c == '/') && ((char)SR.Peek() == '*'))
                {
                    while ((c != '*') || ((char)SR.Peek() != '/'))
                    {
                        c = (char)SR.Read();
                    }
                    SR.Read();
                    c = ' ';
                }

                if (c == '#')
                //#region per region ku krejt kodin ebon kollap
                {
                    while (((char)SR.Peek() != '\n') && (SR.Peek() > -1))
                    {
                        c = (char)SR.Read();
                    }
                    c = ' ';
                }

                parseLine.Append(c);
            }
            SR.Close();
            if (indexOfKeyword(parseLine.ToString().IndexOf("namespace"), "namespace") != -1)
            {
                ProcessLine();
            }
            SR.Dispose();

        }
    }
}