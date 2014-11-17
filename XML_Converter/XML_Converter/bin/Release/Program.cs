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

namespace XML_Converter
{

    class Program
    {

        static void Main(string[] args)
        {
            

            Console.WriteLine("This program takes any XML formatted in a 3 node structure.");
            Console.WriteLine("The highest level node indicating the collection");
            Console.WriteLine("The second highest node indicating collection elements");
            Console.WriteLine("The lowest level nodes indicate the attributes of the collection elements.");


            Console.WriteLine("Where an attribute collision occurs the last attribute value placed will be selected");

            //string newPath = Console.ReadLine();

            string path = @"resources\Books.xml";
            string readText = File.ReadAllText(path);
            FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read);

            XML_factory xmlOut = XMLParser(path);
            
            string outPath = String.Format("{0}resources/BooksOUT.xml",AppDomain.CurrentDomain.BaseDirectory);

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
                    else
                    {
                        Console.WriteLine("No duplicate found");
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
                    else
                    {
                        Console.WriteLine("No duplicate found");
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
        
    }

}
