__author__ = 'gerson64'
from xml.etree.ElementTree import Element, SubElement, Comment, tostring
from xml.etree import ElementTree
from xml.dom import minidom
import hashlib
from lxml import etree


def prettify(elem):
    """Return a pretty-printed XML string for the Element.
    """
    rough_string = ElementTree.tostring(elem, 'utf-8')
    reparsed = minidom.parseString(rough_string)
    return reparsed.toprettyxml(indent="  ")


inFile = open("C:/Users/gerson64/Desktop/Dropbox Sync/Dropbox/Fall 2014/XML/Week2/employees2.csv", 'r')
outFile = open("C:/Users/gerson64/Desktop/Dropbox Sync/Dropbox/Fall 2014/XML/Week2/out.xml", 'w')

employees = Element('employees')

for line in inFile:
    name = ''
    salary = ''
    jobtitle = ''
    region = ''
    splitline = line.split(',')
    name = splitline[0].strip()
    salary = splitline[1].strip()
    jobtitle = splitline[2].strip()
    region = splitline[3].strip()

    employee = SubElement(employees, "employee")
    name_node = SubElement(employee, "name")
    name_node.text = name

    salary_node = SubElement(employee, "salary")
    salary_node.text = salary

    jobtitle_node = SubElement(employee, "jobtitle")
    jobtitle_node.text = jobtitle

    region_node = SubElement(employee, "region")
    region_node.text = region


# print prettify(employees)
outFile.write(prettify(employees))
outFile.close()
inFile.close()

schema_root = etree.XML('''\
<xs:schema attributeFormDefault="unqualified" elementFormDefault="qualified" xmlns:xs="http://www.w3.org/2001/XMLSchema">
  <xs:element name="employees">
    <xs:complexType>
      <xs:sequence>
        <xs:element name="employee" maxOccurs="unbounded" minOccurs="0">
          <xs:complexType>
            <xs:sequence>
              <xs:element type="xs:string" name="name"/>
              <xs:element type="xs:int" name="salary"/>
              <xs:element type="xs:string" name="jobtitle"/>
              <xs:element type="xs:string" name="region"/>
            </xs:sequence>
          </xs:complexType>
        </xs:element>
      </xs:sequence>
    </xs:complexType>
  </xs:element>
</xs:schema>
 ''')
schema = etree.XMLSchema(schema_root)

parser = etree.XMLParser(schema = schema)

try :
    etree.fromstring(open("C:/Users/gerson64/Desktop/Dropbox Sync/Dropbox/Fall 2014/XML/Week2/out.xml").read(), parser)
except:
    print("THIS IS NOT RIGHT!!! FIX THIS XML POSTHASTE")
    exit()



if hashlib.md5(open("C:/Users/gerson64/Desktop/Dropbox Sync/Dropbox/Fall 2014/XML/Week2/out.xml").read().replace("\n",
                                                                                                                 "").replace(
        "\t", "").replace(" ", "").replace("\r", "")).hexdigest() == hashlib.md5(
        open("C:/Users/gerson64/Desktop/Dropbox Sync/Dropbox/Fall 2014/XML/Week2/employees2.xml").read().replace("\n",
                                                                                                                 "").replace(
                "\t", "").replace(" ", "").replace("\r", "")).hexdigest():
    print ("MATCH")