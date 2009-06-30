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
    public partial class Form1 : Form
    {
        List<FNamespace> fmN = new List<FNamespace>();
        InputSource inputSuorce = new InputSource();
        MainMenu MyMenu; 
           
        public Form1()
        {
            InitializeComponent(); 
    Text = "PARSING AND MODELING C# SYSTEMS";
   MyMenu = new MainMenu();
           
    // Add top-level menu items to the menu. 
    MenuItem m1 = new MenuItem("Choose Visualization Form");          
    MyMenu.MenuItems.Add(m1);

    MyMenu.MenuItems.Add(m1);
    MenuItem subm2 = new MenuItem("Text");
    m1.MenuItems.Add(subm2);

    MenuItem subm1 = new MenuItem("TreeView");
    m1.MenuItems.Add(subm1);

    MenuItem m2 = new MenuItem("Exit");
    MyMenu.MenuItems.Add(m2); 


    m2.Click += new EventHandler(MMExit);
    subm1.Click += new EventHandler(MMOpenTree);
    subm2.Click += new EventHandler(MMOpenText);
    Menu = MyMenu;
  }
           protected void MMOpenTree(object who, EventArgs e)
        {
            btn_Parse.Visible = false;
            if (button2.Visible == false)
            {
                button2.Visible = true;
            }
          
            
        }
        protected void MMOpenText(object who, EventArgs e)
        {
            button2.Visible = false;
            if (btn_Parse.Visible == false)
            {
                btn_Parse.Visible = true;
            }

        }
        protected void MMExit(object who, EventArgs e)
        {
             DialogResult result = MessageBox.Show("Stop Program?", 
                            "Terminate", 
                             MessageBoxButtons.YesNo); 
 
    if(result == DialogResult.Yes) Application.Exit(); 
  } 


        

        private void btn_Parse_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            inputSuorce.ElementID = 0;
            inputSuorce.ListOfNamespaces.Clear();
            //inputSuorce.ReadProject(this.textBox1.Text); //ja jep folderin InpitSOursit
            List<FNamespace> fmN = new List<FNamespace>();
            fmN = inputSuorce.ListOfNamespaces; //klasat fieldsat metodat krejt po i run ne listen e namspaceve
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
        private void btn_Export_Click(object sender, EventArgs e)
        {
            sfd.ShowDialog();
            if (sfd.FileName != "")
            {
                List<FNamespace> fmN = new List<FNamespace>();
                StreamWriter SW = new StreamWriter(sfd.FileName);//this.textBox2.Text + "//test.mse");
                StringBuilder line = new StringBuilder();

                fmN = inputSuorce.ListOfNamespaces;
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
                    //for (int xxx = 0; xxx < fmN[i].FInvoc.Count; xxx++)
                    //{
                    //    line.AppendLine("(FAMIX.Access (id: " + fmN[i].FAccess[xxx].AID + ")");
                    //    line.AppendLine("\t\t\t(accessedIn (idref: " + fmN[i].FAccess[xxx].ABelongsTo+ "))");
                    //    line.AppendLine("\t\t\t(accesses (idref: " + fmN[i].FAccess[xxx].AAccesses + "))");
                    //    line.AppendLine("\t\t\t(stub false))");
                    //}
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

        private int fileCount;

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

        private void btn_Open_Click(object sender, EventArgs e)
        {
            this.listBox1.Items.Clear();
            fbd.ShowDialog();
            this.textBox1.Text = fbd.SelectedPath;  //e perdor FileBrowseDialog per me hap file, metode e C#
        }

        private void button1_Click(object sender, EventArgs e)
        {
            fbd.ShowDialog();
            this.textBox2.Text = fbd.SelectedPath;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
          //  this.lblFileCount.Text += "a";
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode node = new TreeNode();
        }

        private void btn_Open_Click_1(object sender, EventArgs e)
        {

            this.listBox1.Items.Clear();
            fbd.ShowDialog();
            this.textBox1.Text = fbd.SelectedPath;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            fmN = inputSuorce.ListOfNamespaces;
            //inputSuorce.ReadProject(this.textBox1.Text);
            this.treeView4.Nodes.Clear();
            for (int nspace = 0; nspace < fmN.Count; nspace++)
            {
                TreeNode NS = new TreeNode();
                NS.Name = nspace.ToString();
                NS.Text = "namespace " + fmN[nspace].NName + ", with id: " + fmN[nspace].NId;
                for (int clas = 0; clas < fmN[nspace].NClasses.Count; clas++)
                {
                    TreeNode CL = new TreeNode();
                    //CL.Index = clas;
                    CL.Name = nspace.ToString() + "-" + clas.ToString();
                    //this.treeView4.Nodes.Insert(clas, NS);
                    CL.Text = "Class name: " + fmN[nspace].NClasses[clas].CName+", Id: "+ fmN[nspace].NClasses[clas].CId + ", and it belongs to: "+ fmN[nspace].NClasses[clas].CBelongsTo;

                    NS.Nodes.Add(CL);

                    TreeNode F = new TreeNode();
                    F.Name = nspace.ToString() + "-" + clas.ToString() + "-F1";
                    F.Text = "Fields";
                    CL.Nodes.Add(F);
                    for (int f = 0; f < fmN[nspace].NClasses[clas].cCFields.Count; f++)
                    {
                        TreeNode ff = new TreeNode();
                        ff.Name = nspace.ToString() + "-" + clas.ToString() + "-" + f.ToString();
                        //for (int kk = 0; kk < fmN[nspace].NClasses[clas].cCFields[f].FName.Count; kk++)
                        //{
                            ff.Text = fmN[nspace].NClasses[clas].cCFields[f].FID + "  Field name: " + fmN[nspace].NClasses[clas].cCFields[f].FName + " modifier is: " + fmN[nspace].NClasses[clas].cCFields[f].FAccessControlQualifier;
                            F.Nodes.Add(ff.Text);
                       //}
                    }

                    TreeNode M = new TreeNode();
                    M.Name = nspace.ToString() + "-" + clas.ToString() + "-M1";
                    M.Text = "Methods";
                    CL.Nodes.Add(M);
                    for (int m = 0; m < fmN[nspace].NClasses[clas].CMethods.Count; m++)
                    {
                        TreeNode mm = new TreeNode();
                        mm.Name = nspace.ToString() + "-" + clas.ToString() + "-" + m.ToString();
                        mm.Text =fmN[nspace].NClasses[clas].CMethods[m].MId+ " Method name: " + fmN[nspace].NClasses[clas].CMethods[m].MName + ", belongs to: " + fmN[nspace].NClasses[clas].CMethods[m].MBelongsTo + ", modifier: " + fmN[nspace].NClasses[clas].CMethods[m].mMccessControlQualifier + " kind: " + fmN[nspace].NClasses[clas].CMethods[m].MKind; 
                        
                        M.Nodes.Add(mm);
                    }
                    TreeNode P = new TreeNode();
                    P.Name = nspace.ToString() + "-" + clas.ToString() + "-P1";
                    P.Text = "Properties";
                    CL.Nodes.Add(P);
                    for (int p = 0; p < fmN[nspace].NClasses[clas].CAttributes.Count; p++)
                    {
                        TreeNode pp = new TreeNode();
                        pp.Name = nspace.ToString() + "-" + clas.ToString() + "-" + p.ToString();
                        pp.Text = "Property name: " + fmN[nspace].NClasses[clas].CAttributes[p].AName + ", belongs to: " + fmN[nspace].NClasses[clas].CAttributes[p].ABelongsTo + ", type: " + fmN[nspace].NClasses[clas].CAttributes[p].AAccessControlQualifier ;

                        P.Nodes.Add(pp);
                    }

                }
                TreeNode A = new TreeNode();
                A.Name = nspace.ToString() + "-A";
                A.Text = "Accesses";
                NS.Nodes.Add(A);
                for (int a = 0; a < fmN[nspace].FAccess.Count; a++)
                {
                    TreeNode aa = new TreeNode();
                    aa.Name = nspace.ToString() + "-" + a.ToString();
                    aa.Text = "method id: " + fmN[nspace].FAccess[a].AAccessBy + ", field name: " + fmN[nspace].FAccess[a].AName;

                    A.Nodes.Add(aa);
                }
                TreeNode Invoke = new TreeNode();
                Invoke.Name = nspace.ToString() + "-Invoke";
                Invoke.Text = "Invocation";
                NS.Nodes.Add(Invoke);
                for (int inv = 0; inv < fmN[nspace].FInvoc.Count; inv++)
                {
                    TreeNode invokee = new TreeNode();
                    invokee.Name = nspace.ToString() + "-" + inv.ToString();
                    invokee.Text = "  method with id: " + fmN[nspace].FInvoc[inv].FInvokedBy + ", invokes method " + fmN[nspace].FInvoc[inv].FName;

                    Invoke.Nodes.Add(invokee);
                }
                TreeNode Inherit = new TreeNode();
               
                Inherit.Name = nspace.ToString() + "-Invoke";
                Inherit.Text = "Inheritance";
                NS.Nodes.Add(Inherit);
                for (int inh = 0; inh < fmN[nspace].FInheritance.Count; inh++)
                {
                    TreeNode inher = new TreeNode();
                    inher.Name = nspace.ToString() + "-" + inh.ToString();
                    inher.Text = "  class with id: " + fmN[nspace].FInheritance[inh].SubClass + ", inherits class whith id: " + fmN[nspace].FInheritance[inh].SuperClass;

                    Invoke.Nodes.Add(inher);
                }
                //NS.Index = nspace;
                this.treeView4.Nodes.Add(NS);

            }


        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btn_Export_Click_1(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

        }

       

    }
}
