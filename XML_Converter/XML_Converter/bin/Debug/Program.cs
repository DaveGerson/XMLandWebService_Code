using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Windows.Forms;

namespace XML_Converter
{

    class Program
    {

        [STAThread]
        static void Main(string[] args)
        {
            System.Threading.Thread.CurrentThread.ApartmentState = System.Threading.ApartmentState.STA;




            string path = infileLocationFinder();
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            //Prompts user for valid location to Save Data

            int outCheck = 0;
            string outPath = "";
            while (outCheck.Equals(0))
            {
                //Find output dir from user
                outPath = saveLocationFinder();
                if (hasWriteAccessToFolder(Path.GetDirectoryName(outPath).ToString()))
                {
                    outCheck = 1;
                }

            }




            //Code is called to Run

            XML_factory xmlOut = XMLParser(path);
            XMLPrinter(xmlOut, outPath);

        }

        public static XML_factory XMLParser(string path)
        {
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(stream);

            XmlNode root = xmlDoc.SelectSingleNode("(/*)");
            Console.WriteLine("First Level Node");
            Console.WriteLine(root.Name.ToString());

            string XML_Root = root.Name.ToString();
            List<XML_Node> XML_node_List = new List<XML_Node>();

            XmlNodeList nodeList = xmlDoc.SelectNodes("(/*/*)");
            IEnumerator mainEnum = nodeList.GetEnumerator();
            while (mainEnum.MoveNext())
            {
                Console.WriteLine("Second Level Node");
                XmlNode secondNode = (XmlNode)mainEnum.Current;
                Console.WriteLine(secondNode.Name.ToString());
                string TopNode = secondNode.Name.ToString();
                List<XML_element> curElemList = new List<XML_element>();

                IEnumerator attributeEnum = secondNode.Attributes.GetEnumerator();
                while (attributeEnum.MoveNext())
                {
                    XmlNode curAttribute = (XmlAttribute)attributeEnum.Current;
                    string valtype = curAttribute.Name.ToString();
                    string value = curAttribute.Value.ToString();

                    XML_element curNodeElemsAt = new XML_element(valtype, value);
                    Console.WriteLine(curNodeElemsAt.Valtype.ToString());
                    Console.WriteLine(curNodeElemsAt.Value.ToString());
                    //Turn on for testing for add node functionality
                    //curElemList.Add(curNodeElems);
                    if (curElemList.Exists(x => x.Valtype == curNodeElemsAt.Valtype))
                    {
                        int repIndex = curElemList.FindIndex(x => x.Valtype.Equals(curNodeElemsAt.Valtype));
                        curElemList.RemoveAt(repIndex);
                    }
                    ;
                    curElemList.Add(curNodeElemsAt);
                }


                XmlNodeList lower_nodeList = secondNode.ChildNodes;
                IEnumerator secondEnum = lower_nodeList.GetEnumerator();
                while (secondEnum.MoveNext())
                {
                    Console.WriteLine("Third Level Node");
                    XmlNode thirdNode = (XmlNode)secondEnum.Current;
                    Console.Write(thirdNode.Name.ToString());
                    Console.Write(" : ");
                    Console.Write(thirdNode.InnerText.ToString());

                    string valtype = thirdNode.Name.ToString();
                    string value = thirdNode.InnerText.ToString();
                    XML_element curNodeElemsNode = new XML_element(valtype, value);
                    if (curElemList.Exists(x => x.Valtype == curNodeElemsNode.Valtype))
                    {
                        int repIndex = curElemList.FindIndex(x => x.Valtype.Equals(curNodeElemsNode.Valtype));
                        curElemList.RemoveAt(repIndex);
                    }
                    ;
                    curElemList.Add(curNodeElemsNode);

                }

                XML_Node curxml_node = new XML_Node(TopNode, curElemList);
                XML_node_List.Add(curxml_node);
            }
            XML_factory out_xml = new XML_factory(XML_Root, XML_node_List);
            return out_xml;
        }

        /*
         * 
         * The below items are the objects used to create the xml object that will be fed into the XML creator class.  
         * */

        public class XML_element
        {
            public string Valtype { get; set; }
            public string Value { get; set; }
            public XML_element(string valtype, string value)
            {
                Valtype = valtype;
                Value = value;
            }

        }


