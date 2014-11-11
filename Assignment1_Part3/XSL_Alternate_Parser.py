__author__ = 'gerson64'
from bs4 import BeautifulSoup


def tableElem( stringIn ):
   "This prints a passed string into this function"
   stringOut = str('<TD>'+stringIn+'</TD>')
   return stringOut



initial_string='<HTML><TABLE BORDER="1" WIDTH="100%"><THEAD BGCOLOR="GREY"><TR><TD>License</TD><TD>number</TD><TD>issue_date</TD><TD>exp_date</TD><TD>DOB</TD><TD>End</TD><TD>Class</TD><TD>Rest</TD><TD>Sex</TD><TD>Height</TD><TD>Weight</TD><TD>Eyes</TD><TD>Hair</TD><TD>Donor</TD><TD>First_Name</TD><TD>Last_Name</TD><TD>Street</TD><TD>State</TD><TD>City</TD><TD>Zip</TD></TR></THEAD><TBODY BGCOLOR="WHITE">'
end_string='</TBODY></TABLE></HTML>'
inFile = open("C:/Users/gerson64/Desktop/Dropbox Sync/Dropbox/Fall 2014/XML/Assignment1_Part3/drivers.xml", 'r')

soup = BeautifulSoup(inFile)
#print(soup.prettify())

licenses=soup.find_all('license')

mid_string=''

for license in licenses:
    picture = ""
    number = ""
    issue_date = ""
    exp_date = ""
    DOB = ""
    End = ""
    Class = ""
    Rest = ""
    Sex = ""
    Height = ""
    Weight = ""
    Eyes = ""
    Hair = ""
    Donor = ""
    Name_First= ""
    Name_Last= ""
    Address_Street= ""
    Address_State= ""
    Address_City= ""
    Address_Zip= ""


    if license.find("picture") is not None:
        picture = license.find("picture")
        picture = picture.text


    if license.find("number") is not None:
        number = license.find("number")
        number = number.text


    if license.find("issue_date") is not None:
        issue_date = license.find("issue_date")
        issue_date = issue_date.text

    if license.find("exp_date") is not None:
        exp_date = license.find("exp_date")
        exp_date = exp_date.text

    if license.find("dob") is not None:
        DOB = license.find("dob")
        DOB = DOB.text


    if license.find("end") is not None:
        End = license.find("end")
        End = End.text

    if license.find("class") is not None:
        Class = license.find("class")
        Class = Class.text

    if license.find("rest") is not None:
        Rest = license.find("rest")
        Rest = Rest.text

    if license.find("sex") is not None:
       Sex = license.find("sex")
       Sex = Sex.text

    if license.find("height") is not None:
        Height = license.find("height")
        Height = Height.text

    if  license.find("weight") is not None:
        Weight = license.find("weight")
        Weight = Weight.text

    if license.find("eyes") is not None:
        Eyes = license.find("eyes")
        Eyes = Eyes.text

    if license.find("hair") is not None:
        Hair = license.find("hair")
        Hair = Hair.text

    if license.find("donor") is not None:
        Donor = license.find("donor")
        Donor = Donor.text

    if license.find("first") is not None:
        Name_First = license.find("first")
        Name_First = Name_First.text

    if license.find("last") is not None:
        Name_Last = license.find("last")
        Name_Last = Name_Last.text

    if license.find("street") is not None:
        Address_Street = license.find("street")
        Address_Street = Address_Street.text

    if license.find("state") is not None:
        Address_State = license.find("state")
        Address_State = Address_State.text

    if license.find("city") is not None:
        Address_City = license.find("city")
        Address_City = Address_City.text

    if license.find("zip") is not None:
        Address_Zip = license.find("zip")
        Address_Zip = Address_Zip.text

    picture=str('<picture> <img src="' + picture +'" alt="IMG" style="width:304px;height:228px"></picture>')

    mid_string=str(mid_string + tableElem(picture) + tableElem(number) + tableElem(issue_date) + tableElem(exp_date) + tableElem(DOB) + tableElem(End) + tableElem(Class) + tableElem(Rest) + tableElem(Sex) + tableElem(Height) + tableElem(Weight) + tableElem(Eyes)  + tableElem(Hair) + tableElem(Donor) + tableElem(Name_First) + tableElem(Name_Last) + tableElem(Address_Street) + tableElem(Address_State) + tableElem(Address_City) + tableElem(Address_Zip) )


outFileString=str(initial_string+mid_string+end_string)

outFile = open("C:/Users/gerson64/Desktop/Dropbox Sync/Dropbox/Fall 2014/XML/Assignment1_Part3/out.html", 'w')
outFile.write(outFileString)
outFile.close()