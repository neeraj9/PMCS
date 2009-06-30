using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using PMCS.Classes;


namespace PMCS
{
    public partial class frmMain : Form
    {
        InputSource inputSource = new InputSource();
        List<FNamespace> fmN = new List<FNamespace>();

        public frmMain()
        {
            InitializeComponent();
        }

        public void OpenProject()
        {
            this.listBox1.Items.Clear();
            this.treeView1.Nodes.Clear();
            this.lblPath.Text = "";
            fbd.ShowDialog();
            this.lblPath.Text = fbd.SelectedPath;
            if ((fbd.SelectedPath != null) && (fbd.SelectedPath != ""))
            {
                inputSource.ProjectFileCount(fbd.SelectedPath.ToString());
            }
            inputSource.ElementID = 0;
            inputSource.ListOfNamespaces.Clear();
            this.progressBar.Maximum = inputSource.FileCount;
        }

        public void ParseProject()
        {
            this.progressBar.Value = 0;
            this.progressBar.Visible = true;

            if ((fbd.SelectedPath != null) && (fbd.SelectedPath != ""))
            {
                inputSource.ReadProject(fbd.SelectedPath, this.progressBar);
                PopulateText();
                PopulateTree();
            }
            else
            {
                MessageBox.Show("Please select a project path");
            }

            //List<FNamespace> fmN = new List<FNamespace>();
            //fmN = inputSource.ListOfNamespaces;


            this.progressBar.Visible = false;
        }