        public class XML_Node
        {
            public string TopNodes { get; set; }
            public List<XML_element> NodeElements { get; set; }
            public XML_Node(string topNodes, List<XML_element> nodeElements)
            {
                TopNodes = topNodes;
                NodeElements = nodeElements;
            }

        }



        public class XML_factory
        {
            public string Root { get; set; }
            public List<XML_Node> Elements { get; set; }
            public XML_factory(string root, List<XML_Node> elements)
            {
                Root = root;
                Elements = elements;
            }
        }



        /*
         * 
         * The below items are the objects used to create the xml object that will be fed into the XML creator class.  
         * */


        public static void XMLPrinter(XML_factory xmlObj, string outpath)
        {
            XmlDocument outdoc = new XmlDocument();
            XmlElement xmlRoot = outdoc.CreateElement(xmlObj.Root);

            IEnumerator nodeEnum = xmlObj.Elements.GetEnumerator();
            while (nodeEnum.MoveNext())
            {
                XML_Node curNode = (XML_Node)nodeEnum.Current;
                XmlElement node = outdoc.CreateElement(curNode.TopNodes);
                IEnumerator attEnum = curNode.NodeElements.GetEnumerator();
                while (attEnum.MoveNext())
                {
                    XML_element curAtt = (XML_element)attEnum.Current;
                    XmlAttribute curOutAtt = outdoc.CreateAttribute(curAtt.Valtype);
                    curOutAtt.Value = curAtt.Value;
                    node.Attributes.Append(curOutAtt);
                }
                xmlRoot.AppendChild(node);
            }
            outdoc.AppendChild(xmlRoot);

            outdoc.Save(outpath);
        }


        //http://stackoverflow.com/questions/1410127/c-sharp-test-if-user-has-write-access-to-a-folder
        public static bool hasWriteAccessToFolder(string folderPath)
        {
            try
            {
                // Attempt to get a list of security permissions from the folder. 
                // This will raise an exception if the path is read only or do not have access to view the permissions. 
                System.Security.AccessControl.DirectorySecurity ds = Directory.GetAccessControl(folderPath);
                return true;
            }
            catch (UnauthorizedAccessException)
            {
                return false;
            }
        }



        public static string saveLocationFinder()
        {
            SaveFileDialog saveFileDialog_in = new SaveFileDialog();
            saveFileDialog_in.Title = "Select ouput Location";
            saveFileDialog_in.Filter = "XML output|*.xml";
            DialogResult saveFilein_result = saveFileDialog_in.ShowDialog();


            if (saveFilein_result == DialogResult.Cancel | saveFilein_result == DialogResult.Abort)
            {
                System.Environment.Exit(1);
            }

            return saveFileDialog_in.FileName;
        }


        //http://stackoverflow.com/questions/3036829/how-to-create-a-messagebox-with-yes-no-choices-and-a-dialogresult-in-c
        //http://stackoverflow.com/questions/16136383/reading-a-text-file-using-openfiledialog-in-windows-forms
        public static string infileLocationFinder()
        {

            string OUTPATH = "";

            DialogResult dialogResult = MessageBox.Show("Do you want to use the default books xml?", "Do you want to use the default books xml?", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes)
            {
                OUTPATH = @"resources\Books.xml";
            }
            else if (dialogResult == DialogResult.No)
            {

                OpenFileDialog inFilePrompt = new OpenFileDialog();
                inFilePrompt.Title = "Open XML for reconfiguration";
                inFilePrompt.Filter = "XML file|*.xml";
                System.Windows.Forms.DialogResult inDialog = inFilePrompt.ShowDialog();
                if (inDialog == DialogResult.OK)
                {
                    try
                    {
                        string tryPath = inFilePrompt.FileName;
                        XDocument.Load(tryPath);
                        OUTPATH = tryPath;
                    }
                    catch (Exception x)
                    {
                        MessageBox.Show("This file Could not be read as a valid XML. Please try again.");
                        System.Environment.Exit(1);
                    }
                }
                if (inDialog == DialogResult.Cancel | inDialog == DialogResult.Abort)
                {
                    System.Environment.Exit(1);
                }

            }
            return OUTPATH;
        }




    }



}