using System.Collections.Generic;
using System.IO;
using System.Text;

namespace PMCS.Classes
{
    internal class MseOutput
    {
        public void WriteMse(List<FNamespace> namespaces, TextWriter writer)
        {
            var line = new StringBuilder();

            line.AppendLine("(Moose.Model (id: 1)");
            line.AppendLine("\t(name 'PMC#')");
            line.AppendLine("\t(entity");
            for(int i = 0; i < namespaces.Count; i++)
            {
                line.AppendLine("\t\t(FAMIX.Package (id: " + namespaces[i].NId + ")");
                line.AppendLine("\t\t\t(name '" + namespaces[i].NName + "'))");
                if(namespaces[i].NPackagedIn > 1)
                {
                    line.AppendLine("\t\t\t(packagedIn (idref: " + namespaces[i].NPackagedIn + "))");
                }

                for(int k = 0; k < namespaces[i].NClasses.Count; k++)
                {
                    line.AppendLine("\t\t(FAMIX.Class (id: " + namespaces[i].NClasses[k].CId + ")");
                    line.AppendLine("\t\t\t(name '" + namespaces[i].NClasses[k].CName + "')");
                    line.AppendLine("\t\t\t(packagedIn (idref: " + namespaces[i].NId + "))");
                    if(namespaces[i].NClasses[k].CIsAbstract)
                    {
                        line.AppendLine("\t\t\t(isAbstract true))");
                    }
                    else
                    {
                        line.AppendLine("\t\t\t(isAbstract false))");
                    }
                    for(int x = 0; x < namespaces[i].NClasses[k].CMethods.Count; x++)
                    {
                        line.AppendLine("\t\t(FAMIX.Method (id: " + namespaces[i].NClasses[k].CMethods[x].MId + ")");
                        line.AppendLine("\t\t\t(name '" + namespaces[i].NClasses[k].CMethods[x].MName + "')");
                        line.AppendLine("\t\t\t(accessControlQualifier '" +
                                        namespaces[i].NClasses[k].CMethods[x].mMccessControlQualifier + "')");
                        line.AppendLine("\t\t\t(belongsTo (idref: " + namespaces[i].NClasses[k].CId + "))");
                        line.AppendLine("\t\t\t(LOC " + namespaces[i].NClasses[k].CMethods[x].Loc + ")");
                        line.AppendLine("\t\t\t(packagedIn (idref: " + namespaces[i].NId + "))");
                        line.AppendLine("\t\t\t(signature '" + namespaces[i].NClasses[k].CMethods[x].MSignature + "'))");
                    }
                    for(int n = 0; n < namespaces[i].NClasses[k].cCFields.Count; n++)
                    {
                        line.AppendLine("\t\t(FAMIX.Attribute (id: " + namespaces[i].NClasses[k].cCFields[n].FID + ")");
                        line.AppendLine("\t\t\t(name '" + namespaces[i].NClasses[k].cCFields[n].FName + "')");
                        if(namespaces[i].NClasses[k].cCFields[n].FAccessControlQualifier != "")
                        {
                            line.AppendLine("\t\t\t(accessControlQualifier '" +
                                            namespaces[i].NClasses[k].cCFields[n].FAccessControlQualifier + "')");
                        }
                        line.AppendLine("\t\t\t(belongsTo (idref: " + namespaces[i].NClasses[k].CId + ")))");
                    }
                }
                for(int x = 0; x < namespaces[i].FInheritance.Count; x++)
                {
                    line.AppendLine("(FAMIX.InheritanceDefinition (id: " + namespaces[i].FInheritance[x].IId + ")");
                    line.AppendLine("\t\t\t(stub false)");
                    line.AppendLine("\t\t\t(subclass (idref: " + namespaces[i].FInheritance[x].SubClass + "))");
                    line.AppendLine("\t\t\t(superclass (idref: " + namespaces[i].FInheritance[x].SuperClass + ")))");
                }
                for(int xx = 0; xx < namespaces[i].FInvoc.Count; xx++)
                {
                    line.AppendLine("(FAMIX.Invocation (id: " + namespaces[i].FInvoc[xx].FID + ")");
                    line.AppendLine("\t\t\t(candidate (idref: " + namespaces[i].FInvoc[xx].FCandidate + "))");
                    line.AppendLine("\t\t\t(invokedBy (idref: " + namespaces[i].FInvoc[xx].FInvokedBy + "))");
                    line.AppendLine("\t\t\t(invokes '" + namespaces[i].FInvoc[xx].FName + "')");
                    line.AppendLine("\t\t\t(stub false))");
                }
                for(int xxx = 0; xxx < namespaces[i].FAccess.Count; xxx++)
                {
                    line.AppendLine("(FAMIX.Access (id: " + namespaces[i].FAccess[xxx].AID + ")");
                    line.AppendLine("\t\t\t(accessedIn (idref: " + namespaces[i].FAccess[xxx].ABelongsTo + "))");
                    line.AppendLine("\t\t\t(accesses (idref: " + namespaces[i].FAccess[xxx].AAccesses + "))");
                    line.AppendLine("\t\t\t(stub false))");
                }
            }
            line.Remove(line.Length - 2, 1);
            line.AppendLine(")");
            line.AppendLine("(sourceLanguage 'C#'))");
            writer.Write(line.ToString());
        }
    }
}