        public void PopulateText()
        {
            fmN = inputSource.ListOfNamespaces; //klasat fieldsat metodat krejt po i run ne listen e namspaceve
            string stringOfListBox;
            listBox1.Items.Add("This is a project done with C# programming lanuage. ");
            for (int i = 0; i < fmN.Count; i++)
            {

                listBox1.Items.Add("Namespace : " + fmN[i].NName);
                if (fmN[i].ParnetNamespace != null)
                {
                    listBox1.Items.Add("Belongs to the Namespace " + fmN[i].ParnetNamespace);
                }

                for (int j = 0; j < fmN[i].NClasses.Count; j++)
                {
                    // listBox1.Items.Add("\t\tThis source file has a Class named:" + fmN[i].NClasses[j].CName );
                    string s = "\t\tThis source file has a Class named:" + fmN[i].NClasses[j].CName;
                    if (fmN[i].NClasses[j].ParentClass.Count > 0)
                    {
                        for (int jj = 0; jj < fmN[i].NClasses[j].ParentClass.Count; jj++)
                        {
                            //  s += "It is a child class of the Class " + fmN[i].NClasses[j].ParentClass[jj];

                            listBox1.Items.Add("\t\t\tIt is a child class of the Class " + fmN[i].NClasses[j].ParentClass[jj]);
                        }
                    }
                    s += ", and is under the namespace  " + fmN[i].NClasses[j].CBelongsTo;
                    //listBox1.Items.Add("and is under the namespace  " + fmN[i].NClasses[j].CBelongsTo );
                    if (fmN[i].NClasses[j].CIsAbstract == true)
                    {
                        s += " Class is Abstract class.";
                        //listBox1.Items.Add(" Class is Abstract class.");
                    }
                    listBox1.Items.Add(s);

                    for (int k = 0; k < fmN[i].NClasses[j].cCFields.Count; k++)
                    {

                        //for (int kk = 0; kk < fmN[i].NClasses[j].cCFields[k].FName.Count; kk++)
                        //{

                        listBox1.Items.Add("\t\t\t\tUnder this class there is a Field :  " + fmN[i].NClasses[j].cCFields[k].FName);
                        //}
                    }
                    // listBox1.Items.Add("");
                    for (int l = 0; l < fmN[i].NClasses[j].CAttributes.Count; l++)
                    {
                        //listBox1.Items.Add("");
                        //s += "\t\t\t\tUnder this class there is a property : " + fmN[i].NClasses[j].CAttributes[l].AName;
                        listBox1.Items.Add("\t\t\t\tUnder this class there is a Property : " + fmN[i].NClasses[j].CAttributes[l].AName);
                    }
                    //  listBox1.Items.Add("");
                    for (int m = 0; m < fmN[i].NClasses[j].CMethods.Count; m++)
                    {
                        //   listBox1.Items.Add("");
                        string ss = "\t\t\t\t-Method in this class is called " + fmN[i].NClasses[j].CMethods[m].MName;
                        // listBox1.Items.Add("\t\t\t\t-Method in this class is called " + fmN[i].NClasses[j].CMethods[m].MName );
                        if (fmN[i].NClasses[j].CName == fmN[i].NClasses[j].CMethods[m].MName)
                        {
                            ss += ". Method is Constructor. ";
                            // listBox1.Items.Add("\t\t\t\tMethod is Constructor");
                        }

                        //listBox1.Items.Add("\t\t\t\tIt Belongs To the class with ID " + fmN[i].NClasses[j].CId + "," + "Signature of method is - " + fmN[i].NClasses[j].CMethods[m].MSignature + "-");
                        listBox1.Items.Add(ss);
                        ss = "\t\t\t\tThis Belongs To the class with ID " + fmN[i].NClasses[j].CId + "," + " Signature of method is - " + fmN[i].NClasses[j].CMethods[m].MSignature + "-";
                        //listBox1.Items.Add("and its Has Class Scope property is " + fmN[i].NClasses[j].CMethods[m].MHasClassScope + ".");
                        //listBox1.Items.Add("Signature of method is - " + fmN[i].NClasses[j].CMethods[m].MSignature);

                        //listBox1.Items.Add("\t\t\t\tThis method has " + fmN[i].NClasses[j].CMethods[m].Loc + " lines of code. ");
                        ss += ". This method has " + fmN[i].NClasses[j].CMethods[m].Loc + " lines of code. ";
                        listBox1.Items.Add(ss);
                        //for (int c = 0; c < fmN[i].NClasses[j].CMethods[m].MInvocation.Count; c++)
                        //{
                        //    listBox1.Items.Add("Invovation and accesses of the class are: ");
                        //    listBox1.Items.Add("Invocation: " + fmN[i].NClasses[j].CMethods[m].MInvocation[c].FName);
                        //}
                        //for (int v = 0; v < fmN[i].NClasses[j].CMethods[m].MAccess.Count; v++)
                        //{
                        //    listBox1.Items.Add("");
                        //    listBox1.Items.Add("Access: " + fmN[i].NClasses[j].CMethods[m].MAccess[v].AName);
                        //}
                    }
                    listBox1.Items.Add("");
                }
            }
            for (int inheritaceN = 0; inheritaceN < fmN.Count; inheritaceN++)
            {
                for (int inheritance = 0; inheritance < fmN[inheritaceN].FInheritance.Count; inheritance++)
                {
                    stringOfListBox = "\t\tThe source code has inheritaces between classes and they are specified by the id of classes. The Inheritace  with ID " + fmN[inheritaceN].FInheritance[inheritance].IId + ".";
                    //stringOfListBox += "\tstub: " + fmN[inheritaceN].FInheritance[inheritance].IStub;
                    stringOfListBox += " has a Subclass with ID: " + fmN[inheritaceN].FInheritance[inheritance].SubClass;
                    stringOfListBox += ", and SuperClass with ID: " + fmN[inheritaceN].FInheritance[inheritance].SuperClass + ".";
                    listBox1.Items.Add(stringOfListBox);
                }
            }
            for (int invocationN = 0; invocationN < fmN.Count; invocationN++)
            {
                for (int invocation = 0; invocation < fmN[invocationN].FInvoc.Count; invocation++)
                {
                    //stringOfListBox = "\t\tInvocations of the code are: ";

                    stringOfListBox = "\t\tInvocation Name is:" + fmN[invocationN].FInvoc[invocation].FName;
                    listBox1.Items.Add(stringOfListBox);

                }
            }
            listBox1.Items.Add(" ");
            for (int accessN = 0; accessN < fmN.Count; accessN++)
            {
                for (int access = 0; access < fmN[accessN].FAccess.Count; access++)
                {
                    stringOfListBox = "\t\tSource code has method accesses with class fields that are: ";
                    stringOfListBox += "Access Name is:" + fmN[accessN].FAccess[access].AName;
                    listBox1.Items.Add(stringOfListBox);
                }
            }
        }

