using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class ParseInterface
    {
        Parser parser = new Parser();

        public ParseInterface(Parser p)
        {
            parser = p;
        }

        public void ParseInterfaceMethod()
        {
            FMethod fmMethod = new FMethod();
            int lastIndex = parser.parseLine.ToString().IndexOf(";");
            int startIndex = parser.parseLine.ToString().IndexOf("(");
            String[] method;
            method = parser.parseLine.ToString().Substring(0, startIndex).Trim().Split(' ');
            fmMethod.MId = parser.inputSource.ElementID;
            fmMethod.MName = "";
            fmMethod.MSignature = "";
            fmMethod.mMccessControlQualifier = "";
            for (int i = method.Length - 1; i >= 0; i--)
            {
                if ((method[i] != "") && (method[i] != "\r\n") && (method[i] != "\n") && (method[i] != "\t"))
                {
                    if (fmMethod.MName == "" || fmMethod.MName == null)
                    {
                        fmMethod.MName = method[i];
                    }
                    else
                    {
                        if (fmMethod.MSignature == "")
                        {
                            fmMethod.MSignature = method[i] + fmMethod.MName;
                        }
                        else
                        {
                            if (fmMethod.mMccessControlQualifier == "")
                            {
                                if ((method[i] == "protected") || (method[i] == "public") || (method[i] == "private") || (method[i] == "internal"))
                                {
                                    fmMethod.mMccessControlQualifier = method[i];
                                }
                                //fmMethod.mMccessControlQualifier = method[i];
                            }

                        }

                    }
                }
            }
            fmMethod.MStub = false;
            parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods.Add(fmMethod);
            parser.inputSource.ElementID++;
            parser.parseLine.Remove(0, lastIndex + 1);

        }

    

        public void ProcessInterfaceLine()
        {


            int status = 1;
            while (status > 0)
            {
                int indexOfVoid = parser.parseLine.ToString().IndexOf("{");
                int indexOfField = parser.parseLine.ToString().IndexOf(";");
                int indexOfClose = parser.parseLine.ToString().IndexOf("}");
                int indexOfClass = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("class"), "class");
                int indexOfInterface = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("interface"), "interface");
                int indexOfStuct = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("struct"), "struct");
                int indexOfEnum = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("enum"), "enum");
                int indexOfDelegate = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("delegate"), "delegate");
                int indexOfAttribut = parser.parseLine.ToString().IndexOf("[");

                int indexOfParameter = parser.parseLine.ToString().IndexOf(")");
                if (indexOfAttribut > indexOfParameter)
                {
                    indexOfAttribut = -1;
                }
                int startIndex = parser.Min(indexOfVoid, indexOfField, indexOfClose);
                if (startIndex == indexOfVoid)
                {
                    parser.ParseProperty();
                }
                else
                {
                    if (startIndex == indexOfField)
                    {
                        if (parser.parseLine.ToString().IndexOf(")") > -1)
                        {
                            if (parser.parseLine.ToString().IndexOf("=") > indexOfField || parser.parseLine.ToString().IndexOf("=") == -1)
                            {
                                if (parser.parseLine.ToString().IndexOf(")") < indexOfField)
                                {
                                    ParseInterfaceMethod();
                                }
                                else
                                {
                                    parser.ParseField(indexOfField);
                                }
                            }
                            else
                            {
                                parser.ParseField(indexOfField);
                            }
                        }
                        else
                        {
                            parser.ParseField(indexOfField);
                        }
                    }
                    else
                    {
                        if (startIndex == indexOfClose)
                        {
                            parser.parseLine.Remove(0, startIndex + 1);
                            //if (parser.statusOfClass.Count >= parser.statusOfNamepsace.Count)
                            //{
                            //interfacei munet me pas innerclass edhe na duhet me kqyre ku jemi ne hierarki
                                if (parser.statusOfClass.Count != 0)
                                {
                                    parser.statusOfClass.RemoveAt(parser.statusOfClass.Count - 1);
                                    status--;
                                }
                            //}
                        }
                        if (startIndex == indexOfClass)
                        {
                            parser.ParseClass();
                        }
                        else
                        {
                            if (startIndex == indexOfInterface)
                            {
                                parser.ParseInterface();
                            }
                            else
                            {
                                if (startIndex == indexOfStuct)
                                {
                                    parser.ParseStruct();
                                }
                                else
                                {
                                    if (startIndex == indexOfEnum)
                                    {
                                        parser.ParseEnum();
                                    }
                                    else
                                    {
                                        if (startIndex == indexOfDelegate)
                                        {
                                            parser.ParseDelegate();
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

