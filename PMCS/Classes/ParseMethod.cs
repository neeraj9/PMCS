using System;
using System.Collections.Generic;
using System.Text;

namespace PMCS.Classes
{
    class ParseMethod
    {

        Parser parser = new Parser();
        private String[] operands = new String[] { "+", "-", "*", "/", "%", "&", "|", "^", "!", "~", "true", "false", ">", "<", "=", "?", ":", "new", "return", "this." };

        FMethod fMethod = new FMethod();
        public ParseMethod(Parser p)
        {
            parser = p;
        }
        public bool FindParameter(string s, int ind)
        {
            fMethod = parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[ind];
            for (int i = 0; i < fMethod.MVariable.Count; i++)
            {
                if (s.CompareTo(fMethod.MVariable[i].LName) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public bool FindOperand(string s)
        {
            for (int i = 0; i < operands.Length; i++)
            {
                if (operands[i].CompareTo(s.Trim()) == 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void ProcessString(string s)
        {
            int ind = parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods.Count - 1;
            int index;
            s = s.Trim();
            //process invokation
            if (s.IndexOf("(") > -1 && s.IndexOf(")") > -1)
            {
                FInvocation fi = new FInvocation();
                fi.FID = parser.inputSource.ElementID;
                if (s.IndexOf("(") == 0)
                {
                    fi.FParent = "";
                    fi.FName = s.Substring(0, s.IndexOf(")") + 1);
                }
                else
                {
                    string tempS = s.Substring(0, s.IndexOf("(")).Trim().Replace("new", " ").Trim();
                    String[] Fi = tempS.Trim().Split('.');
                    index = Fi.Length - 1;
                    if (index > 0)
                    {
                        fi.FParent = Fi[index - 1];
                        fi.FName = Fi[index];
                    }
                    else
                    {
                        fi.FName = Fi[index];
                        fi.FParent = "";
                    }
                }
                fi.FInvokedBy = parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[ind].MId;
                parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[ind].MInvocation.Add(fi);
                parser.inputSource.ElementID++;
            }
            else
            {
                s = s.Trim();
                s = s.Replace('(', ' ');
                s = s.Replace(')', ' ');
                s = s.Trim();
                String[] e = s.Split(' ');
                //process variable
                if (e.Length > 1)
                {
                    FLocalVariable lv = new FLocalVariable();

                    lv.LId = parser.inputSource.ElementID;
                    lv.LName = e[e.Length - 1];
                    //int ind = parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods.Count - 1;
                    lv.LBelongsTo = parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[ind].MId;
                    lv.LStub = false;
                    parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[ind].MVariable.Add(lv);
                    parser.inputSource.ElementID++;
                }
                else
                {
                    //process access
                    if (FindParameter(e[0], ind) == false)
                    {
                        String[] facc = e[0].Split('.');
                        FAccess fa = new FAccess();
                        fa.AID = parser.inputSource.ElementID;
                        fa.AName = facc[0];
                        fa.ABelongsTo = parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CId;
                        fa.AAccessBy = parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[ind].MId;
                        parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[ind].MAccess.Add(fa);
                        parser.inputSource.ElementID++;
                    }
                }
            }
        }
        public void ReadCharacters(string s)
        {
            s = s.Trim();
            string temp = "";
            string param = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (s[i] == '<')
                {
                    int ch = 0;
                    int stat = 1;
                    int j = i + 1;
                    while ((j < s.Length) && (stat > 0))
                    {
                        if (s[j] == '<')
                        {
                            stat++;
                        }
                        if (s[j] == '>')
                        {
                            stat--;
                            ch = j;
                        }
                        j++;
                    }
                    if (ch > 0)
                    {
                        param += s.Substring(i, ch + 1 - i);
                        i = ch + 1;

                    }
                }
                if (i < s.Length)
                //po pysin prap se i-ja a eshtema e vogel se s-ja se  i = ch + 1; mundet me e rrit i ma shuem se karakterat e s
                {
                    if (FindOperand(s[i].ToString()) == true)
                    {
                        ProcessString(param);
                        param = "";
                        temp = "";
                        i++;
                    }
                    if (i < s.Length)
                    {
                        if (s[i] == ' ')
                        {
                            if (FindOperand(temp) == true)
                            {
                                param = param.Replace(temp, " ").Trim();
                                ProcessString(param);

                            }
                            temp = "";
                        }
                        param += s[i];
                        temp += s[i];
                    } 
                }
            }
            if (param != "")
            {
                ProcessString(param);
            }
        }
        public void ParseEnd(int index)
        {
            ReadCharacters(parser.parseLine.ToString().Substring(0, index));
            parser.parseLine.Remove(0, index + 1);
        }
        public void ParseFor()
        {
            int i = 0;
            int status = 1;
            int openFor = parser.parseLine.ToString().IndexOf("(");
            int openBr = parser.parseLine.ToString().IndexOf("{");
            int close = parser.parseLine.ToString().IndexOf(";");
            int min = parser.Min(openBr, openFor, close);

            if (min == openFor)
            {
                parser.parseLine.Remove(0, openFor + 1);
                while (status > 0)
                {
                    if (parser.parseLine.ToString()[i] == '(')
                    {
                        status++;
                    }
                    else
                    {
                        if (parser.parseLine.ToString()[i] == ')')
                        {
                            status--;
                        }
                    }
                    i++;
                }
                parser.parseLine.Replace(')', ';', i - 1, 1);
            }
            else
            {
                if (min == openBr)
                {
                    parser.parseLine.Remove(0, openBr);
                }
                else
                {
                    if (min == close)
                    {
                        parser.parseLine.Remove(0, parser.parseLine.ToString().IndexOf(" "));
                    }
                }
            }

        }
        public int FindMethod(string s)
        {
            FClass fClass = new FClass();
            for (int i = 0; i < fClass.CMethods.Count; i++)
            {
                if (s.CompareTo(fClass.CMethods[i].MName) == 0)
                {
                    return parser.inputSource.ListOfNamespaces[parser.statusOfNamepsace[parser.statusOfNamepsace.Count - 1]].NClasses[parser.statusOfClass[parser.statusOfClass.Count - 1]].CMethods[i].MId;
                }
            }
            return 0;
        }
        public void ParseMehodLine()
        {
            int status = 1;
            while (status != 0)
            {

                int indexOfEnd = parser.parseLine.ToString().IndexOf(";");
                int indexOfFor = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("for"), "for");
                int indexOfDo = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("do"), "do");
                int indexOfWhile = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("while"), "while");
                int indexOfIf = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("if"), "if");
                int indexOfForE = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("foreach"), "foreach");
                int indexOfElse = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("else"), "else");
                int indexOfSwitch = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("switch"), "switch");
                int indexOfClose = parser.parseLine.ToString().IndexOf("}");
                int indexOfCatch = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("catch"), "catch");
                int indexOfUsing = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("using"), "using");
                int indexOfCase = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("case"), "case");
                int indexOfGo = parser.indexOfKeyword(parser.parseLine.ToString().IndexOf("goto"), "case");
                int indexOfOpen = parser.parseLine.ToString().IndexOf("{");

                //try
                //{
                int min = parser.Min(indexOfEnd, indexOfFor, indexOfDo, indexOfWhile, indexOfIf, indexOfForE, indexOfElse, indexOfSwitch, indexOfClose, indexOfOpen, indexOfCatch, indexOfUsing, indexOfCase, indexOfGo);
                if (min == indexOfEnd)
                {
                    ParseEnd(min);
                }
                else
                {
                    if ((min == indexOfFor) || (min == indexOfSwitch) || (min == indexOfUsing) || (min == indexOfCatch) || (min == indexOfWhile) || (min == indexOfIf) || (min == indexOfForE))
                    {
                        ParseFor();
                    }
                    else
                    {
                        if (min == indexOfDo)
                        {
                            parser.parseLine.Remove(0, min + 2);
                        }
                        else
                        {

                            if (min == indexOfElse)
                            {
                                parser.parseLine.Remove(0, min + 4);
                            }
                            else
                            {
                                if (min == indexOfSwitch)
                                {
                                }
                                else
                                {
                                    if (min == indexOfClose)
                                    {
                                        parser.parseLine.Remove(0, min + 1);
                                        status--;
                                    }
                                    else
                                    {
                                        if (min == indexOfOpen)
                                        {
                                            parser.parseLine.Remove(0, min + 1);
                                            status++;
                                        }
                                        else
                                        {

                                            if (min == indexOfCase)
                                            {
                                                parser.parseLine.Remove(0, parser.parseLine.ToString().IndexOf(":") + 1);
                                            }
                                            else
                                            {
                                                if (min == indexOfGo)
                                                {
                                                    parser.parseLine.Remove(0, parser.parseLine.ToString().IndexOf(";") + 1);
                                                }
                                            }

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                //}
                //catch
                //{
                //    parser.parseLine.Remove(0, indexOfClose + 1);
                //    status = 0;
                //}
            }
        }
    }
}