        public void PopulateTree()
        {
            //fmN = inputSuorce.ListOfNamespaces;
            //inputSuorce.ReadProject(this.textBox1.Text);
            this.treeView1.Nodes.Clear();
            for (int nspace = 0; nspace < fmN.Count; nspace++)
            {
                TreeNode NS = new TreeNode();
                NS.Name = nspace.ToString();
                NS.Text = "namespace " + fmN[nspace].NName + ", with id: " + fmN[nspace].NId;
                NS.ImageIndex = 0;

                TreeNode L = new TreeNode();
                L.Name = nspace.ToString() + "-L";
                L.Text = "LocalVariable";
                L.SelectedImageIndex = 7;
                L.ImageIndex = 7;
                TreeNode A = new TreeNode();
                A.Name = nspace.ToString() + "-A";
                A.Text = "Accesses";
                A.SelectedImageIndex = 8;
                A.ImageIndex = 8;
                TreeNode Invoke = new TreeNode();
                Invoke.Name = nspace.ToString() + "-Invoke";
                Invoke.Text = "Invocation";
                Invoke.ImageIndex = 5;
                Invoke.SelectedImageIndex = 5;
                TreeNode Inherit = new TreeNode();
                Inherit.Name = nspace.ToString() + "-Invoke";
                Inherit.Text = "Inheritance";
                Inherit.SelectedImageIndex = 6;
                Inherit.ImageIndex = 6;

                for (int clas = 0; clas < fmN[nspace].NClasses.Count; clas++)
                {
                    TreeNode CL = new TreeNode();
                    //CL.Index = clas;
                    CL.Name = nspace.ToString() + "-" + clas.ToString();
                    //this.treeView4.Nodes.Insert(clas, NS);
                    CL.Text = "Class name: " + fmN[nspace].NClasses[clas].CName + ", Id: " + fmN[nspace].NClasses[clas].CId + ", and it belongs to: " + fmN[nspace].NClasses[clas].CBelongsTo;
                    CL.ImageIndex = 1;
                    CL.SelectedImageIndex = 1;
                    NS.Nodes.Add(CL);

                    TreeNode F = new TreeNode();
                    F.Name = nspace.ToString() + "-" + clas.ToString() + "-F1";
                    F.Text = "Fields";
                    F.ImageIndex = 2;
                    F.SelectedImageIndex = 2;
                    CL.Nodes.Add(F);
                    for (int f = 0; f < fmN[nspace].NClasses[clas].cCFields.Count; f++)
                    {
                        TreeNode ff = new TreeNode();
                        ff.Name = nspace.ToString() + "-" + clas.ToString() + "-" + f.ToString();
                        ff.Text = fmN[nspace].NClasses[clas].cCFields[f].FID + "  Field name: " + fmN[nspace].NClasses[clas].cCFields[f].FName + " modifier is: " + fmN[nspace].NClasses[clas].cCFields[f].FAccessControlQualifier;
                        ff.ImageIndex = 2;
                        ff.SelectedImageIndex = 2;
                        F.Nodes.Add(ff);
                    }

                    TreeNode M = new TreeNode();
                    M.Name = nspace.ToString() + "-" + clas.ToString() + "-M1";
                    M.Text = "Methods";
                    M.ImageIndex = 3;
                    M.SelectedImageIndex = 3;
                    CL.Nodes.Add(M);

                    for (int m = 0; m < fmN[nspace].NClasses[clas].CMethods.Count; m++)
                    {
                        TreeNode mm = new TreeNode();
                        mm.Name = nspace.ToString() + "-" + clas.ToString() + "-" + m.ToString();
                        mm.Text = fmN[nspace].NClasses[clas].CMethods[m].MId + " Method name: " + fmN[nspace].NClasses[clas].CMethods[m].MName + ", belongs to: " + fmN[nspace].NClasses[clas].CMethods[m].MBelongsTo + ", modifier: " + fmN[nspace].NClasses[clas].CMethods[m].mMccessControlQualifier + " kind: " + fmN[nspace].NClasses[clas].CMethods[m].MKind;
                        mm.ImageIndex = 3;
                        mm.SelectedImageIndex = 3;
                        M.Nodes.Add(mm);


                        for (int l = 0; l < fmN[nspace].NClasses[clas].CMethods[m].MVariable.Count; l++)
                        {
                            TreeNode ll = new TreeNode();
                            ll.Name = nspace.ToString() + "-" + l.ToString();
                            ll.Text = fmN[nspace].NClasses[clas].CMethods[m].MVariable[l].LId + " name: " + fmN[nspace].NClasses[clas].CMethods[m].MVariable[l].LName + ", belongTo: " + fmN[nspace].NClasses[clas].CMethods[m].MVariable[l].LBelongsTo;
                            ll.SelectedImageIndex = 7;
                            ll.ImageIndex = 7;
                            L.Nodes.Add(ll);
                        }
                    }
                    TreeNode P = new TreeNode();
                    P.Name = nspace.ToString() + "-" + clas.ToString() + "-P1";
                    P.Text = "Properties";
                    P.ImageIndex = 4;
                    P.SelectedImageIndex = 4;
                    CL.Nodes.Add(P);
                    for (int p = 0; p < fmN[nspace].NClasses[clas].CAttributes.Count; p++)
                    {
                        TreeNode pp = new TreeNode();
                        pp.Name = nspace.ToString() + "-" + clas.ToString() + "-" + p.ToString();
                        pp.Text = "Property name: " + fmN[nspace].NClasses[clas].CAttributes[p].AName + ", belongs to: " + fmN[nspace].NClasses[clas].CAttributes[p].ABelongsTo + ", type: " + fmN[nspace].NClasses[clas].CAttributes[p].AType;
                        pp.ImageIndex = 4;
                        pp.SelectedImageIndex = 4;
                        P.Nodes.Add(pp);
                    }

                }



                L.Text = "LocalVariable";
                NS.Nodes.Add(L);
                NS.Nodes.Add(A);
                for (int a = 0; a < fmN[nspace].FAccess.Count; a++)
                {
                    TreeNode aa = new TreeNode();
                    aa.Name = nspace.ToString() + "-" + a.ToString();
                    aa.Text = "method id: " + fmN[nspace].FAccess[a].AAccessBy + ", field name: " + fmN[nspace].FAccess[a].AName;
                    aa.SelectedImageIndex = 8;
                    aa.ImageIndex = 8;
                    A.Nodes.Add(aa);
                }

                NS.Nodes.Add(Invoke);
                for (int inv = 0; inv < fmN[nspace].FInvoc.Count; inv++)
                {
                    TreeNode invokee = new TreeNode();
                    invokee.Name = nspace.ToString() + "-" + inv.ToString();
                    invokee.Text = "  method with id: " + fmN[nspace].FInvoc[inv].FInvokedBy + ", invokes " + fmN[nspace].FInvoc[inv].FName + ", Candidate: " + fmN[nspace].FInvoc[inv].FCandidate;
                    invokee.ImageIndex = 5;
                    invokee.SelectedImageIndex = 5;
                    Invoke.Nodes.Add(invokee);
                }

                NS.Nodes.Add(Inherit);
                for (int inh = 0; inh < fmN[nspace].FInheritance.Count; inh++)
                {
                    TreeNode inher = new TreeNode();
                    inher.Name = nspace.ToString() + "-" + inh.ToString();
                    inher.Text = "  class with id: " + fmN[nspace].FInheritance[inh].SubClass + ", inherits class whith id: " + fmN[nspace].FInheritance[inh].SuperClass;
                    inher.ImageIndex = 6;
                    inher.SelectedImageIndex = 6;
                    Inherit.Nodes.Add(inher);
                }




                //NS.Index = nspace;
                this.treeView1.Nodes.Add(NS);

            }
        }

        private void openProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenProject();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            OpenProject();
        }



        private void TreeViewMenu_Click(object sender, EventArgs e)
        {
            this.listBox1.Visible = false;
            this.listBox1.Enabled = false;
            this.treeView1.Visible = true;
            this.treeView1.Enabled = true;
        }

        private void TextMenu_Click(object sender, EventArgs e)
        {
            this.listBox1.Visible = true;
            this.listBox1.Enabled = true;
            this.treeView1.Visible = false;
            this.treeView1.Enabled = false;
        }

        private void runParseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParseProject();
        }

        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            ParseProject();
        }


        public void ExportMSE()
        {
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                StreamWriter SW = new StreamWriter(sfd.FileName);//this.textBox2.Text + "//test.mse");
                StringBuilder line = new StringBuilder();

                //fmN = inputSource.ListOfNamespaces;
                line.AppendLine("(Moose.Model (id: 1)");
                line.AppendLine("\t(name 'PMC#')");
                line.AppendLine("\t(entity");
                for (int i = 0; i < fmN.Count; i++)
                {
                    line.AppendLine("\t\t(FAMIX.Package (id: " + fmN[i].NId + ")");
                    line.AppendLine("\t\t\t(name '" + fmN[i].NName + "'))");
                    if (fmN[i].NPackagedIn > 1)
                    {
                        line.AppendLine("\t\t\t(packagedIn (idref: " + fmN[i].NPackagedIn + "))");
                    }

                    for (int k = 0; k < fmN[i].NClasses.Count; k++)
                    {
                        line.AppendLine("\t\t(FAMIX.Class (id: " + fmN[i].NClasses[k].CId + ")");
                        line.AppendLine("\t\t\t(name '" + fmN[i].NClasses[k].CName + "')");
                        line.AppendLine("\t\t\t(packagedIn (idref: " + fmN[i].NId + "))");
                        if (fmN[i].NClasses[k].CIsAbstract == true)
                        {
                            line.AppendLine("\t\t\t(isAbstract true))");
                        }
                        else
                        {
                            line.AppendLine("\t\t\t(isAbstract false))");
                        }
                        for (int x = 0; x < fmN[i].NClasses[k].CMethods.Count; x++)
                        {
                            line.AppendLine("\t\t(FAMIX.Method (id: " + fmN[i].NClasses[k].CMethods[x].MId + ")");
                            line.AppendLine("\t\t\t(name '" + fmN[i].NClasses[k].CMethods[x].MName + "')");
                            line.AppendLine("\t\t\t(accessControlQualifier '" + fmN[i].NClasses[k].CMethods[x].mMccessControlQualifier + "')");
                            line.AppendLine("\t\t\t(belongsTo (idref: " + fmN[i].NClasses[k].CId + "))");
                            line.AppendLine("\t\t\t(LOC " + fmN[i].NClasses[k].CMethods[x].Loc + ")");
                            line.AppendLine("\t\t\t(packagedIn (idref: " + fmN[i].NId + "))");
                            line.AppendLine("\t\t\t(signature '" + fmN[i].NClasses[k].CMethods[x].MSignature + "'))");

                        }
                        for (int n = 0; n < fmN[i].NClasses[k].cCFields.Count; n++)
                        {
                            line.AppendLine("\t\t(FAMIX.Attribute (id: " + fmN[i].NClasses[k].cCFields[n].FID + ")");
                            line.AppendLine("\t\t\t(name '" + fmN[i].NClasses[k].cCFields[n].FName + "')");
                            if (fmN[i].NClasses[k].cCFields[n].FAccessControlQualifier != "")
                            {
                                line.AppendLine("\t\t\t(accessControlQualifier '" + fmN[i].NClasses[k].cCFields[n].FAccessControlQualifier + "')");
                            }
                            line.AppendLine("\t\t\t(belongsTo (idref: " + fmN[i].NClasses[k].CId + ")))");
                        }
                    }
                    for (int x = 0; x < fmN[i].FInheritance.Count; x++)
                    {
                        line.AppendLine("(FAMIX.InheritanceDefinition (id: " + fmN[i].FInheritance[x].IId + ")");
                        line.AppendLine("\t\t\t(stub false)");
                        line.AppendLine("\t\t\t(subclass (idref: " + fmN[i].FInheritance[x].SubClass + "))");
                        line.AppendLine("\t\t\t(superclass (idref: " + fmN[i].FInheritance[x].SuperClass + ")))");
                    }
                    for (int xx = 0; xx < fmN[i].FInvoc.Count; xx++)
                    {
                        line.AppendLine("(FAMIX.Invocation (id: " + fmN[i].FInvoc[xx].FID + ")");
                        line.AppendLine("\t\t\t(candidate (idref: " + fmN[i].FInvoc[xx].FCandidate + "))");
                        line.AppendLine("\t\t\t(invokedBy (idref: " + fmN[i].FInvoc[xx].FInvokedBy + "))");
                        line.AppendLine("\t\t\t(invokes '" + fmN[i].FInvoc[xx].FName + "')");
                        line.AppendLine("\t\t\t(stub false))");
                    }
                    for (int xxx = 0; xxx < fmN[i].FAccess.Count; xxx++)
                    {
                        line.AppendLine("(FAMIX.Access (id: " + fmN[i].FAccess[xxx].AID + ")");
                        line.AppendLine("\t\t\t(accessedIn (idref: " + fmN[i].FAccess[xxx].ABelongsTo + "))");
                        line.AppendLine("\t\t\t(accesses (idref: " + fmN[i].FAccess[xxx].AAccesses + "))");
                        line.AppendLine("\t\t\t(stub false))");
                    }
                }
                line.Remove(line.Length - 2, 1);
                line.AppendLine(")");
                line.AppendLine("(sourceLanguage 'C#'))");
                SW.Write(line.ToString());
                SW.Close();
                MessageBox.Show("The file " + sfd.FileName + " is created");
            }
            else
            {
                MessageBox.Show("Please select the file name");
            }
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportMSE();
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {
            ExportMSE();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAbout fAbout = new frmAbout();
            fAbout.ShowDialog(this);
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

    }
}